import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './components/App/App';
import reportWebVitals from './reportWebVitals';
import {SignalrForumCommunicator} from "./services/signalrForumCommunicator";
import {MessagesApiMessagesRepository} from "./services/messagesApiMessagesRepository";
import {AggregatedForumHandler} from "./services/aggregatedForumHandler";
import FileApiFileRepository from "./services/fileApiFileRepository";


const serverUrl = process.env.REACT_APP_SERVER_URL;

if (!serverUrl) {
    throw new Error('REACT_APP_SERVER_URL env variable is not provided');
}

const communicator = new SignalrForumCommunicator(serverUrl);
const messagesRepository = new MessagesApiMessagesRepository(serverUrl);
const forumHandler = new AggregatedForumHandler(messagesRepository, communicator)

window.onunload = () => {
    communicator.close()
}

window.onload = () => {
    communicator.open()
}

const fileServerUrl = process.env.REACT_APP_FILE_SERVER_URL;
if (!fileServerUrl) {
    throw new Error('REACT_APP_FILE_SERVER_URL env variable is not provided')
}

const fileRepository = new FileApiFileRepository(fileServerUrl);

const root = ReactDOM.createRoot(
    document.getElementById('root') as HTMLElement
);

root.render(
    <React.StrictMode>
        <App forumHandler={forumHandler}
             fileRepository={fileRepository}/>
    </React.StrictMode>
);


// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
