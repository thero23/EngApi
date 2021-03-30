import React from 'react';
import Button from '@material-ui/core/Button';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import axios from '../../../../axios';

const DeleteSubsection = ({ handleClose, subsectionId }) => {

  const removeSubsection = () => {
    axios.delete(`subsections/${subsectionId}`)
      .then((response) => {
        handleClose();
      }).catch(error => {
        alert(error)
      })
  }
  return (
    <div>
      <DialogTitle id="alert-dialog-title">
        Remove subsection
        </DialogTitle>
      <DialogContent>
        <DialogContentText id="alert-dialog-description">
          Are you sure you want to remove this subsection subsection?
          </DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button onClick={() => removeSubsection()} color="primary">
          Yes
          </Button>
        <Button onClick={handleClose} color="primary" autoFocus>
          No
          </Button>
      </DialogActions>
    </div>
  );
}

export default DeleteSubsection;