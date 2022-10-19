import {Message} from "../models/message";
import {BaseForumCommunicator} from "./baseForumCommunicator";
import * as signalR from '@microsoft/signalr'
import {HubConnection, LogLevel} from "@microsoft/signalr";
import {FetchHttpClient} from "@microsoft/signalr/dist/esm/FetchHttpClient";
import {ConsoleLogger} from "@microsoft/signalr/dist/esm/Utils";
import Attachment from "../models/attachment";
import FileRepository from "../interfaces/fileRepository";

export class SignalrForumCommunicator extends BaseForumCommunicator {
    static messagePublishedFunction = "publishMessage";
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
        this.connection.on(SignalrForumCommunicator.messagePublishedFunction, async (username, message, fileId) => {
            console.log('Sfjasdfkjasdfhasdfhasdfjhasdkfjasdlkfjhasdf')
            if (!(typeof username === 'string' && typeof message === "string")) {
                console.error('Received message arguments are not strings', {
                    username,
                    message,
                    fileId
                });
                return;
            }
            
            const getAttachment = async (): Promise<Attachment | undefined> => {
                if (!fileId) {
                    return undefined;
                }
                
                if (typeof fileId !== 'string') {
                    console.error('Returned file id is not string', {
                        fileId
                    })
                }
                
                const file = await this.fileRepository.getFileAsync(fileId);
                if (!file) {
                    console.warn('Could not download file with provided file id. Return undefined', {
                        fileId
                    })
                    return undefined;
                }
                
                return {
                    fileId,
                    contentUrl: URL.createObjectURL(file),
                    name: file.name,
                    contentType: file.type
                }
            }
            
            this.notifyMessage({username, message, attachment: await getAttachment()});
        });
        await this.connection.start();

    }

    async close() {
        await this.connection.stop();
    }

    async sendMessage(msg: Message): Promise<void> {
        
        await this.connection.send(SignalrForumCommunicator.messagePublishedFunction, 
            msg.username,
            msg.message,
            msg.attachment?.fileId ?? null)
            .catch(e => {
                console.error('Could not send message', {
                    error: e
                });
        });
    }
}