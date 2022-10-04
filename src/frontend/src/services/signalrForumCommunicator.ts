import {Message} from "../models/message";
import {BaseForumCommunicator} from "./baseForumCommunicator";
import * as signalR from '@microsoft/signalr'
import {HubConnection, LogLevel} from "@microsoft/signalr";
import {FetchHttpClient} from "@microsoft/signalr/dist/esm/FetchHttpClient";
import {ConsoleLogger} from "@microsoft/signalr/dist/esm/Utils";

export class SignalrForumCommunicator extends BaseForumCommunicator {
    static messagePublishedFunction = "publishMessage";
    connection: HubConnection

    constructor(readonly url: string,
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
        this.connection.on(SignalrForumCommunicator.messagePublishedFunction, (username, message) => {
            if (!(typeof username === 'string' && typeof message === "string")) {
                console.error('Received message arguments are not strings', {
                    username: username,
                    message: message
                });
                return;
            }

            this.notifyMessage({username, message});
        });
        await this.connection.start();

    }

    async close() {
        await this.connection.stop();
    }

    async sendMessage(msg: Message): Promise<void> {
        await this.connection.send(SignalrForumCommunicator.messagePublishedFunction, msg.username, msg.message)
            .catch(e => {
                console.error('Could not send message', {
                    error: e
                });
        });
    }
}