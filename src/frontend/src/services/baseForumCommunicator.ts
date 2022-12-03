import {ForumCommunicator} from "../interfaces/forumCommunicator";
import {MessageCallback} from "../interfaces/messageCallback";
import {Message} from "../models/message";
import {FileUploadedCallback} from "../interfaces/fileUploadedCallback";
import {UploadFile} from "../models/uploadFile";
import {ChatEndedCallback} from "../interfaces/chatEndedCallback";
import {ChatStartedCallback} from "../interfaces/chatStartedCallback";

export abstract class BaseForumCommunicator implements ForumCommunicator {
    messageCallbacks: MessageCallback[] = []
    fileUploadedCallbacks: FileUploadedCallback[] = []
    chatEndedCallbacks: ChatEndedCallback[] = []
    chatStartedCallbacks: ChatStartedCallback[] = []

    protected constructor() {  }

    registerOnFileUploadedCallback(cb: FileUploadedCallback): void {
        this.fileUploadedCallbacks.push(cb)
    }

    unregisterOnFileUploadCallback(cb: FileUploadedCallback): void {
        const index = this.fileUploadedCallbacks.indexOf(cb);
        if (index !== -1) {
            this.fileUploadedCallbacks.splice(index, 1);
        }
    }

    protected notifyFileUploaded(uploadedFile: UploadFile) {
        this.fileUploadedCallbacks.forEach(cb => cb(uploadedFile));
    }

    registerOnMessageCallback(cb: MessageCallback): void {
        this.messageCallbacks.push(cb);
    }

    unregisterOnMessageCallback(cb: MessageCallback): void {
        const index = this.messageCallbacks.indexOf(cb);
        if (index !== -1) {
            this.messageCallbacks.splice(index, 1);
        }
    }

    private unregisterCallbackBase<T>(array: T[], cb: T): void {
        const index = array.indexOf(cb);
        if (index !== -1) {
            array.splice(index, 1)
        }
    }

    protected notifyMessage(msg: Message) {
        this.messageCallbacks.forEach(cb => cb(msg));
    }

    abstract sendMessage(msg: Message): Promise<void>;
    abstract endChat(): Promise<void>;
    abstract login(username: string): Promise<void>;

    registerOnChatEndedCallback(cb: ChatEndedCallback): void {
        this.chatEndedCallbacks.push(cb)
    }

    registerOnChatStartedCallback(cb: ChatStartedCallback): void {
        this.chatStartedCallbacks.push(cb)
    }

    unregisterOnChatEndedCallback(cb: ChatEndedCallback): void {
        this.unregisterCallbackBase(this.chatEndedCallbacks, cb)
    }

    unregisterOnChatStartedCallback(cb: ChatStartedCallback): void {
        this.unregisterCallbackBase(this.chatStartedCallbacks, cb);
    }
}