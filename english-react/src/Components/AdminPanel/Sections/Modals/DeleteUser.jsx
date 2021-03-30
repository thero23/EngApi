import React from 'react';
import Button from '@material-ui/core/Button';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import axios from '../../../../axios';

const DeleteUser = ({ handleClose, sectionId, userId }) => {

  const removeUser = () => {
    axios.delete(`sections/${sectionId}/users/${userId}`)
      .then((response) => {
        handleClose();
      }).catch(error => {
        alert(error)
      })
  }
  return (
    <div>
      <DialogTitle id="alert-dialog-title">
        Remove user
        </DialogTitle>
      <DialogContent>
        <DialogContentText id="alert-dialog-description">
          Are you sure you want to deny this user access to the section?
          </DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button onClick={() => removeUser()} color="primary">
          Yes
          </Button>
        <Button onClick={handleClose} color="primary" autoFocus>
          No
          </Button>
      </DialogActions>
    </div>
  );
}

export default DeleteUser;