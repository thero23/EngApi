import React, { useState, useEffect } from 'react';
import axios from '../../../axios';
import Section from './Section/Section';
import './Sections.css';
const Sections = (props) => {
    const [sections, changeSections] = useState([]);

    useEffect(() => {
        axios.get("/sections")
            .then(response => {
                const sect = response.data;
                changeSections(sect);
                console.log(sect);
            })
            .catch(error => {   
                console.log(error)
            });
    },[]);

        


    let sect = sections.map(sec => {
        return (
            <Section key={sec.id} name={sec.name} />
        )
    });
    


    return (
        <div className="Sections">
            
            <div className="Tree">
                {sect}
            </div>
            <div className="Content">
                content
            </div>

        </div>


    );
}

export default Sections;