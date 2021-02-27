import React,{ useEffect} from 'react';
import './Navigation.css';
import {NavLink} from 'react-router-dom'; 

import AuthButton from './AuthButton/AuthButton';
import {useRecoilState} from 'recoil';
import isAuthState from '../../recoilStates/isAuthState';
import isTeacherState from '../../recoilStates/isTeacherState';
import axios from '../../axios';

const Navigation =(props)=>{
 
    const [isAuth,changeAuth]=useRecoilState(isAuthState);
    const [isTeacher,changeTeacher]=useRecoilState(isTeacherState);

    useEffect(()=>{
        axios.get('authentication/check')
        .then(response=>{
            changeAuth(true);
            console.log('isTeacher: ',response.data.teacher);
            changeTeacher(response.data.teacher);
        })
        .catch(error=>{
            changeAuth(false);
            changeTeacher(false);
        });

    },[isAuth]);

    return(

        <div className="Navigation">
           <header>
               <nav>
                   <ul>
                       <li><NavLink to="/sections">Главная</NavLink></li>
                       <li><NavLink to="/dictionaries">Словари</NavLink></li>
                       {/* <li><NavLink to="/tasks">Задания</NavLink></li> */}
                       <li><NavLink to="/help">Справка</NavLink></li>
                       {isTeacher ?  <li><NavLink to="/admin">Админка</NavLink></li> : null}
                   </ul>
               </nav>
               <AuthButton/>
           </header>
        </div>
    );
}

export default Navigation;