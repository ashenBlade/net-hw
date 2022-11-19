import ChatMessage from "./СhatMessage";
import Attachment from "../../../models/attachment";

export interface ChatProps {
    messages: ChatMessage[],
    files: Map<string, Attachment>
}