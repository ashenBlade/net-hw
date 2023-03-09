import React, {useState} from 'react';
import axios from "axios";
import {Button, FormGroup, Input, InputLabel} from "@mui/material";
import {useNavigate} from "react-router-dom";


interface AuthorizationState {
    userName: string;
    password: string;
}

export const AuthorizationPage = () => {
    const [authorizationState, setAuthorizationState] = useState<AuthorizationState>({
        userName: '',
        password: ''
    });

    const navigate = useNavigate();

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const {name, value} = event.target;
        setAuthorizationState((prevState) => ({...prevState, [name]: value}));
    };

    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();

        if (!authorizationState.userName || !authorizationState.password) {
            alert("All fields are required");
            return;
        }
        try {
            const response = await axios.post(process.env.REACT_APP_ORIGIN_URL + '/api/auth/token', authorizationState);
            if (response.status === 200) {
                alert("You successfully logged in!");
                localStorage.setItem("access_token", response.data.accessToken);
                navigate(`/chat`, {replace: true});
            } else {
                alert("Server error!")
            }

        } catch (error: any) {
            alert("Invalid data");
        }
    };

    return (
        <div className={"container"}>
            <div>
                <form onSubmit={handleSubmit} className={"registration-form"}>
                    <FormGroup className={"form-group-inputs"}>
                        <InputLabel htmlFor="userName">Username</InputLabel>
                        <Input
                            type="text"
                            name="userName"
                            value={authorizationState.userName}
                            onChange={handleChange}
                            id="username"
                            required
                        />

                        <InputLabel htmlFor="password">Password</InputLabel>
                        <Input
                            type="password"
                            name="password"
                            value={authorizationState.password}
                            onChange={handleChange}
                            id="password"
                            required
                        />
                    </FormGroup>

                    <Button type="submit" fullWidth={true}>Submit</Button>
                </form>

                <Button onClick={() => {
                    navigate(`/registration`, {replace: true})
                }} fullWidth={true} style={{marginTop: "2rem"}}
                        color={"secondary"}>
                    Register
                </Button>
            </div>
        </div>
    );
};
