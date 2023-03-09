import React, {useEffect, useRef, useState} from 'react';
import {useNavigate} from "react-router-dom";
import {GrpcWebFetchTransport} from "@protobuf-ts/grpcweb-transport";
import {ChatServiceClient} from "../generated/chat.client";
import {ChatMessageResponse, SendMessageRequest} from "../generated/chat";
export const ChatPage = () => {

    const [chatClientService, setChatClientService] = useState<ChatServiceClient>();
    const [messages, setMessages] = useState<ChatMessageResponse[]>([]);
    const inputMessage = useRef<HTMLInputElement>(null);

    const meta = {
        Authorization: `Bearer ${localStorage.getItem("access_token") as string}`
    }

    const receiveMessages = async () => {
        const stream = chatClientService!.getChatMessages({}, {meta: meta});

        for await (let message of stream.responses) {
            setMessages(m => [...m, message])
        }
    }

    const onSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const request = {message: inputMessage.current?.value || "empty"} as SendMessageRequest;
        await chatClientService!.sendMessage(request, {meta: meta})

        if (inputMessage.current) {
            inputMessage.current.value = "";
        }
    };

    const navigate = useNavigate();

    useEffect(() => {
        let jwtToken = localStorage.getItem("access_token") as string;
        if (jwtToken) {
            const transport = new GrpcWebFetchTransport({
                baseUrl: 'http://localhost:8081',
            });
            const cl = new ChatServiceClient(transport);
            setChatClientService(cl);

        } else {
            navigate(`/`, {replace: true});
        }

    }, [])

    useEffect(() => {
        if (chatClientService) {
            receiveMessages().then(() => {});
        }

    }, [chatClientService])

    return (
        <div className={"container"}>
            <div>
                {messages.map((m, index) => <div key={index}>
                    {m.userName} {m.message}
                </div>)}
                <form onSubmit={onSubmit}>
                    <input placeholder={"Message"} ref={inputMessage}/>
                    <button>Send</button>
                </form>
            </div>
        </div>
    );
};
