import React from 'react';
import './RegForm.css';

const RegForm = () => {
    return (
        <div className="AuthForm">
        <form className="login-form"  autoComplete="off">
            <h1 className="a11y-hidden">Login Form</h1>
            <div>
                <label className="label-email">
                    <input type="text" className="text" name="login" placeholder="Login"  tabIndex="1"  required />
                    <span className="required">Login</span>
                </label>
            </div>
            <div>
                <label className="label-email">
                    <input type="text" className="text" name="login" placeholder="Email"  tabIndex="2"  required />
                    <span className="required">Email</span>
                </label>
            </div>
            <div>
                <label className="label-email">
                    <input type="text" className="text" name="login" placeholder="FirstName"  tabIndex="3"  required />
                    <span className="required">FirstName</span>
                </label>
            </div>
            <div>
                <label className="label-email">
                    <input type="text" className="text" name="login" placeholder="LastName"  tabIndex="4"  required />
                    <span className="required">LastName</span>
                </label>
            </div>
            <div>
                <label className="label-email">
                    <input type="text" className="text" name="login" placeholder="Patronymic"  tabIndex="5"  required />
                    <span className="required">Patronymic</span>
                </label>
            </div>
            <div>
                <label className="label-email">
                    <input type="text" className="text" name="login" placeholder="Phone Number"  tabIndex="6"  required />
                    <span className="required">Phone Number</span>
                </label>
            </div>
            <div>
                <label className="label-password">
                    <input type="text" className="text"  placeholder="Password" tabIndex="7" required />
                    <span className="required">Password</span>
                </label>
            </div>
            <div>
                <label className="label-password">
                    <input type="text" className="text" placeholder="Password" tabIndex="8" required />
                    <span className="required">Repeat Password</span>
                </label>
            </div>
            <input type="submit" value="Log In" /> 
        </form>
    </div>

    );

}

export default RegForm;