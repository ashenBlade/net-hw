import {MessagesRepository} from "./messagesRepository";
import {ForumCommunicator} from "./forumCommunicator";

export interface ForumHandler extends MessagesRepository, ForumCommunicator {
}