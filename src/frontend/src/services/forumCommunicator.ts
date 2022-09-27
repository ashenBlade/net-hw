import {Message} from "../models/message";
import {MessageCallback} from "./messageCallback";

export interface ForumCommunicator {
    registerOnMessageCallback: (cb: MessageCallback) => (void)
    sendMessage: (msg: Message) => Promise<void>
    unregisterOnMessageCallback: (cb: MessageCallback) => (void)
}