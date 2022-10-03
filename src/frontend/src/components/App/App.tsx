import React, {useState} from 'react';
import './App.css';
import ChatPage from "../ChatPage/ChatPage";
import {useEffectOnce} from "../../hooks/useEffectOnce";
import {MessagesApiMessagesRepository} from "../../services/messagesApiMessagesRepository";
import {AggregatedForumHandler} from "../../services/aggregatedForumHandler";
import {SignalrForumCommunicator} from "../../services/signalrForumCommunicator";

const App = () => {
    const url = process.env.REACT_APP_SERVER_URL;
    if (!url) {
        throw new Error('Server url is not provided');
    }
    const [communicator,] = useState(new SignalrForumCommunicator(`${url}/chat`));
    const [messagesRepository,] = useState(new MessagesApiMessagesRepository(url));
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
