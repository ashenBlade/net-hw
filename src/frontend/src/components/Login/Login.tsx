import {FC, useState} from "react";
import LoginProps from "./LoginProps";

export const Login: FC<LoginProps> = ({authorizer, onLoginedCallback}) => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    function ensureInputValid() {
        if (password.length === 0) {
            throw new Error('Пароль пустой')
        }
        if (username.length === 0) {
            throw new Error("Имя пользователя пустое")
        }
    }

    async function loginAsync() {
        ensureInputValid();
        const jwt = await authorizer.loginAsync(username, password);
        onLoginedCallback(jwt);
    }

    return (<div>
        <form>
            <label>
                Ваше имя
                <input value={username} type='text' onChange={x => setUsername(x.currentTarget.value)}/>
            </label>
            <label>
                Пароль
                <input value={password} type='password' onChange={x => setPassword(x.currentTarget.value)}/>
            </label>
            <button type={'button'} onClick={loginAsync}>Войти</button>
        </form>
    </div>)
}