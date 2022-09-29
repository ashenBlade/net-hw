export interface Message {
    username: string
    message: string
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

    return {
        username: parsed.username,
        message: parsed.message
    }
}