import Attachment from "../models/attachment";
import Guid from "../models/guid";

export default interface FileRepository {
    getFileAsync(fileId: Guid): Promise<Attachment | null>

    createContentUrl(fileId: Guid): string;

    uploadFileAsync(file: File, metadata: Map<string, string>, requestId: Guid): Promise<void>
}