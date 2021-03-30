import React, { useState } from 'react';
import img from '../../../assets/img/auth-ico.svg';
import './AuthButton.css';
import { Link, useHistory } from 'react-router-dom';
import isAuthState from '../../../recoilStates/isAuthState';
import { useRecoilState } from 'recoil';
import Button from '@material-ui/core/Button';
import Menu from '@material-ui/core/Menu';
import MenuItem from '@material-ui/core/MenuItem';




export default function AuthButton() {
    const [anchorEl, setAnchorEl] = React.useState(null);
    const [isAuth, changeAuth] = useRecoilState(isAuthState);
    const history = useHistory();

    const handleClick = (event) => {
        if (isAuth) setAnchorEl(event.currentTarget)
        else history.push("/authentication");
    };

    const handleClose = () => {
        setAnchorEl(null);
    };


    const logOutHandler = () => {
        localStorage.removeItem('TOKEN');
        handleClose();
        history.push("/authentication");
        changeAuth(false);
    };
    return (
        <div>
            <Button aria-controls="simple-menu" aria-haspopup="true" onClick={handleClick}>
                <img src={img} alt="auth ico" />
            </Button>
            <Menu
                id="simple-menu"
                anchorEl={anchorEl}
                keepMounted
                open={Boolean(anchorEl)}
                onClose={handleClose}
            >
                <MenuItem onClick={()=> {
                    history.push('/profile');
                    handleClose();
                }}>Profile</MenuItem>
                <MenuItem onClick={logOutHandler}>Logout</MenuItem>
            </Menu>
        </div>
    );
}
