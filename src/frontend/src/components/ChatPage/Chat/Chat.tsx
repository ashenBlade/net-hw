import React, {FC} from 'react';
import {Message} from "../../../models/message";

interface ChatProps {
    messages: Message[]
}

function createMessageRecord(message: Message) {
    const name = (<b>
        {message.username ?? 'Unknown'}
    </b>)

    const contents = message.message;

    return (<div>
        {name}: {contents}
    </div>)
}

const Chat: FC<ChatProps> = ({messages}) => {
    return (
        <div>
            {messages.map(m => createMessageRecord(m))}
        </div>
    );
};

export default Chat;
