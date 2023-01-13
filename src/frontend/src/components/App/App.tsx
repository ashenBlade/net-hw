import React, {FC, useEffect, useState} from 'react';
import './App.css';
import AppProps from "./AppProps";
import {Login} from "../Login/Login";
import Register from "../Register/Register";
import {GamePage} from "../Game/GamePage";
import BackendGamesRepository from "../../services/backendGamesRepository";
import IGameCommunicator from "../../interfaces/IGameCommunicator";
import IGamesRepository from "../../interfaces/iGamesRepository";
import SignalRGameCommunicator from "../../services/signalRGameCommunicator";

const App: FC<AppProps> = ({authorizer}) => {
  const [isAuthorized, setIsAuthorized] = useState(authorizer.getJwt() !== null);
  const [communicator, setCommunicator] = useState<SignalRGameCommunicator>();
  const [repo, setRepo] = useState<BackendGamesRepository>();
  const [useLogin, setUseLogin] = useState(true);
  const onLogin = (jwt: string) => {
      const cum = new SignalRGameCommunicator('http://localhost:8081', jwt, '/game');
      const r = new BackendGamesRepository('http://localhost:8081', jwt);
      setCommunicator(cum);
      setRepo(r);
      cum.open().then(() => {
          setIsAuthorized(true)
      }).catch(() => setIsAuthorized(false))
  }
  
  useEffect(() => {
      return () => {
          if (communicator) {
              communicator.stop()
          }
      }
  }, [])
  return (
      <>
        {isAuthorized
            ? <>
                <GamePage authorizer={authorizer} gamesRepository={repo!} gameCommunicator={communicator!}/>
            </>
            : <>
              {
                useLogin
                    ? <>
                      <Login authorizer={authorizer} onLoginedCallback={onLogin}/>
                      <button type={'button'} onClick={() => setUseLogin(false)}>
                        Зарегистрироваться
                      </button>
                    </>
                    : <>
                      <Register onLoginedCallback={onLogin} authorizer={authorizer}/>
                      <button type={'button'} onClick={() => setUseLogin(true)}>
                        Уже есть аккаунт? Войти
                      </button>
                    </>
              }
            </>
        }
      </>
  )
};

export default App;
