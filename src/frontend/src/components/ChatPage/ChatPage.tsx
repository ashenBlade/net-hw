import React, {FC, useEffect, useRef, useState} from 'react';
import {Message} from "../../models/message";
import {ChatPageProps} from "./ChatPageProps";
import Chat from "./Chat/Chat";
import './ChatPage.tsx.css';
import {useEffectOnce} from "../../hooks/useEffectOnce";

const ChatPage: FC<ChatPageProps> = ({forumHandler, username}) => {
    const [userMessage, setUserMessage] = useState('');
    const [messageSending, setMessageSending] = useState(false);
    const [messages, setMessages] = useState<Message[]>([]);
    const inputRef = useRef<HTMLInputElement>(null);

    async function sendMessage() {
        const inputMessage = userMessage.trim();
        if (inputMessage.length < 3) {
            alert('Min message length is 3');
            return;
        }

        setMessageSending(true);
        try {
            await forumHandler.sendMessage({message: inputMessage, username: username});
            setUserMessage('');
        } finally {
            setMessageSending(false);
        }
    }

    const onSendMessageButtonClick = async () => {
        try {
            await sendMessage();
        } finally {
            inputRef.current?.focus();
        }
    };

    const onInputKeyDown = async (event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === 'Enter') {
            try {
                await sendMessage();
            } finally {
                inputRef.current?.focus();
            }
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

    useEffectOnce(() => {
        setMessageSending(true)
        forumHandler.getPreviousMessages(1, 15).then(received => {
            setMessages([...received, ...messages]);
        }).finally(() => {
            setMessageSending(false);
        })
    })

    return (
        <div className={'h-100 chat-page'}>
            <Chat messages={messages}/>
            <input className={'form-control my-2'}
                   placeholder={'Введите сообщение другим участникам'}
                   value={userMessage}
                   onChange={e => setUserMessage(e.currentTarget.value)}
                   onKeyDown={onInputKeyDown}
                   autoFocus={true}
                   disabled={messageSending}
                   ref={inputRef}/>
            <button className={'btn btn-success mb-1'}
                    disabled={messageSending}
                    onClick={onSendMessageButtonClick}>
                Отправить
            </button>
        </div>
    );
};

export default ChatPage;
