import {ForumHandler} from "../interfaces/forumHandler";
import {Message} from "../models/message";
import {MessageCallback} from "../interfaces/messageCallback";
import {MessagesRepository} from "../interfaces/messagesRepository";
import {ForumCommunicator} from "../interfaces/forumCommunicator";
import {FileUploadedCallback} from "../interfaces/fileUploadedCallback";
import {ChatEndedCallback} from "../interfaces/chatEndedCallback";
import {ChatStartedCallback} from "../interfaces/chatStartedCallback";

export class AggregatedForumHandler implements ForumHandler {
    endChat(): Promise<void> {
        return this.communicator.endChat();
    }

    login(username: string): Promise<void> {
        return this.communicator.login(username);
    }

    registerOnChatEndedCallback(cb: ChatEndedCallback): void {
        this.communicator.registerOnChatEndedCallback(cb);
    }

    registerOnChatStartedCallback(cb: ChatStartedCallback): void {
        this.communicator.registerOnChatStartedCallback(cb);
    }

    unregisterOnChatEndedCallback(cb: ChatEndedCallback): void {
        this.communicator.unregisterOnChatEndedCallback(cb);
    }

    unregisterOnChatStartedCallback(cb: ChatStartedCallback): void {
        this.communicator.unregisterOnChatStartedCallback(cb);
    }
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

    registerOnFileUploadedCallback(cb: FileUploadedCallback): void {
        this.communicator.registerOnFileUploadedCallback(cb);
    }

    unregisterOnFileUploadCallback(cb: FileUploadedCallback): void {
        this.communicator.unregisterOnFileUploadCallback(cb);
    }

}