import {ForumCommunicator} from "./forumCommunicator";
import {MessageCallback} from "./messageCallback";
import {Message} from "../models/message";

export abstract class BaseForumCommunicator implements ForumCommunicator {
    callbacks: MessageCallback[]

    protected constructor() {
        this.callbacks = []
    }

    registerOnMessageCallback(cb: MessageCallback): void {
        this.callbacks.push(cb);
    }

    protected notifyMessage(msg: Message) {
        this.callbacks.forEach(cb => cb(msg));
    }

    abstract sendMessage(msg: Message): Promise<void>;

    unregisterOnMessageCallback(cb: MessageCallback): void {
        const index = this.callbacks.indexOf(cb);
        if (index !== -1) {
            this.callbacks.splice(index, 1);
        }
    }
}