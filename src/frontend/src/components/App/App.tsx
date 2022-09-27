import React, {useState} from 'react';
import './App.css';
import {RabbitmqForumHandler} from "../../services/rabbitmqForumHandler";
import ChatPage from "../ChatPage/ChatPage";
import {useEffectOnce} from "../../hooks/useEffectOnce";

const App = () => {
    const tls = window.location.protocol === "https:";
    const url = `${tls ? "wss" : "ws"}://localhost:15670`;
    const [forumHandler,] = useState(new RabbitmqForumHandler(url));

    useEffectOnce(() => {
        forumHandler.open()
        return () => {
            forumHandler.close()
        }
    })

    return (
        <ChatPage forumHandler={forumHandler}/>
    );
};

export default App;
