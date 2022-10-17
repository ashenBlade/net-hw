import {Message} from "../models/message";

export interface MessagesRepository {
    getPreviousMessages: (page: number, size: number) => Promise<Message[]>
}

