import FileRepository from "../interfaces/fileRepository";

export default class StubFileRepository implements FileRepository {
    async getFileAsync(fileId: string): Promise<File | null> {
        return null;
    }

    addFileAsync(file: File): Promise<string> {
        return Promise.resolve("");
    }

}