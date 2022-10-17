export default interface ChatMessage {
    username?: string
    message: string
    attachment?: {
        downloadUrl: string
        filename: string
    }
}