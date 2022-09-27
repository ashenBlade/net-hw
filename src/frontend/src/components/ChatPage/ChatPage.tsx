import React, {FC, useEffect, useState} from 'react';
import {Message} from "../../models/message";
import {ChatPageProps} from "./ChatPageProps";
import Chat from "./Chat/Chat";

const ChatPage: FC<ChatPageProps> = ({forumHandler}) => {
    const [userMessage, setUserMessage] = useState('');
    const [messageSending, setMessageSending] = useState(false);
    const [messages, setMessages] = useState<Message[]>([]);

    const onButtonClick = async () => {
        const inputMessage = userMessage.trim();
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
        function cb(msg: Message) {
            setMessages([...messages, msg]);
        }
        forumHandler.registerOnMessageCallback(cb);
        return () => {
            forumHandler.unregisterOnMessageCallback(cb);
        }
    }, [forumHandler, messages]);



    return (
        <div>
            <Chat messages={messages}/>
            {/*<textarea rows={10} value={messages} readOnly={true}/>*/}
            <input value={userMessage} minLength={3} onChange={e => setUserMessage(e.currentTarget.value)}/>
            <button disabled={messageSending} onClick={onButtonClick}>Send</button>
        </div>
    );
};

export default ChatPage;
