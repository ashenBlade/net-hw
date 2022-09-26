import React, {useEffect, useState} from 'react';
import './App.css';
import {RabbitmqForumHandler} from "../../services/rabbitmqForumHandler";
import Chat from "../ChatPage/Chat";

const App = () => {
    const tls = window.location.protocol === "https:";
    const url = `${tls ? "wss" : "ws"}://localhost:15670`;
    const [forumHandler,] = useState(new RabbitmqForumHandler(url));
    useEffect(() => {
        forumHandler.open()
        return () => {
            forumHandler.close()
        };
    }, [forumHandler]);

    return (
        <Chat forumHandler={forumHandler}/>
    );
};

export default App;
