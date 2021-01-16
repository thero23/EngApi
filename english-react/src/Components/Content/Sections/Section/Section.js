import React from 'react';
import './Section.css';

const Section = (props)=>{
    return(
        <div className="Section">
            {props.name}           
        </div>
    );
}

export default Section;