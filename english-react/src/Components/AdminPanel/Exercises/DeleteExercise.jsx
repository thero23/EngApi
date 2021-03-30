import React from 'react';
import Button from '@material-ui/core/Button';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import axios from '../../../axios';

const DeleteExercise = ({handleClose, exerciseId, getItems}) => {
  const deleteExercise = () => {

    axios.delete(`exercises/${exerciseId}`)
    .then(()=>{
      getItems()
      handleClose();
    }).catch(error=>{
      console.log(error);
    })
  }
  return (
    <div>
        <DialogTitle id="alert-dialog-title">
          Delete exercise
        </DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            Do You realy want to delete this exercise?
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={deleteExercise} color="primary">
            Yes
          </Button>
          <Button onClick={handleClose} color="primary" autoFocus>
            No
          </Button>
        </DialogActions>
    </div>
  );
}

export default DeleteExercise;