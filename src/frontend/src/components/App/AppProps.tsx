import {ForumHandler} from "../../interfaces/forumHandler";
import FileRepository from "../../interfaces/fileRepository";

export interface AppProps {
    forumHandler: ForumHandler,
    fileRepository: FileRepository
}