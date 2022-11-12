import {ForumCommunicator} from "../interfaces/forumCommunicator";
import {MessageCallback} from "../interfaces/messageCallback";
import {Message} from "../models/message";
import {FileUploadedCallback} from "../interfaces/fileUploadedCallback";
import {UploadFile} from "../models/uploadFile";

export abstract class BaseForumCommunicator implements ForumCommunicator {
    messageCallbacks: MessageCallback[] = []
    fileUploadedCallbacks: FileUploadedCallback[] = []

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

    protected notifyMessage(msg: Message) {
        this.messageCallbacks.forEach(cb => cb(msg));
    }

    abstract sendMessage(msg: Message): Promise<void>;

    unregisterOnMessageCallback(cb: MessageCallback): void {
        const index = this.messageCallbacks.indexOf(cb);
        if (index !== -1) {
            this.messageCallbacks.splice(index, 1);
        }
    }
}