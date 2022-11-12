import Guid from "../../../models/guid";

export default interface ChatMessage {
    username?: string
    message: string
    requestId?: Guid
    attachment?: {
        name: string
        contentUrl: string
        metadata: Map<string, string>
    }
}