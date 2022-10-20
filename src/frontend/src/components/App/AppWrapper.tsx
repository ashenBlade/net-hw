import React, {FC, useState} from 'react';
import FileApiFileRepository from "../../services/fileApiFileRepository";
import {SignalrForumCommunicator} from "../../services/signalrForumCommunicator";
import {MessagesApiMessagesRepository} from "../../services/messagesApiMessagesRepository";
import {AggregatedForumHandler} from "../../services/aggregatedForumHandler";
import App from "./App";
import {useEffectOnce} from "../../hooks/useEffectOnce";

interface AppWrapperProps {
    serverUrl: string
    fileServerUrl: string
}

const AppWrapper: FC<AppWrapperProps> = ({serverUrl, fileServerUrl}) => {
    const [fileRepository,] = useState(new FileApiFileRepository(fileServerUrl));
    const [communicator,] = useState(new SignalrForumCommunicator(serverUrl, fileRepository));
    const [messagesRepository,] = useState(new MessagesApiMessagesRepository(serverUrl, fileRepository));
    const [forumHandler,] = useState(new AggregatedForumHandler(messagesRepository, communicator));
    
    useEffectOnce(() => {
        communicator.open()
        return () => {
            communicator.close()
        }
    })
    
    return (
        <App forumHandler={forumHandler} fileRepository={fileRepository}/>
    );
};

export default AppWrapper;