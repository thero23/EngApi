import React, { useEffect, useState } from 'react';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import axios from '../../../../axios';

export default function AddWord({ handleClose, getItems }) {
  const [translate, setTranslate] = useState('');
  const [original, setOriginal] = useState('');
  const [isDisabled, setIsDisabled] = useState(true);
  const saveWord = () => {
    axios.post('dictionaries/words', { original, translate })
      .then((response) => {
        getItems();
        handleClose();
      }).catch(error => {
        alert(error);
      })
  }

  useEffect(() => {
    setIsDisabled(!original.length || !translate.length);
  }, [original, translate])

  return (
    <div>
      <DialogTitle id="form-dialog-title">Add Word</DialogTitle>
      <DialogContent>
        <TextField
          autoFocus
          margin="dense"
          id="name"
          label="Original"
          value={original}
          onChange={(e) => setOriginal(e.target.value)}
          type="text"
          fullWidth
          autoComplete={false}
        />
      </DialogContent>
      <DialogContent>
        <TextField
          margin="dense"
          label="Translate"
          value={translate}
          onChange={(e) => setTranslate(e.target.value)}
          type="text"
          fullWidth
          multiline
        />
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose} color="primary">
          Cancel
        </Button>
        <Button onClick={saveWord}  disabled={isDisabled} color="primary">
          Add
        </Button>
      </DialogActions>
    </div>
  );
}
