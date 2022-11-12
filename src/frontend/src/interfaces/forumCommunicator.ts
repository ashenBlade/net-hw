import {Message} from "../models/message";
import {MessageCallback} from "./messageCallback";
import {FileUploadedCallback} from "./fileUploadedCallback";

export interface ForumCommunicator {
    sendMessage: (msg: Message) => Promise<void>

    registerOnMessageCallback: (cb: MessageCallback) => (void)
    unregisterOnMessageCallback: (cb: MessageCallback) => (void)

    registerOnFileUploadedCallback: (cb: FileUploadedCallback) => (void)
    unregisterOnFileUploadCallback: (cb: FileUploadedCallback) => (void)
}