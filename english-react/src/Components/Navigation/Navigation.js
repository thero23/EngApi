import React,{ useEffect} from 'react';
import './Navigation.css';
import {NavLink} from 'react-router-dom'; 

import AuthButton from './AuthButton/AuthButton';
import {useRecoilState} from 'recoil';
import isAuthState from '../../recoilStates/isAuthState';
import axios from '../../axios';

const Navigation =(props)=>{
 
    const [isAuth,changeAuth]=useRecoilState(isAuthState);

    useEffect(()=>{
        axios.get('authentication/check')
        .then(response=>changeAuth(true))
        .catch(error=>changeAuth(false));

    },[]);

    return(

        <div className="Navigation">
           <header>
               <nav>
                   <ul>
                       <li><NavLink to="/dictionaries">Словари</NavLink></li>
                       <li><NavLink to="/sections">Разделы</NavLink></li>
                       <li><NavLink to="/tasks">Задания</NavLink></li>
                       <li><NavLink to="/help">Справка</NavLink></li>
                   </ul>
               </nav>
               <button>{String(isAuth)}</button>
               <AuthButton/>
           </header>
        </div>
    );
}

export default Navigation;