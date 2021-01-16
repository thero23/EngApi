import React, { useState, useEffect } from 'react';
import Dictionary from './Dictionary/Dictionary';
import './Dictionaries.css';
import axios from '../../../axios';
import { Link } from 'react-router-dom';


const Dictionaries = (props) => {
    const [dictionaries, dictChange] = useState([]);
    useEffect(() => {
        axios.get('/dictionaries')
            .then(response => {
                const dict = response.data;
                dictChange(dict);
            })
            .catch(error=>console.log(error));
    },[]);

    let dict = dictionaries.map(dic => {
        return (
            <Link to={"/dictionaries/" + dic.id} key={dic.id}><Dictionary secretName={dic.secretName} name={dic.name} /></Link>
        )
    })
    return (
        <div className="Dictionaries">
            {dict}
            <Link to="/words">Words</Link>
        </div>
    );

}

export default Dictionaries;

/*

class Dictionaries extends Component{
    state = {dictionaries:[]}
    componentDidMount(){
        axios.get('/dictionaries')
        .then(response=>{
            const dictionaries = response.data;
            this.setState({dictionaries: dictionaries})
         });
    }



    render(){
        let dictionaries = this.state.dictionaries.map(dic=>{
            return(
                <Link to={"/dictionaries/"+dic.id}  key={dic.id}><Dictionary secretName={dic.secretName} name={dic.name}/></Link>
            )
        })
        return (
            <div className="Dictionaries">
                {dictionaries}
                <Link to="/words">Words</Link>
            </div>
        );
    }
}
*/