import React from 'react';
import Navigation from '../Navigation/Navigation';
import {Route, Switch} from 'react-router-dom';
import Dictionaries from '../Content/Dictionaries/Dictionaries';
import Help from '../Content/Help/Help';
import Tasks from '../Content/Tasks/Tasks';
import Lectures from '../Content/Lectures/Lectures';


const Main=(props)=>{
    return(
        <React.Fragment>
            <Navigation/>
            <Switch>
                <Route path="/dictionaries" component={Dictionaries}/>
                <Route path="/lectures" component={Lectures}/>
                <Route path="/tasks" component={Tasks}/>
                <Route path="/help" component={Help}/>
            </Switch>
        </React.Fragment>
    );
}

export default Main;
