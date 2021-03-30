import React from 'react';
import { useHistory } from 'react-router';
import './style.css';

const Footer = () => {
  const history = useHistory();
  return (
    <div className='footer'>
      <h3>Vitebsk State Technologocal College</h3>
      <div className='footer-bottom'>
        <div className='footer-bottom-left'>
          <u onClick={() => history.push('/help')}>Help</u>
        </div>
        <div className='footer-bottom-right'>
          All right reserved by VSTC. (c) 2021
        </div>

      </div>

    </div>
  )
}

export default Footer;