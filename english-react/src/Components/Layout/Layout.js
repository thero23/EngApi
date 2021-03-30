import React from 'react';
import Navigation from '../Navigation/Navigation';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import Dictionaries from '../Content/Dictionaries/Dictionaries';
import Help from '../Content/Help/Help';
import Sections from '../Content/Sections/Sections';
import Words from '../Content/Words/Words';
import AuthForm from '../Authentication/AuthForm/AuthForm';
import RegForm from '../Authentication/RegistrationForm/RegForm';
import AdminPanel from '../AdminPanel/AdminPanel';
import { useRecoilValue } from 'recoil';
import isTeacherState from '../../recoilStates/isTeacherState';
import NoAccess from '../ErrorPages/Access/NoAccess';
import Profile from '../Navigation/Profile/Profile';
import { Paper } from '@material-ui/core';

const Layout = (props) => {
  const isTeacher = useRecoilValue(isTeacherState);

  return (
    <React.Fragment>
      <BrowserRouter basename="/englishcourse">
        <Navigation />
        <Paper className='main-content'>
          <Switch>
            <Route path="/sections" component={Sections} />
            <Route path="/dictionaries/:dictId" exact component={Words} />
            <Route path="/dictionaries" exact component={Dictionaries} />
            <Route path="/help" component={Help} />
            <Route path="/words" exact component={Words} />
            <Route path="/authentication" component={AuthForm} />
            <Route path="/registration" component={RegForm} />
            <Route path="/admin" component={isTeacher ? AdminPanel : NoAccess} />
            <Route path="/profile" component={Profile}/>
            <Route path="/" component={Sections} />
          </Switch>
        </Paper>
      </BrowserRouter>
    </React.Fragment>
  );
}

export default Layout;
