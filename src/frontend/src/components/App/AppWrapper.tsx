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
    fileMetadataServerUrl: string
}

const AppWrapper: FC<AppWrapperProps> = ({serverUrl, fileServerUrl, fileMetadataServerUrl}) => {
    const [fileRepository,] = useState(new FileApiFileRepository(fileServerUrl, fileMetadataServerUrl));
    const [communicator,] = useState(new SignalrForumCommunicator(serverUrl, fileRepository));
    const [messagesRepository,] = useState(new MessagesApiMessagesRepository(serverUrl));
    const [forumHandler,] = useState(new AggregatedForumHandler(messagesRepository, communicator));
    const [connectionEstablished, setConnectionEstablished] = useState(false);
    
    useEffectOnce( () => {
        communicator.open().then(() => setConnectionEstablished(true))
        return () => {
            communicator.close().then(() => setConnectionEstablished(false))
        }
    })
    
    
    return (
        connectionEstablished 
            ? <App forumHandler={forumHandler} fileRepository={fileRepository}/>
            : <div>Соединение устанавливается</div> 
    );
};

export default AppWrapper;