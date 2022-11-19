import ChatMessage from "./СhatMessage";
import Guid from "../../../models/guid";
import Attachment from "../../../models/attachment";

export interface ChatProps {
    messages: ChatMessage[],
    files: Map<Guid, Attachment>
}