export interface Message {
    username: string
    message: string
    attachment?: string
}

export function parseMessage(json: string): Message {
    console.log({
        json
    })
    const parsed = JSON.parse(json);
    if (!(parsed.message && parsed.username)) {
        throw new Error('No message passed');
    }
    if (typeof parsed.message !== 'string') {
        throw new Error('Message must be a string. Given', parsed.message);
    }

    if (typeof parsed.username !== 'string') {
        throw new Error('Username must be string. Given: ', parsed.username);
    }

    if (parsed.attachment && typeof parsed.attachment !== 'string') {
        throw new Error('Attachment must be string or does not exist. Given: ', parsed.attachment)
    }

    return {
        username: parsed.username,
        message: parsed.message,
        attachment: parsed.attachment
    }
}