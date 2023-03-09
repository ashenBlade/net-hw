import React, {useState} from 'react';
import {Button, FormGroup, Input, InputLabel} from "@mui/material";
import axios from "axios";
import {useNavigate} from "react-router-dom";

interface RegistrationState {
    userName: string;
    email: string;
    password: string;
}
export const RegistrationPage = () => {

    const [registrationState, setRegistrationState] = useState<RegistrationState>({
        userName: '',
        email: '',
        password: ''
    });

    const navigate = useNavigate();

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const {name, value} = event.target;
        setRegistrationState((prevState) => ({...prevState, [name]: value}));
    };

    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();
        try {
            if (registrationState.userName.length < 6)
            {
                alert("Username must be at least 6 characters long");
                return;
            }
            if (registrationState.password.length < 6)
            {
                alert("Password must be at least 6 characters long");
                return;
            }
            await axios.post(process.env.REACT_APP_ORIGIN_URL + '/api/auth/users', registrationState);
            alert("You successfully registered!");
            navigate(`/authorization`, {replace: true});
        } catch (error: any) {
            const errors = error.response?.data?.errors;
            if (Array.isArray(errors) && errors.length > 0){
                alert(errors[0]);
                return;
            }
            else{
                alert("Server error!");
            }
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
                            value={registrationState.userName}
                            onChange={handleChange}
                            id="username"
                            
                            required
                        />

                        <InputLabel htmlFor="email">Email</InputLabel>
                        <Input
                            type="email"
                            name="email"
                            value={registrationState.email}
                            onChange={handleChange}
                            id="email"
                            required
                        />

                        <InputLabel htmlFor="password">Password</InputLabel>
                        <Input
                            type="password"
                            name="password"
                            value={registrationState.password}
                            onChange={handleChange}
                            id="password"
                            required
                        />
                    </FormGroup>

                    <Button type="submit" fullWidth={true}>Submit</Button>
                
                </form>

                <Button onClick={() => {
                    navigate(`/authorization`, {replace: true})
                }} fullWidth={true} style={{marginTop: "2rem"}}
                        color={"secondary"}>
                    Authorize
                </Button>
            </div>
        </div>
    );
};
