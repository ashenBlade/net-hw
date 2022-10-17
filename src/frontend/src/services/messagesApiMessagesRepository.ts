import {Message} from "../models/message";
import {MessagesRepository} from "../interfaces/messagesRepository";

const parseMessage = (obj: any): Message => {
    const message = obj.message;
    const username = obj.username;
    if (!(message && username)) {
        throw new Error('Could not get message from response. Message and username not provided');
    }
    
    if (typeof message !== 'string') {
        throw new Error(`Message must be string. Given: ${message}`)
    }
    
    if (typeof username !== 'string') {
        throw new Error(`Username must be string. Given: ${username}`)
    }
    
    return {
        username, message
    }
}

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

        const response = await fetch(`${this.url}/api/messages?fromEnd=true&page=${page}&size=${size}`, {
            method: 'GET',
            mode: 'cors',
            credentials: 'same-origin'
        })

        if (!response.ok) {
            throw new Error(`Could not get response from server. Invalid response status code: ${response.status} ${response.statusText}`)
        }
        const json: Array<any> = await response.json();
        try {
            return json.map(parseMessage)
        } catch (e: any) {
            throw new Error('Server responded with invalid json. Could not parse', e)
        }
    }
}