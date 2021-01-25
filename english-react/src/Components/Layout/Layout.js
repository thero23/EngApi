import React from 'react';
import Navigation from '../Navigation/Navigation';
import { Route, Switch } from 'react-router-dom';
import Dictionaries from '../Content/Dictionaries/Dictionaries';
import Help from '../Content/Help/Help';
import Tasks from '../Content/Tasks/Tasks';
import Sections from '../Content/Sections/Sections';
import Words from '../Content/Words/Words';
import AuthForm from '../Authentication/AuthForm/AuthForm';
import RegForm from '../Authentication/RegistrationForm/RegForm';

const Layout = (props) => {
   
   
    return (
        <React.Fragment>
            
            <Navigation />
            <Switch>
                <Route path="/dictionaries/:dictId" exact component={Words} />
                <Route path="/dictionaries" exact component={Dictionaries} />
                <Route path="/sections" component={Sections} />
                <Route path="/tasks" component={Tasks} />
                <Route path="/help" component={Help} />
                <Route path="/words" exact component={Words} />
                <Route path="/authentication" component={AuthForm} />
                <Route path="/registration" component={RegForm} />

            </Switch>
        </React.Fragment>
    );
}

export default Layout;
