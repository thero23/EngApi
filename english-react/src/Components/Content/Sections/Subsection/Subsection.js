import React from 'react';
import './Subsection.css';

const Subsection = ({name, clicked, selected}) =>{
    return(
        <div className={`Subsection ${selected &&('selected-subsection')}`} onClick={clicked}>
            {name}
        </div>
    );
}

export default Subsection;