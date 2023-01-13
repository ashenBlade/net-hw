import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './components/App/App';
import reportWebVitals from './reportWebVitals';
import BackendAuthorizer from "./services/backendAuthorizer";
import SignalRGameCommunicator from "./services/signalRGameCommunicator";
import BackendGamesRepository from "./services/backendGamesRepository";

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

const authorizer = new BackendAuthorizer('http://localhost:8081');

root.render(
  <React.StrictMode>
    <App authorizer={authorizer}/>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
