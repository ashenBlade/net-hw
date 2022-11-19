import React, {FC} from 'react';
import {ChatProps} from "./ChatProps";
import ChatMessage from "./СhatMessage";
import Attachment from "../../../models/attachment";
import Guid from "../../../models/guid";

function createMessageRecord(message: ChatMessage, i: number, files: Map<string, Attachment>) {
    const name = (
        <b>
            {message.username ?? 'Unknown'}
        </b>
    )

    const contents = message.message;
    
    const attachment = message.requestId 
        ? files.get(message.requestId.value)
        : undefined;
    return (
        <div key={i}>
            <div>{name}: {contents}</div>
            {
                attachment ? (
                    <>
                        <a download 
                           href={attachment.contentUrl} 
                           target={'_blank'}>
                            {attachment.name}
                        </a>
                        <button className={'btn btn-info'} onClick={() => {
                            alert(`Metadata:\n${
                                attachment && 
                                Array.from(attachment.metadata.entries())
                                    .map(([x, y]) => `${x}: ${y}`)
                                    .join('\n')
                            }`)
                        }}>
                            Show metadata
                        </button>
                    </>
                )
                    : null
            }
        </div>
    )
}

const Chat: FC<ChatProps> = ({messages, files}) => {
    return (
        <div className={'h-100'}>
            {messages.map((m, i) => createMessageRecord(m, i, files))}
        </div>
    );
};

export default Chat;
