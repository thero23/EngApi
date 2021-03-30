import React from 'react';
import Button from '@material-ui/core/Button';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import axios from '../../../axios';

const DeleteSection = ({handleClose, sectionId, getItems}) => {
  const deleteSection = () => {

    axios.delete(`sections/${sectionId}`)
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
          Delete section
        </DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            Do You realy want to delete this section?
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={deleteSection} color="primary">
            Yes
          </Button>
          <Button onClick={handleClose} color="primary" autoFocus>
            No
          </Button>
        </DialogActions>
    </div>
  );
}

export default DeleteSection;