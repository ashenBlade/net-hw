import FileRepository from "../interfaces/fileRepository";
import Attachment from "../models/attachment";
import Guid from "../models/guid";

export default class FileApiFileRepository implements FileRepository {
    constructor(readonly fileServerUrl: string,
                readonly fileMetadataServerUrl: string) { }

    createContentUrl(fileId: Guid): string {
        return `${this.fileServerUrl}/api/files/${fileId.value}`;
    }

    private uploadFile(file: File, requestId: Guid): Promise<Response> {
        console.log({
            requestId
        })
        const form = new FormData();
        form.set('file', file);
        form.set('requestId', requestId.value);
        return fetch(`${this.fileServerUrl}/api/files`, {
            method: 'POST',
            body: form,
            mode: 'cors'
        });
    }

    private uploadMetadata(metadata: Map<string, string>, requestId: Guid): Promise<Response> {

        return fetch(`${this.fileMetadataServerUrl}/api/metadata`, {
            method: 'POST',
            body: JSON.stringify({
                metadata: Object.fromEntries(metadata),
                requestId: requestId.value
            }),
            mode: 'cors',
            headers: {
                'Content-Type': 'application/json'
            }
        });
    }

    async uploadFileAsync(file: File, metadata: Map<string, string>): Promise<Guid> {
        const requestGuid = Guid.generate();
        const fileUploadPromise = this.uploadFile(file, requestGuid);
        const metadataUploadPromise = this.uploadMetadata(metadata, requestGuid);
        const [fileUploadResponse, metadataUploadResponse] = await Promise.all([fileUploadPromise, metadataUploadPromise]);
        if (!fileUploadResponse.ok) {
            console.error('Error during file upload', `Status code: ${fileUploadResponse.status}`);
        }
        if (!metadataUploadResponse.ok) {
            console.error('Error during metadata upload', `Status code: ${metadataUploadResponse.status}`)
        }
        return requestGuid;
    }

    // https://stackoverflow.com/a/40940790/14109140
    static extractFilename(disposition: string | null): string | null {
        if (!disposition || disposition.indexOf('attachment') === -1) {
            return null;
        }
        const filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
        const matches = filenameRegex.exec(disposition);
        if (matches != null && matches[1]) {
            return matches[1].replace(/['"]/, '');
        }
        return null;
    }

    async getFileAsync(fileGuid: Guid): Promise<Attachment | null> {
        const fileId = fileGuid.value;
        const response = await fetch(`${this.fileServerUrl}/api/files/${fileId}`, {
            mode: 'cors',
            method: 'GET',
        });
        if (!response.ok) {
            if (response.status === 404) {
                return null;
            }
            console.error('Error during file fetch', response)
            throw new Error('Could not find requested file. Error on server')
        }

        const json = await response.json();
        return {
            contentUrl: `${this.fileServerUrl}/api/files/${fileId}/blob`,
            name: json.filename,
        }
    }
}