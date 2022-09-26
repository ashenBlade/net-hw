import React, {FC, useState} from 'react';
import './App.css';
import {ForumHandler} from "../../services/forumHandler";

export interface AppProps {
  forumHandler: ForumHandler
}

const App: FC<AppProps> = ({forumHandler}: AppProps) => {
  const [message, setMessage] = useState('');
  const [messageSending, setMessageSending] = useState(false);
  const [forum, setForum] = useState('');

  forumHandler.registerOnMessageCallback((msg) => {
    setForum(forum + `${msg.username ?? 'Unknown'}: ${msg.message}\n`)
  });

  const onButtonClick = async () => {
    const inputMessage = message.trim();
    if (inputMessage.length < 3) {
      alert('Min message length is 3');
      return;
    }
    setMessageSending(true);
    try {
      await forumHandler.sendMessage({message: inputMessage});
    } finally {
      setMessageSending(false);
    }
  }

  return (
    <div>
      <form>
        <textarea rows={10} value={forum} readOnly={true}/>
        <input value={message} onChange={e => setMessage(e.currentTarget.value)}/>
        <button disabled={messageSending} onClick={onButtonClick}>Send</button>
      </form>
    </div>
  );
};

export default App;
