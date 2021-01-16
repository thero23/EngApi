import React, { Component } from 'react';
import './Dictionary.css';

class Dictionary extends Component{

    render(){
        return(
            <div className="Dictionary">
                <h1>{this.props.name}</h1>
                <h2>{this.props.secretName}</h2>
            </div>
        );
    }
}

export default Dictionary;