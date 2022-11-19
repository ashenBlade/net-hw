import {Message} from "../models/message";
import {MessagesRepository} from "../interfaces/messagesRepository";
import Guid from "../models/guid";

export class MessagesApiMessagesRepository implements MessagesRepository {
    constructor(readonly url: string) {  }

    async parseMessage(obj: any): Promise<Message> {
        const message = obj.message;
        const username = obj.username;
        const requestId = obj.requestId ? new Guid(obj.requestId) : undefined;
        const fileId = obj.fileId ? new Guid(obj.fileId) : undefined;
        return {
            username,
            message,
            requestId,
            fileId
        }
    }

    async getPreviousMessages(page: number, size: number): Promise<Message[]> {
        if (page < 1) {
            throw new Error('page number must be positive');
        }
        if (size < 1) {
            throw new Error('page size must be positive');
        }
        if (!Number.isInteger(page)) {
            throw new Error('page number must be integer');
        }

        if (!Number.isInteger(size)) {
            throw new Error('page size must be integer');
        }

        const response = await fetch(`${this.url}/api/messages?fromEnd=true&page=${page}&size=${size}`, {
            method: 'GET',
            mode: 'cors',
        })

        if (!response.ok) {
            throw new Error(`Could not get response from server. Invalid response status code: ${response.status} ${response.statusText}`)
        }
        const json: Array<any> = await response.json();
        try {
            return await Promise.all(json.map(obj => this.parseMessage(obj)));
        } catch (e: any) {
            throw new Error('Server responded with invalid json. Could not parse', e)
        }
    }
}