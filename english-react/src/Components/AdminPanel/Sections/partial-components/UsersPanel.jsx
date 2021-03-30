import React, { useEffect, useState } from 'react';
import Grid from '@material-ui/core/Grid';
import { Paper } from '@material-ui/core';
import axios from '../../../../axios';
import IconButton from '@material-ui/core/IconButton';
import { AddCircleTwoTone, DeleteTwoTone, MoreTwoTone } from '@material-ui/icons';
import { red, blue } from '@material-ui/core/colors';


export const UsersPanel = ({ editMode, sectionId, handleClickOpen, isOpen }) => {
  const [users, changeUsers] = useState([]);

  const getUsers = () => {
    axios.get(`/sections/${sectionId}/users`)
      .then(response => {
        const item = response.data;
        changeUsers(item);
      })
      .catch(error => {
        alert(error);
      })
  }
  
  useEffect(() => {
    getUsers();
  }, [isOpen]);

  return (
    <Grid item xs={6}>
      <h2>Users</h2>
      <Paper>
        {users.map((user) => {
          return (
            <Paper key={user.id}>
              {user.userName}
              <IconButton aria-label="details" >
                <MoreTwoTone fontSize="default" style={{ color: blue[500] }} onClick={() => handleClickOpen('userDetails', user)} />
              </IconButton>
              {editMode && (
              <IconButton aria-label="delete" >
                <DeleteTwoTone fontSize="default" style={{ color: red[500] }} onClick={() => handleClickOpen('deleteUser', user.id)} />
              </IconButton>)}
            </Paper>
          )
        })}

      </Paper>
      {editMode && (<IconButton aria-label="delete"  onClick={() => handleClickOpen('addUsers', sectionId)} >
        <AddCircleTwoTone fontSize="default" style={{ color: blue[500] }} />
      </IconButton>)}
    </Grid>
  );
}