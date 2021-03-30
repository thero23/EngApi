import React from 'react';
import Button from '@material-ui/core/Button';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import axios from '../../../../axios';

const DeleteDictionary = ({ handleClose, sectionId, dictionaryId }) => {

  const removeDictionary = () => {
    axios.delete(`sections/${sectionId}/dictionaries/${dictionaryId}`)
      .then((response) => {
        handleClose();
      }).catch(error => {
        alert(error)
      })
  }
  return (
    <div>
      <DialogTitle id="alert-dialog-title">
        Remove dictionary
        </DialogTitle>
      <DialogContent>
        <DialogContentText id="alert-dialog-description">
          Are you sure you want to remove this dictionary?
        </DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button onClick={() => removeDictionary()} color="primary">
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