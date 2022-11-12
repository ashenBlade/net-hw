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
            {
                message.attachment && (
                    <>
                        <a download={message.attachment.name} href={message.attachment.contentUrl}>
                            {message.attachment.name}
                        </a>
                        <button className={'btn btn-info'} onClick={() => {
                            alert(`Metadata:\n${
                                message.attachment && 
                                Array.from(message.attachment.metadata.entries())
                                    .map(([x, y]) => `${x}: ${y}`)
                                    .join('\n')
                            }`)
                        }}>

                        </button>
                    </>
                )
            }
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
