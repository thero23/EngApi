import React, {useState}  from 'react';
import axios from '../../../axios';
import isAuthState from '../../../recoilStates/isAuthState';
import { useRecoilState } from 'recoil';
import './RegForm.css';

const RegForm = (props) => {
    const [login, changeLogin] = useState('');
    const [password, changePassword] = useState('');
    const [email, changeEmail] = useState('');
    const [firstName, changeFirstName] = useState('');
    const [lastName, changeLastName] = useState('');
    const [phoneNumber, changePhoneNumber] = useState('');
    const [patronymic, changePatronymic] = useState('');
    const [, changeAuth] = useRecoilState(isAuthState);

    const regSubmitHandler = (event) => {
        event.preventDefault();
        const data = {
            username: login,
            email: email,
            firstName: firstName,
            lastName: lastName,
            patronymic: patronymic,
            phoneNumber: phoneNumber,
            roles:["User"],
            password: password,
        }
        
        axios.post('authentication/register/', data)
            .then(response => {
                localStorage.removeItem('TOKEN');
                changeAuth(false);
                props.history.push('/authentication');
            })
            .catch(error => {
                alert("Something went wrong =)");
            }) 
    }

    return (
        <div className="AuthForm">
        <form className="login-form"  onSubmit={(e) => regSubmitHandler(e)} autoComplete="off">
            <h1 className="a11y-hidden">Login Form</h1>
            <div>
                <label className="label-email">
                    <input type="text" className="text" name="login" value={login} onChange={(event)=>changeLogin(event.target.value)} placeholder="Login"  tabIndex="1"  required />
                    <span className="required">Login</span>
                </label>
            </div>
            <div>
                <label className="label-email">
                    <input type="text" className="text" placeholder="Email" value={email} onChange={(event)=>changeEmail(event.target.value)} tabIndex="2"  />
                    <span>Email</span>
                </label>
            </div>
            <div>
                <label className="label-email">
                    <input type="text" className="text" placeholder="FirstName"  tabIndex="3" value={firstName} onChange={(event)=>changeFirstName(event.target.value)}  />
                    <span>FirstName</span>
                </label>
            </div>
            <div>
                <label className="label-email">
                    <input type="text" className="text" placeholder="LastName"  tabIndex="4"  value={lastName} onChange={(event)=>changeLastName(event.target.value)} />
                    <span>LastName</span>
                </label>
            </div>
            <div>
                <label className="label-email">
                    <input type="text" className="text" placeholder="Patronymic"  tabIndex="5" value={patronymic} onChange={(event)=>changePatronymic(event.target.value)} />
                    <span>Patronymic</span>
                </label>
            </div>
            <div>
                <label className="label-email">
                    <input type="text" className="text" placeholder="Phone Number"  tabIndex="6" value={phoneNumber} onChange={(event)=>changePhoneNumber(event.target.value)} />
                    <span>Phone Number</span>
                </label>
            </div>
            <div>
                <label className="label-password">
                    <input type="text" className="text"  placeholder="Password" tabIndex="7" required value={password} onChange={(event)=>changePassword(event.target.value)} />
                    <span className="required">Password</span>
                </label>
            </div>
            <input type="submit" value="Log In" /> 
        </form>
    </div>

    );

}

export default RegForm;