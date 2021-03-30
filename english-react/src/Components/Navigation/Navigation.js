import React, { useEffect } from 'react';
import './Navigation.css';
import { NavLink } from 'react-router-dom';

import AuthButton from './AuthButton/AuthButton';
import { useRecoilState } from 'recoil';
import isAuthState from '../../recoilStates/isAuthState';
import isTeacherState from '../../recoilStates/isTeacherState';
import axios from '../../axios';
import { Paper } from '@material-ui/core';

const Navigation = (props) => {

    const [isAuth, changeAuth] = useRecoilState(isAuthState);
    const [isTeacher, changeTeacher] = useRecoilState(isTeacherState);

    useEffect(() => {
        axios.get('authentication/check')
            .then(response => {
                changeAuth(true);
                changeTeacher(response.data.teacher);
            })
            .catch(error => {
                changeAuth(false);
                changeTeacher(false);
            });

    }, [isAuth]);

    return (


        <Paper className="Navigation" >

            <header>
                <nav>
                    <ul>
                        {isAuth ? <li><NavLink to="/sections">Main</NavLink></li> : null}
                        <li><NavLink to="/help">Help</NavLink></li>
                        {isTeacher ? <li><NavLink to="/admin">Admin panel</NavLink></li> : null}
                    </ul>
                </nav>
                <AuthButton />
            </header>

        </Paper>

    );
}

export default Navigation;