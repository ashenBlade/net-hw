import React from 'react';
import {Button} from "@mui/material";
import {useNavigate} from "react-router-dom";

export const HomePage = () => {

    const navigate = useNavigate();
    
    return (
        <div className={"container"}>
            <div>
            <Button onClick={() => {navigate(`/authorization`, {replace: true})}} fullWidth={true} style={{marginTop: "2rem"}}
                    color={"secondary"}>
                Authorize
            </Button>

            <Button onClick={() => {navigate(`/registration`, {replace: true})}} fullWidth={true} style={{marginTop: "2rem"}}
                    color={"secondary"}>
                Register
            </Button>
            </div>
        </div>
    );
};
