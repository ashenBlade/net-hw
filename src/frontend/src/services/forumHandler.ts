import {Message} from "../models/message";

export type MessageCallback = ((msg: Message) => (void))

export interface ForumHandler {
    registerOnMessageCallback: (cb: MessageCallback) => (void)
    sendMessage: (msg: Message) => (void)
}