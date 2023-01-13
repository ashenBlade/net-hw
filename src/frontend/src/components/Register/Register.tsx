import React, {FC, useState} from 'react';
import LoginProps from "../Login/LoginProps";

const Register: FC<LoginProps> = ({authorizer, onLoginedCallback}) => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');

    function ensureInputValid() {
        if (password.length === 0) {
            throw new Error('Пароль пустой')
        }
        if (username.length === 0) {
            throw new Error("Имя пользователя пустое")
        }
        if (confirmPassword !== password) {
            throw new Error('Пароли не совпадают')
        }
    }

    async function loginAsync() {
        ensureInputValid();
        const jwt = await authorizer.registerAsync(username, password);
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
            <label>
                Повторите пароль
                <input value={confirmPassword} type='password' onChange={x => setConfirmPassword(x.currentTarget.value)}/>
            </label>
            <button type={'button'} onClick={loginAsync}>
                Зарегистрироваться
            </button>
        </form>
    </div>)
};

export default Register;
