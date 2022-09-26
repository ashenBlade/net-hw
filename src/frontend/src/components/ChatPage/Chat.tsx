import React, {FC, useEffect, useState} from 'react';
import {Message} from "../../models/message";
import {ChatProps} from "./ChatProps";

const Chat: FC<ChatProps> = ({forumHandler}) => {
    const [message, setMessage] = useState('');
    const [messageSending, setMessageSending] = useState(false);
    const [forum, setForum] = useState('');

    const onButtonClick = async () => {
        const inputMessage = message.trim();
        if (inputMessage.length < 3) {
            alert('Min message length is 3');
            return;
        }

        setMessageSending(true);
        try {
            await forumHandler.sendMessage({message: inputMessage});
        } finally {
            setMessageSending(false);
        }
    }

    useEffect(() => {
        forumHandler.registerOnMessageCallback((msg: Message) => {
            setForum(forum + `${msg.username ?? 'Unknown'}: ${msg.message}\n`)
        })
    }, [forumHandler, forum]);

    return (
        <div>
            <textarea rows={10} value={forum} readOnly={true}/>
            <input value={message} onChange={e => setMessage(e.currentTarget.value)}/>
            <button disabled={messageSending} onClick={onButtonClick}>Send</button>
        </div>
    );
};

export default Chat;
