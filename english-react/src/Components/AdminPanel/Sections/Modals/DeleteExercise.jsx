import React from 'react';
import Button from '@material-ui/core/Button';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import axios from '../../../../axios';

const DeleteExercise = ({ handleClose, subsectionId, exerciseId }) => {

  const removeExercise = () => {
    axios.delete(`subsections/${subsectionId}/exercise/${exerciseId}`)
      .then((response) => {
        handleClose();
      }).catch(error => {
        alert(error)
      })
  }
  return (
    <div>
      <DialogTitle id="alert-dialog-title">
        Remove exercise
        </DialogTitle>
      <DialogContent>
        <DialogContentText id="alert-dialog-description">
          Are you sure you want to remove this exercises subsection?
          </DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button onClick={() => removeExercise()} color="primary">
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