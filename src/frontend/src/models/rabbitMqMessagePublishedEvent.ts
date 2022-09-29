import {Message, parseMessage} from "./message";

export class RabbitMqMessagePublishedEvent {
    readonly message: {
        readonly username: string,
        readonly message: string
    }

    readonly messageType: string[] = [
        'urn:message:MessagesListener.Events:MessagePublished'
    ]

    constructor(username: string, message: string) {
        if (!username) {
            throw new Error('Username not provided');
        }
        if (!message) {
            throw new Error('Message not provided');
        }

        this.message = {
            username,
            message
        }
    }

    static fromString(body: string): RabbitMqMessagePublishedEvent {
        const message = JSON.parse(body).message;
        return new RabbitMqMessagePublishedEvent(message.username, message.message)
    }
    

    toMessage(): Message {
        return this.message;
    }
}