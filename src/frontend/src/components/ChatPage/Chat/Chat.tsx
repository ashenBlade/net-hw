import React, {FC} from 'react';
import {ChatProps} from "./ChatProps";
import ChatMessage from "./СhatMessage";

function createMessageRecord(message: ChatMessage, i: number) {
    const name = (
        <b>
            {message.username ?? 'Unknown'}
        </b>
    )

    const contents = message.message;

    return (
        <div key={i}>
            <div>{name}: {contents}</div>
            {message.attachment && (<a download={message.attachment.downloadUrl}>{message.attachment.filename}</a> )}
        </div>
    )
}

const Chat: FC<ChatProps> = ({messages}) => {
    return (
        <div className={'h-100'}>
            {messages.map((m, i) => createMessageRecord(m, i))}
        </div>
    );
};

export default Chat;
