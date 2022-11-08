import React, {FC, useEffect, useRef, useState} from 'react';
import {Message} from "../../models/message";
import {ChatPageProps} from "./ChatPageProps";
import Chat from "./Chat/Chat";
import './ChatPage.tsx.css';
import {useEffectOnce} from "../../hooks/useEffectOnce";
import ChatMessage from "./Chat/СhatMessage";
import Attachment from "../../models/attachment";

const ChatPage: FC<ChatPageProps> = ({forumHandler, username, fileRepository}) => {
    const [userMessage, setUserMessage] = useState('');
    const [messageSending, setMessageSending] = useState(false);
    const [messages, setMessages] = useState<ChatMessage[]>([]);
    const inputRef = useRef<HTMLInputElement>(null);
    const fileInputRef = useRef<HTMLInputElement>(null);

    async function sendMessage() {
        async function getAttachment(): Promise<Attachment | undefined> {
            if (fileInputRef.current?.files?.[0]) {
                const file = fileInputRef.current.files[0];
                try {
                    return await fileRepository.uploadFileAsync(file)

                } catch (e) {
                    console.error('Error during file uploading', e);
                    alert('Could not upload file');
                }
            }
            return undefined;
        }

        const message = userMessage.trim();
        if (message.length < 3) {
            alert('Min message length is 3');
            return;
        }

        setMessageSending(true);
        try {
            const attachment = await getAttachment();
            await forumHandler.sendMessage({
                message,
                username,
                attachment
            });
            setUserMessage('');
            if (fileInputRef.current) {
                fileInputRef.current.value = '';
            }
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

    function mapMessageToChatMessage(msg: Message): ChatMessage {
        return {
            attachment: msg.attachment ? ({
                downloadUrl: msg.attachment.contentUrl,
                filename: msg.attachment.name
            }) : undefined,
            username: msg.username,
            message: msg.message
        }
    }

    useEffect(() => {
        function cb(msg: Message) {
            setMessages([...messages, mapMessageToChatMessage(msg)]);
        }
        forumHandler.registerOnMessageCallback(cb);
        return () => {
            forumHandler.unregisterOnMessageCallback(cb);
        }
    }, [forumHandler, messages]);

    useEffectOnce(() => {
        setMessageSending(true)
        forumHandler.getPreviousMessages(1, 40).then(received => {
            setMessages([...received.map(mapMessageToChatMessage), ...messages]);
        }).catch(e => {
            console.error('Error while retrieving message history', e);
        }).finally(() => {
            setMessageSending(false);
        })
    })

    return (
        <div className={'h-100 chat-page'}>
            <Chat messages={messages}/>
            <div className={'user-input'}>
                <input className={'form-control'}
                       placeholder={'Введите сообщение другим участникам'}
                       value={userMessage}
                       onChange={e => setUserMessage(e.currentTarget.value)}
                       onKeyDown={onInputKeyDown}
                       autoFocus={true}
                       disabled={messageSending}
                       ref={inputRef}/>
                <input type={'file'}
                       className={'form-control mt-1'}
                       ref={fileInputRef}/>
            </div>
            <button className={'btn btn-success my-1'}
                    disabled={messageSending}
                    onClick={onSendMessageButtonClick}>
                Отправить
            </button>
        </div>
    );
};

export default ChatPage;
