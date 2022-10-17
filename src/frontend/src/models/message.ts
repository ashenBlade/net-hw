import Attachment from "./attachment";

export interface Message {
    username: string
    message: string
    attachment?: Attachment
}

export function parseMessage(json: string): Message {
    const parsed = JSON.parse(json);
    if (!(parsed.message && parsed.username)) {
        throw new Error('No message passed');
    }

    function getMessage(): string {
        if (typeof parsed.message !== 'string') {
            throw new Error('Message must be a string. Given', parsed.message);
        }
        return parsed.message;
    }


    function getUsername(): string {
        if (typeof parsed.username !== 'string') {
            throw new Error('Username must be string. Given: ', parsed.username);
        }
        return parsed.username;
    }

    function getAttachment(): Attachment | undefined {
        if (!parsed.attachment) {
            return undefined;
        }

        if (typeof parsed.attachment !== 'object')
            throw new Error('Attachment must be object or does not exist. Given: ', parsed.attachment)
        const attachment = parsed.attachment;
        if (attachment.contentUrl && typeof attachment.contentUrl !== 'string')
            throw new Error('Attachment.contentUrl must be string. Given: ', parsed.attachment)
        if (attachment.name && typeof attachment.name !== 'string')
            throw new Error('Attachment.name must be string. Given: ', parsed.attachment)
        return {
            contentUrl: attachment.contentUrl,
            name: attachment.name
        }
    }

    return {
        username: getUsername(),
        message: getMessage(),
        attachment: getAttachment()
    }
}