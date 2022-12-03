import {Message} from "../models/message";
import {MessageCallback} from "./messageCallback";
import {FileUploadedCallback} from "./fileUploadedCallback";
import {ChatStartedCallback} from "./chatStartedCallback";
import {ChatEndedCallback} from "./chatEndedCallback";

export interface ForumCommunicator {
    sendMessage: (msg: Message) => Promise<void>
    login: (username: string) => Promise<void>
    endChat: () => Promise<void>

    registerOnChatStartedCallback: (cb: ChatStartedCallback) => void;
    unregisterOnChatStartedCallback: (cb: ChatStartedCallback) => void;

    registerOnChatEndedCallback: (cb: ChatEndedCallback) => void;
    unregisterOnChatEndedCallback: (cb: ChatEndedCallback) => void;

    registerOnMessageCallback: (cb: MessageCallback) => (void);
    unregisterOnMessageCallback: (cb: MessageCallback) => (void);

    registerOnFileUploadedCallback: (cb: FileUploadedCallback) => (void);
    unregisterOnFileUploadCallback: (cb: FileUploadedCallback) => (void);
}