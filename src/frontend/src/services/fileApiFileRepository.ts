import FileRepository from "../interfaces/fileRepository";
import Attachment from "../models/attachment";

export default class FileApiFileRepository implements FileRepository {
    constructor(readonly fileServerUrl: string) { }

    async addFileAsync(file: File): Promise<Attachment> {
        const form = new FormData();
        form.set('file', file);
        const response = await fetch(`${this.fileServerUrl}/api/files`, {
            method: 'POST',
            body: form,
            mode: 'cors',
        });
        if (!response.ok) {
            console.error('Error during saving file', {
                statusCode: response.status
            })
            throw new Error('Could not create new file')
        }
        const json = await response.json();
        const fileId = json.fileId;
        const contentUrl = `${this.fileServerUrl}/api/files/${fileId}/blob`;
        return {
            fileId,
            contentUrl,
            contentType: json.contentType,
            name: json.filename
        }
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

    async getFileAsync(fileId: string): Promise<File | null> {
        const response = await fetch(`${this.fileServerUrl}/api/files/${fileId}/blob`, {
            mode: 'cors',
            method: 'GET'
        });
        if (!response.ok) {
            if (response.status === 404) {
                return null;
            }
            console.error('Error during file fetch', response)
            throw new Error('Could not find requested file. Error on server')
        }

        const filename = FileApiFileRepository.extractFilename(response.headers.get('content-disposition')) ?? 'user-file';
        const contentType = response.headers.get('content-type') ?? undefined;
        return new File([await response.blob()], filename, {type: contentType});
    }
}