import React, { useState } from 'react';
import './AuthForm.css';
import axios from '../../../axios';
import isAuthState from '../../../recoilStates/isAuthState';
import { useRecoilState } from 'recoil';
import { Link } from 'react-router-dom';

const AuthForm = (props) => {

    const [, changeAuth] = useRecoilState(isAuthState);

    const [login, changeLogin] = useState('');
    const [password, changePassword] = useState('');


    const authSubmitHandler = (event) => {
        event.preventDefault();
        const data = {
            username: login,
            password: password
        }
        
        axios.post('authentication/login/', data)
            .then(response => {
                localStorage.setItem('TOKEN', response.data.token);
                changeAuth(true);
                props.history.push('/');
            })
            .catch(error => {
                alert("Wrong login or password. Please try again.")
            }) 
    }


    const onChangeLogin = (event) => {
        
        changeLogin(event.target.value);
       
    }
    const onChangePassword = (event) => {
       
        changePassword(event.target.value);
       
    }




    return (
        <div className="AuthForm">
            <form className="login-form" onSubmit={(e) => authSubmitHandler(e)} autoComplete="off">
                <h1 className="a11y-hidden">Login Form</h1>
                <div>
                    <label className="label-email">
                        <input type="text" className="text" name="login" placeholder="Login" onChange={onChangeLogin} tabIndex="1" value={login} required />
                        <span className="required">Login</span>
                    </label>
                </div>
                <input type="checkbox" name="show-password" className="show-password a11y-hidden" id="show-password" tabIndex="3" />
                <label className="label-show-password" htmlFor="show-password">
                    <span>Show Password</span>
                </label>
                <div>
                    <label className="label-password">
                        <input type="text" className="text" name="password" value={password} onChange={onChangePassword} placeholder="Password" tabIndex="2" required />
                        <span className="required">Password</span>
                    </label>
                </div>
                <input type="submit" value="Log In" />
                <div className="email">
                    <a href="/">Forgot password?</a>
                </div>
                <div className="email">
                    <Link to='/registration'>Registration</Link>
                </div>
                <figure aria-hidden="true">
                    <div className="person-body"></div>
                    <div className="neck skin"></div>
                    <div className="head skin">
                        <div className="eyes"></div>
                        <div className="mouth"></div>
                    </div>
                    <div className="hair"></div>
                    <div className="ears"></div>
                    <div className="shirt-1"></div>
                    <div className="shirt-2"></div>
                </figure>
            </form>
        </div>
    );

}

export default AuthForm;



/*


class AuthForm extends Component{
    state={
        username: '',
        password:''
    }

    authSubmitHandler=(event)=>{
        event.preventDefault();
        const data = {
            username: this.state.username,
            password: this.state.password
        }
        axios.post('authentication/login/',data)
            .then(response=>{
                localStorage.setItem('TOKEN', response.data.token);
                this.props.history.goBack(); //сделать так, чтобы не выходило с сайта,Если впервые заходишь

            })
            .catch(error=>{
                alert("Wrong login or password. Please try again.")
            })
    }


    onChangeHandler(event){
        this.setState({
            [event.target.name]: event.target.value
        });
    }

    render(){
        return(
            <div className="AuthForm">
                <form className="login-form" onSubmit={(e)=>this.authSubmitHandler(e)} autoComplete="off">
                <h1 className="a11y-hidden">Login Form</h1>
                <div>
                    <label className="label-email">
                    <input type="text" className="text" name="username" placeholder="Login" onChange={(event)=>this.onChangeHandler(event)} tabIndex="1" value={this.state.login} required />
                    <span className="required">Login</span>
                    </label>
                </div>
                <input type="checkbox" name="show-password" className="show-password a11y-hidden" id="show-password" tabIndex="3" />
                <label className="label-show-password" htmlFor="show-password">
                    <span>Show Password</span>
                </label>
                <div>
                    <label className="label-password">
                    <input type="text" className="text" name="password" value={this.state.password} onChange={(event)=>this.onChangeHandler(event)} placeholder="Password" tabIndex="2" required />
                    <span className="required">Password</span>
                    </label>
                </div>
                <input type="submit" value="Log In" />
                <div className="email">
                    <a href="/">Forgot password?</a>
                </div>
                <figure aria-hidden="true">
                    <div className="person-body"></div>
                    <div className="neck skin"></div>
                    <div className="head skin">
                    <div className="eyes"></div>
                    <div className="mouth"></div>
                    </div>
                    <div className="hair"></div>
                    <div className="ears"></div>
                    <div className="shirt-1"></div>
                    <div className="shirt-2"></div>
                </figure>
                </form>
            </div>
        );
    }
}


*/