export default interface FileRepository {
    getFileAsync(fileId: string): Promise<File | null>

    // Returns created file content url
    addFileAsync(file: File): Promise<string>
}