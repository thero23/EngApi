import React, { useEffect, useState } from 'react';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import axios from '../../../../axios';


export default function AddWord({ handleClose, getItems, wordId }) {
  const [translate, setTranslate] = useState('');
  const [original, setOriginal] = useState('');
  const [isDisabled, setIsDisabled] = useState(true);
  const getWord = () => {
    axios.get(`dictionaries/words/${wordId}`)
      .then(response => {
        setTranslate(response.data.translate);
        setOriginal(response.data.original);
      })
      .catch(() => handleClose());
  }
  useEffect(() => {
    getWord();
  }, []);

  const saveWord = () => {
    const word = {
      id: wordId,
      translate: translate,
      original: original,
    }
    axios.put('dictionaries/words', word)
      .then((response) => {
        getItems();
        handleClose();
      }).catch(error => {
        console.log(error);
      })
  }

  useEffect(() => {
    setIsDisabled(!original.length || !translate.length);
  }, [original, translate])
  return (
    <div>
      <DialogTitle id="form-dialog-title">Edit Word</DialogTitle>
      <DialogContent>
        <TextField
          autoFocus
          margin="dense"
          id="name"
          label="Original"
          value={original}
          onChange={(e) => setOriginal(e.target.value)}
          type="text"
          autoComplete={false}
          fullWidth
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
        <Button onClick={() => {
          saveWord();
        }} disabled={isDisabled} color="primary">
          Edit
          </Button>
      </DialogActions>
    </div>
  );
}
