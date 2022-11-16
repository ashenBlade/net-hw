import React, {FC, useEffect, useReducer, useRef, useState} from 'react';
import {Message} from "../../models/message";
import {ChatPageProps} from "./ChatPageProps";
import Chat from "./Chat/Chat";
import './ChatPage.tsx.css';
import {useEffectOnce} from "../../hooks/useEffectOnce";
import ChatMessage from "./Chat/СhatMessage";
import Guid from "../../models/guid";
import {UploadFile} from "../../models/uploadFile";

const ChatPage: FC<ChatPageProps> = ({forumHandler, username, fileRepository}) => {
    const [userMessage, setUserMessage] = useState('');
    const [messageSending, setMessageSending] = useState(false);
    const [messages, setMessages] = useState<ChatMessage[]>([]);
    const inputRef = useRef<HTMLInputElement>(null);
    const fileInputRef = useRef<HTMLInputElement>(null);
    const [myUploadedFiles,] = useState<Set<string>>(new Set());
    const [, rerender] = useReducer(x => x + 1, 0);

    function promptUserInput(name: string): string {
        let value = prompt(`${name}`);
        while (!value) {
            value = prompt(`Че? Не понятно? Введи: ${name}`);
        }
        return value;
    }

    function promptUserMetadata(names: string[]): Map<string, string> {
        const map = new Map<string, string>();
        names.forEach(n => map.set(n, promptUserInput(n)))
        return map;
    }

    function promptFileMetadata(contentType: string, extension: string): Map<string, string> {
        const [baseType, concreteType] = contentType.split('/');
        let toPrompt: string[] = [];
        switch (baseType) {
            case 'image':
                toPrompt = ['Размер изображения', 'Цвет'];
                break;
            case 'video':
                toPrompt = ['Длительность', 'Актеры'];
                break;
            case 'text':
                switch (concreteType) {
                    case 'html':
                        toPrompt = ['HTML язык программирования?']
                        break;
                    case 'plain':
                        toPrompt = ['Количество слов']
                        break;
                    case 'css':
                        toPrompt = ['Количество классов']
                        break;
                }
                break;
            case 'application':
                switch (concreteType) {
                    case 'pdf':
                        toPrompt = ['Количество страниц']
                        break;
                    case 'json':
                        toPrompt = ['Массив или объект']
                        break;
                    case 'x-bittorrent':
                        toPrompt = ['Ты пират?']
                        break;
                }
        }
        return promptUserMetadata(toPrompt)
    }



    async function sendMessage() {
        async function getRequestId(): Promise<Guid | undefined> {
            if (!fileInputRef.current?.files?.[0]) {
                return undefined;
            }
            const file = fileInputRef.current.files[0];
            const metadata = await promptFileMetadata(file.type, file.name.split('.').pop() ?? file.name)
            try {
                let guid = await fileRepository.uploadFileAsync(file, metadata);
                myUploadedFiles.add(guid.value);
                return guid;
            } catch (e) {
                console.error('Error during file uploading', e);
                alert('Could not upload file');
                return undefined;
            }
        }

        const message = userMessage.trim();
        if (message.length < 3) {
            alert('Min message length is 3');
            return;
        }

        setMessageSending(true);
        try {
            const requestId = await getRequestId();
            await forumHandler.sendMessage({
                message,
                username,
                requestId
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
            username: msg.username,
            message: msg.message,
            requestId: msg.requestId,
        }
    }

    useEffect(() => {
        function onMessageCallback(msg: Message) {
            setMessages([...messages, mapMessageToChatMessage(msg)]);
        }

        function onFileUploadCallback(uploadFile: UploadFile) {
            const requiredMessage = messages.find(msg => msg.requestId === uploadFile.requestId);
            if (!requiredMessage) {
                console.warn('Could not find message for onFileUploadCallback');
                return;
            }
            if (myUploadedFiles.has(uploadFile.requestId.value)) {
                alert('Your file was successfully uploaded!')
                myUploadedFiles.delete(uploadFile.requestId.value)
            }
            requiredMessage.attachment = {
                name: 'Attachment',
                contentUrl: uploadFile.contentUrl,
                metadata: uploadFile.metadata
            }
            setMessages(messages);
            rerender();
        }

        forumHandler.registerOnMessageCallback(onMessageCallback);
        forumHandler.registerOnFileUploadedCallback(onFileUploadCallback);
        return () => {
            forumHandler.unregisterOnMessageCallback(onMessageCallback);
            forumHandler.unregisterOnFileUploadCallback(onFileUploadCallback);
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
