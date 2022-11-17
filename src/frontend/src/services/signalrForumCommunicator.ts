import {Message} from "../models/message";
import {BaseForumCommunicator} from "./baseForumCommunicator";
import * as signalR from '@microsoft/signalr'
import {HubConnection, LogLevel} from '@microsoft/signalr'
import {FetchHttpClient} from "@microsoft/signalr/dist/esm/FetchHttpClient";
import {ConsoleLogger} from "@microsoft/signalr/dist/esm/Utils";
import FileRepository from "../interfaces/fileRepository";
import Guid from "../models/guid";

export class SignalrForumCommunicator extends BaseForumCommunicator {
    static messagePublishedFunction = "publishMessage";
    static fileUploadedFunction = "onFileUploaded";
    connection: HubConnection

    constructor(readonly url: string,
                readonly fileRepository: FileRepository,
                readonly chatEndpoint: string = '/chat') {
        super();
        let fetchHttpClient = new FetchHttpClient(new ConsoleLogger(LogLevel.Information));
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(this.endpoint, {
                withCredentials: false,
                httpClient: fetchHttpClient
            })
            .build();
    }

    get endpoint() {
        return `${this.url}${this.chatEndpoint}`
    }

    async open() {
        this.connection.on(SignalrForumCommunicator.messagePublishedFunction,
            (username, message, requestId) => {
                console.log('Received published message');
                this.notifyMessage({
                    username,
                    message,
                    requestId: requestId === undefined || requestId === null
                        ? undefined
                        : new Guid(requestId),
                });
            });
        this.connection.on(SignalrForumCommunicator.fileUploadedFunction,
            (requestId: string, fileId: string, metadata: string) => {
                console.log('Received file uploaded event')
                this.notifyFileUploaded({
                    fileId: new Guid(fileId),
                    requestId: new Guid(requestId),
                    metadata: new Map(Object.entries(JSON.parse(metadata))
                        .map(([x, y]) => ([String(x), String(y)]))),
                    contentUrl: this.fileRepository.createContentUrl(new Guid(fileId))
                })
            })
        await this.connection.start();

    }

    async close() {
        await this.connection.stop();
    }

    async sendMessage(msg: Message): Promise<void> {
        
        await this.connection.send(SignalrForumCommunicator.messagePublishedFunction,
            msg.username,
            msg.message,
            msg.requestId ?? null)
            .catch(e => {
                console.error('Could not send message', {
                    error: e
                });
        });
    }
}