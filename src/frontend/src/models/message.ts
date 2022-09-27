export interface Message {
    username?: string
    message: string
}

export function parseMessage(json: string): Message {
    const parsed = JSON.parse(json);
    if (parsed.message === undefined || parsed.message === null) {
        throw new Error('No message passed');
    }
    if (typeof parsed.message !== 'string') {
        throw new Error('Message must be a string. Given', parsed.message);
    }

    if (parsed.username !== undefined && typeof parsed.username !== 'string') {
        throw new Error('Username must be string or undefined');
    }

    return {
        username: parsed.username,
        message: parsed.message
    }
}