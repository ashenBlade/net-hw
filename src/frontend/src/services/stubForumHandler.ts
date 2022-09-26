import {ForumHandler, MessageCallback} from "./forumHandler";
import {Message} from "../models/message";

export class StubForumHandler implements ForumHandler {
    callbacks: MessageCallback[] = []

    registerOnMessageCallback(cb: (msg: Message) => void): void {
        this.callbacks.push(cb);
    }

    async sendMessage(msg: Message): Promise<void> {
        for (const callback of this.callbacks) {
            callback(msg)
        }
    }

}