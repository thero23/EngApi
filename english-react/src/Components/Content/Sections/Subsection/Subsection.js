import React from 'react';
import './Subsection.css';

const Subsection = ({name, clicked}) =>{
    return(
        <div className='Subsection' onClick={clicked}>
            {name}
        </div>
    );
}

export default Subsection;