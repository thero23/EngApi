import React from 'react';
import Button from '@material-ui/core/Button';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import axios from '../../../axios';

const DeleteDictionary = ({handleClose, getItems, dictionaryId}) => {
  const deleteDictionary = () => {
    axios.delete(`dictionaries/${dictionaryId}`)
    .then(()=>{
      getItems();
      handleClose();
    }).catch(error=>{
      alert(dictionaryId);
    })
  }
  return (
    <div>
        <DialogTitle id="alert-dialog-title">{"Delete dictionary"}</DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            Do You realy want to delete this dictionary?
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={deleteDictionary} color="primary">
            Yes
          </Button>
          <Button onClick={handleClose} color="primary" autoFocus>
            No
          </Button>
        </DialogActions>
    </div>
  );
}
export default DeleteDictionary;