import React from 'react';
import './Word.css';


const word=(props)=>{
    return(
            <div className="Word">
                <div className="Original">{props.original}</div><div className="Translate">{props.translate}</div>
            </div>
        
    );
}

export default word;