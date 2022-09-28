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
        const object = JSON.parse(body);
        const messageObject = object.message;
        const message = {
            username: messageObject.username,
            message: messageObject.message
        }
        return new RabbitMqMessagePublishedEvent(message.username, message.username)
    }



    toMessage(): Message {
        return this.message;
    }
}