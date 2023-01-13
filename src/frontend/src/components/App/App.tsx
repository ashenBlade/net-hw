import React, {FC, useState} from 'react';
import './App.css';
import AppProps from "./AppProps";
import {Login} from "../Login/Login";
import Register from "../Register/Register";
import Game from "../Game/Game";

const App: FC<AppProps> = ({authorizer, communicator, repository}) => {
  const [isAuthorized, setIsAuthorized] = useState(authorizer.getJwt() !== null);
  const [useLogin, setUseLogin] = useState(true);
  return (
      <>
        {isAuthorized
            ? <>
                <Game authorizer={authorizer} gameCommunicator={communicator} gamesRepository={repository}/>
            </>
            : <>
              {
                useLogin
                    ? <>
                      <Login authorizer={authorizer} onLoginedCallback={jwt => {
                        authorizer.setJwt(jwt);
                        setIsAuthorized(true)
                      }}/>
                      <button type={'button'} onClick={() => setUseLogin(false)}>
                        Зарегистрироваться
                      </button>
                    </>
                    : <>
                      <Register onLoginedCallback={jwt => {
                        authorizer.setJwt(jwt);
                        setIsAuthorized(true)
                      }} authorizer={authorizer}/>
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
