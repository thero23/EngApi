import React from 'react';
import './Navigation.css';
import {NavLink} from 'react-router-dom'; 

const Navigation =(props)=>{
    return(

        <div className="Navigation">
           <header>
               <nav>
                   <ul>
                       <li><NavLink to="/dictionaries">Словари</NavLink></li>
                       <li><NavLink to="/lectures">Лекции</NavLink></li>
                       <li><NavLink to="/tasks">Задания</NavLink></li>
                       <li><NavLink to="/help">Справка</NavLink></li>
                   </ul>
               </nav>
           </header>
        </div>
    );
}

export default Navigation;