import Attachment from "../models/attachment";

export default interface FileRepository {
    getFileAsync(fileId: string): Promise<Attachment | null>

    addFileAsync(file: File): Promise<Attachment>
}