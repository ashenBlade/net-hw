import Attachment from "../models/attachment";

export default interface FileRepository {
    getFileAsync(fileId: string): Promise<File | null>

    addFileAsync(file: File): Promise<Attachment>
}