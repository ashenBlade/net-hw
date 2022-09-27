import {ForumHandler} from "./forumHandler";
import {Message} from "../models/message";
import {MessageCallback} from "./messageCallback";
import {MessagesRepository} from "./messagesRepository";
import {ForumCommunicator} from "./forumCommunicator";

export class AggregatedForumHandler implements ForumHandler {
    constructor(readonly repository: MessagesRepository,
                readonly communicator: ForumCommunicator) {
    }

    getPreviousMessages(page: number, size: number): Promise<Message[]> {
        return this.repository.getPreviousMessages(page, size);
    }

    registerOnMessageCallback(cb: MessageCallback): void {
        this.communicator.registerOnMessageCallback(cb);
    }

    sendMessage(msg: Message): Promise<void> {
        return this.communicator.sendMessage(msg);
    }

    unregisterOnMessageCallback(cb: MessageCallback): void {
        this.communicator.unregisterOnMessageCallback(cb);
    }

}