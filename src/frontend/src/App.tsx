import React, {useEffect, useRef} from 'react';
import logo from './logo.svg';
import './App.css';
import {AMQPChannel, AMQPWebSocketClient} from "@cloudamqp/amqp-client";

function App() {

  const tls = window.location.protocol === "https:";
  const url = `${tls ? "wss" : "ws"}://localhost:15670`
  const amqp = new AMQPWebSocketClient(url, '/', 'guest', 'guest')

  const input = useRef<HTMLInputElement>(null);
  const textarea = useRef<HTMLTextAreaElement>(null);

  async function start() {
    try {
      const conn = await amqp.connect()
      const ch = await conn.channel()
      attachPublish(ch)
      const q = await ch.queue("");
      await q.bind("amq.fanout");
      const consumer = await q.subscribe({noAck: false}, (msg) => {
        console.log(msg)

        if (textarea.current) {
          textarea.current.value += msg.bodyToString() + "\n";
        }
        msg.ack()
      })
    } catch (err) {
      console.error("Error", err, "reconnecting in 1s")
      disablePublish()
      setTimeout(start, 1000)
    }
  }

  function attachPublish(ch: AMQPChannel) {
    document.forms[0].onsubmit = async (e) => {
      e.preventDefault()
      try {
        await ch.basicPublish("amq.fanout",
            "",
            input.current?.value ?? '',
            { contentType: "text/plain" })
      } catch (err) {
        console.error("Error", err, "reconnecting in 1s")
        disablePublish()
        setTimeout(start, 1000)
      }

    }
  }

  function disablePublish() {
    document.forms[0].onsubmit = (e) => { alert("Disconnected, waiting to be reconnected"); }
  }

  useEffect(() => {
    start();
  }, []);


  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
        </p>
        <form>
          <textarea rows={10} ref={textarea} value={''}/>
          <input ref={input}/>
          <button type={'submit'}>Send</button>
        </form>
      </header>
    </div>
  );
}

export default App;
