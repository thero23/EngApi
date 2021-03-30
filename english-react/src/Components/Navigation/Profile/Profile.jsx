import React, { useEffect, useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import { useRecoilState } from 'recoil';
import isAuthState from '../../../recoilStates/isAuthState';
import axios from '../../../axios';
import { useHistory } from 'react-router';
import './style.css';

const useStyles = makeStyles({
  root: {
    minWidth: 275,
  },
  bullet: {
    display: 'inline-block',
    margin: '0 2px',
    transform: 'scale(0.8)',
  },
  title: {
    fontSize: 14,
  },
  pos: {
    marginBottom: 12,
  },
});

export default function Profile() {
  const history = useHistory();
  const classes = useStyles();
  const [user, setUser] = useState({});
  const [roles, setRoles] = useState([]);
  const [isAuth, changeAuth] = useRecoilState(isAuthState);

  useEffect(() => {
    if (isAuth) {
      axios.get('authentication/getCurrentUser')
        .then(({ data }) => {
          setUser(data.user);
          setRoles(data.role);
        })
        .catch(error => {
          alert(error)
        })
    } else history.push('/authentication');
  }, [])

  const logOutHandler = () => {
    localStorage.removeItem('TOKEN');
    changeAuth(false);
    history.push("/authentication");
    
};
  return (
    <div style={{ height: '75vh' }}>
      <Card className={classes.root} variant="outlined">
        <div className='profile-content'>
          <div className='profile-left'>
            <Typography>
              Login:
            </Typography>
            <Typography>
              Email: 
            </Typography>
            <Typography>
              Name: 
            </Typography>
            <Typography>
              Last name: 
            </Typography>
            <Typography>
              Patronymic: 
            </Typography>
            <Typography>
              Phone:
            </Typography>
            <Typography>
              Role:
            </Typography>
          </div>
          <div className='profile-right'>
            <Typography>
              {user.userName}
            </Typography>
            <Typography>
               {user.email}
            </Typography>
            <Typography>
               {user.firstName}
            </Typography>
            <Typography>
               {user.lastName}
            </Typography>
            <Typography>
             {user.patronymic}
            </Typography>
            <Typography>
          {user.phoneNumber}
            </Typography>
            <Typography>
          {roles.join(', ')}
            </Typography>
          </div>
        </div>

        <CardActions>
          <Button size="small" onClick={() => logOutHandler()}>Logout</Button>
        </CardActions>
      </Card>
    </div>
  );
}
