import {Message} from "../models/message";
import {MessagesRepository} from "./messagesRepository";

export class MessagesApiMessagesRepository implements MessagesRepository {
    constructor(readonly url: string) {  }

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

        // const response = await fetch(`${this.url}/api/messages?from-end=true&page=${page}&size=${size}`, {
        //     method: 'GET',
        //     mode: 'cors',
        //     credentials: 'include'
        // })
        //
        // if (!response.ok) {
        //     throw new Error(`Could not get response from server. Invalid response status code: ${response.status} ${response.statusText}`)
        // }
        //
        // const json = await response.json();
        // let result: Message[];
        // try {
        //     result = json.map((item: any) => parseMessage(item))
        // } catch (e) {
        //     throw new Error('Server responded with invalid json. Could not parse')
        // }
        // return result;
        return [
            {username: 'Stub name', message: 'Sample message'},
            {username: 'Ilya', message: 'Sample message 2'},
            {username: 'Stub name', message: 'Sample message 3'},
            {username: 'Vlad', message: 'Sample message 4'},
        ]
    }
}