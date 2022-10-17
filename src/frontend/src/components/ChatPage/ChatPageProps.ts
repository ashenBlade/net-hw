import {ForumHandler} from "../../interfaces/forumHandler";
import FileRepository from "../../interfaces/fileRepository";

export interface ChatPageProps {
    forumHandler: ForumHandler
    username: string
    fileRepository: FileRepository
}