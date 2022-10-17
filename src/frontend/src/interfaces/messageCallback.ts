import {Message} from "../models/message";

export type MessageCallback = ((msg: Message) => (void))