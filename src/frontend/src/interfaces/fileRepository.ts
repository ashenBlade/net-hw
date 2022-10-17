export default interface FileRepository {
    getFileAsync(fileId: string): Promise<File | null>

    // Returns created file id
    addFileAsync(file: File): Promise<string>
}