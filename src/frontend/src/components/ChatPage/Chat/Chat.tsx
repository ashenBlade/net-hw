import React, {FC} from 'react';
import {Message} from "../../../models/message";

interface ChatProps {
    messages: Message[]
}

function createMessageRecord(message: Message, i: number) {
    const name = (<b>
        {message.username ?? 'Unknown'}
    </b>)

    const contents = message.message;

    return (<div key={i}>
        {name}: {contents}
    </div>)
}

const Chat: FC<ChatProps> = ({messages}) => {
    return (
        <div className={'h-100'}>
            {messages.map((m, i) => createMessageRecord(m, i))}
        </div>
    );
};

export default Chat;
