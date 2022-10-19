import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import reportWebVitals from './reportWebVitals';
import AppWrapper from "./components/App/AppWrapper";


const fileServerUrl = process.env.REACT_APP_FILE_SERVER_URL;
const serverUrl = process.env.REACT_APP_SERVER_URL;
if (!fileServerUrl) {
    throw new Error('REACT_APP_FILE_SERVER_URL env variable is not provided')
}
if (!serverUrl) {
    throw new Error('REACT_APP_SERVER_URL env variable is not provided');
}

const root = ReactDOM.createRoot(
    document.getElementById('root') as HTMLElement
);

root.render(
    <React.StrictMode>
        <AppWrapper serverUrl={serverUrl} fileServerUrl={fileServerUrl}/>
    </React.StrictMode>
);


// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
