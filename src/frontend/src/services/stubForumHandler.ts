import {ForumHandler} from "./forumHandler";
import {Message} from "../models/message";
import {MessageCallback} from "./messageCallback";

export class StubForumHandler implements ForumHandler {
    callbacks: MessageCallback[] = []

    close(): Promise<void> {
        return Promise.resolve(undefined);
    }

    unregisterOnMessageCallback(cb: MessageCallback): void {
        const index = this.callbacks.indexOf(cb);
        if (index !== -1) {
            this.callbacks.splice(index, 1);
        }
    }

    getPreviousMessages(page: number, size: number): Promise<Message[]> {
        return Promise.resolve([]);
    }


    open(): Promise<void> {
        return Promise.resolve(undefined);
    }

    registerOnMessageCallback(cb: (msg: Message) => void): void {
        this.callbacks.push(cb);
    }

    async sendMessage(msg: Message): Promise<void> {
        for (const callback of this.callbacks) {
            callback(msg)
        }
    }

}