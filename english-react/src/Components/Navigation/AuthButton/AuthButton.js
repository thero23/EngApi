import React,{useState} from 'react';
import img from '../../../assets/img/auth-ico.svg';
import './AuthButton.css';
import {Link} from 'react-router-dom';
import isAuthState from '../../../recoilStates/isAuthState';
import {useRecoilState} from 'recoil';

const AuthButton = (props)=>{
    let button =null;
    const [logOut,showLogOut]=useState(false);

    const [isAuth,changeAuth]=useRecoilState(isAuthState);

    const logOutShowHandler=()=>{
        showLogOut(!logOut);
    };

    const logOutHandler=()=>{
        localStorage.removeItem('TOKEN');
       changeAuth(false);
    };

    if(isAuth){
        button=(
            <div className="AuthButton" onClick={()=>logOutShowHandler()}>
                <img src={img} alt="auth ico"/>
                <button style={{display: logOut? "block":"none"}} onClick={()=>logOutHandler()}>LogOut</button>
            </div>
        );
    }else{
        button = (
            <Link to="/authentication" className="AuthButton">
                <img src={img} alt="auth ico"/>
            </Link>
        );
    }

   
    return(
        <React.Fragment>
             {button}
        </React.Fragment>
        
    );
}

export default AuthButton;