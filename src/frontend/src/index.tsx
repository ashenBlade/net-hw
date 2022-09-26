import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './components/App/App';
import reportWebVitals from './reportWebVitals';
import {StubForumHandler} from "./services/stubForumHandler";

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

// const tls = window.location.protocol === "https:";
// const url = `${tls ? "wss" : "ws"}://localhost:15670`;
// const amqp = new AMQPWebSocketClient(url, '/', 'guest', 'guest')

root.render(
  <React.StrictMode>
    <App forumHandler={new StubForumHandler()}/>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
