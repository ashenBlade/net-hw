import Attachment from "./attachment";

export interface Message {
    username: string
    message: string
    attachment?: Attachment
}