import React, { useEffect, useState } from 'react';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import axios from '../../../axios';

export default function AddWord({ handleClose, getItems, dictionaryId }) {
  const [name, setName] = useState('');
  const [secretName, setSecretName] = useState('');
  const [isDisabled, setIsDisabled] = useState(true);
  const getWord = () => {
    axios.get(`dictionaries/${dictionaryId}`)
      .then(response => {
        setName(response.data.name);
        setSecretName(response.data.secretName);
      })
      .catch(() => handleClose());
  }
  useEffect(() => {
    getWord();
  }, []);

  const saveDictionary = () => {
    const dictionary = {
      id: dictionaryId,
      name: name,
      secretName: secretName,
    }
    axios.put('dictionaries/', dictionary)
      .then((response) => {
        getItems();
        handleClose();
      }).catch(error => {
        console.log(error);
      })
  }

  useEffect(() => {
    setIsDisabled(!name.length || !secretName.length);
  }, [name, secretName])
  return (
    <div>
      <DialogTitle id="form-dialog-title">Edit Word</DialogTitle>
      <DialogContent>
        <TextField
          autoFocus
          margin="dense"
          id="name"
          label="Name"
          value={name}
          onChange={(e) => setName(e.target.value)}
          type="text"
          fullWidth
        />
      </DialogContent>
      <DialogContent>
        <TextField
          margin="dense"
          label="Name for teachers"
          value={secretName}
          onChange={(e) => setSecretName(e.target.value)}
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
          saveDictionary();
        }} disabled={isDisabled} color="primary">
          Edit
        </Button>
      </DialogActions>
    </div>
  );
}
