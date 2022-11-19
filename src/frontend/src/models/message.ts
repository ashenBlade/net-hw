import Guid from "./guid";

export interface Message {
    username: string
    message: string
    requestId?: Guid
    fileId?: Guid
}