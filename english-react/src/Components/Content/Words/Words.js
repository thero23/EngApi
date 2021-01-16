import React, { Component } from 'react';
import Word from './Word/Word';
import './Words.css';
import axios from '../../../axios';

class Words extends Component{
    state = {
        words:[]
    }
    
    componentDidMount(){
        let toUrl;
        this.props.match.params.dictId ? toUrl=this.props.match.params.dictId+"/words":toUrl="words";
        axios.get("/dictionaries/"+toUrl)
            .then(response=>{
                const words = response.data;
                this.setState({words: words});
            })
            .catch(error=>{
                this.props.history.push("/authentication");
            })
    }
    render(){
        const words = this.state.words.map(word=>{
            return(
                <Word key={word.id} translate={word.translate} original={word.original}/>
            );
        })

        return(
            <div className="Words">
               {words}
            </div>
    
        );
    }
}
   


export default Words;