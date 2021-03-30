import React, { useEffect, useState } from 'react';
import Button from '@material-ui/core/Button';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import { IconButton, Paper } from '@material-ui/core';
import axios from '../../../../axios';
import { AddCircleTwoTone } from '@material-ui/icons';
import { blue } from '@material-ui/core/colors';

const AddUsers = ({ handleClose, sectionId }) => {
  const [users, changeUsers] = useState([]);

  const getUsers = () => {
    axios.get(`sections/${sectionId}/notusers`)
      .then((response) => {
        changeUsers(response.data)
      }).catch(error => {
        alert(error)
      })
  }

  const addUser = (id) =>{
    axios.post(`sections/${sectionId}/users/${id}`)
      .then(() => {
        getUsers();;
      }).catch(error => {
        alert(error)
      })
  }

  useEffect(() => {
    getUsers();
  }, [])
  return (
    <div>
      <DialogTitle id="alert-dialog-title">
        Add users
        </DialogTitle>
      <DialogContent>
        {users.map((user) => {
          return (
            <Paper key={user.id}>
              {user.userName}
              <IconButton aria-label="add" onClick={() => addUser(user.id)} >
                <AddCircleTwoTone fontSize="default" style={{ color: blue[500] }} />
              </IconButton>
            </Paper>
          );
        })}
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose} color="primary" autoFocus>
          Back
        </Button>
      </DialogActions>
    </div>
  );
}

export default AddUsers;