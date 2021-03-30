import React from 'react';
import Button from '@material-ui/core/Button';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import { Paper } from '@material-ui/core';

const UserDetails = ({ handleClose, user }) => {

  return (
    <div>
      <DialogTitle id="alert-dialog-title">
        User details
        </DialogTitle>
      <DialogContent>
        <Paper>
          Login: {user.userName}
        </Paper>
        <Paper>
          Email: {user.email}
        </Paper>
        <Paper>
          First name: {user.firstName}
        </Paper>
        <Paper>
          Last ame: {user.lastName}
        </Paper>
        <Paper>
          Patronymic: {user.patronymic}
        </Paper>
        <Paper>
          Phone Number: {user.phoneNumber}
        </Paper>
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose} color="primary" autoFocus>
          Back
        </Button>
      </DialogActions>
    </div>
  );
}

export default UserDetails;