import React, {useState} from 'react';
import './App.css';
import {RabbitmqForumCommunicator} from "../../services/rabbitmqForumCommunicator";
import ChatPage from "../ChatPage/ChatPage";
import {useEffectOnce} from "../../hooks/useEffectOnce";
import {MessagesApiMessagesRepository} from "../../services/messagesApiMessagesRepository";
import {AggregatedForumHandler} from "../../services/aggregatedForumHandler";

const App = () => {
    const tls = window.location.protocol === "https:";
    const url = `${tls ? "wss" : "ws"}://localhost:15670`;
    const [communicator,] = useState(new RabbitmqForumCommunicator(url));
    const [messagesRepository,] = useState(new MessagesApiMessagesRepository('http://localhost:8081'));
    const [forumHandler,] = useState(new AggregatedForumHandler(messagesRepository, communicator))

    useEffectOnce(() => {
        communicator.open()
        return () => {
            communicator.close()
        }
    });

    const [username, setUsername] = useState('');

    const minNameLength = 5;

    useEffectOnce(() => {
        let name: string | null = null;
        let message = `Введите ваше имя (Минимум ${minNameLength} символов)`;
        while (!name || name.length < minNameLength) {
            name = window.prompt(message);
            if (name) {
                name = name.trim();
            }
            message = `Кому не понятно? Минимальная длина - ${minNameLength}. Еще раз!`
        }

        setUsername(name!);
    })

    return (
        <div className={'container-lg h-100'}>
            <ChatPage forumHandler={forumHandler}
                      username={username} />
        </div>
    );
};

export default App;
