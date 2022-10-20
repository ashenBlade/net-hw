import {Message} from "../models/message";
import {MessagesRepository} from "../interfaces/messagesRepository";
import FileRepository from "../interfaces/fileRepository";
import Attachment from "../models/attachment";

export class MessagesApiMessagesRepository implements MessagesRepository {
    constructor(readonly url: string, 
                readonly fileRepository: FileRepository) {  }
    
    async downloadAttachment(fileId: string | null): Promise<Attachment | null> {
        if (!fileId) {
            return null;
        }
        
        const file = await this.fileRepository.getFileAsync(fileId);
        if (!file) {
            console.warn('Could not download user attachment. File service did not returned file from provided fileId', {fileId})
            return null;
        }
        
        return {
            fileId,
            contentUrl: URL.createObjectURL(file),
            name: file.name,
            contentType: file.type
        }
    }
    
    async parseMessage(obj: any): Promise<Message> {
        const message = obj.message;
        const username = obj.username;
        const fileId = obj.fileId;
        
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
            username,
            message,
            attachment: await this.downloadAttachment(fileId) ?? undefined
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