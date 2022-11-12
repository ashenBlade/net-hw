import Guid from "./guid";

export interface UploadFile {
    requestId: Guid
    fileId: Guid
    metadata: Map<string, string>
    contentUrl: string
}