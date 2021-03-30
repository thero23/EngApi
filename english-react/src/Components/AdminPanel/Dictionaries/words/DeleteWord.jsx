import React from 'react';
import Button from '@material-ui/core/Button';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import axios from '../../../../axios';

export default function DeleteWord({handleClose, getItems, wordId, dictionaryId}) {
  const deleteWord = () => {

    axios.delete(`dictionaries/${dictionaryId}/words/${wordId}`)
    .then(()=>{
      getItems();
      handleClose();
    }).catch(error=>{
      console.log(error);
      console.log(dictionaryId);
      console.log(wordId);
    })
  }
  return (
    <div>
        <DialogTitle id="alert-dialog-title">
          {'Delete word'}
        </DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            Do You realy want to remove this word from dictionary?
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={deleteWord} color="primary">
            Yes
          </Button>
          <Button onClick={handleClose} color="primary" autoFocus>
            No
          </Button>
        </DialogActions>
    </div>
  );
}
