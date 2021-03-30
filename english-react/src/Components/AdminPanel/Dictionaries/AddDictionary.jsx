/* eslint-disable import/no-extraneous-dependencies */
import React, { useEffect, useState } from 'react';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import Snackbar from '@material-ui/core/Snackbar';
import MuiAlert from '@material-ui/lab/Alert';
import axios from '../../../axios';

function Alert(props) {
  return <MuiAlert elevation={6} variant="filled" {...props} />;
}

export default function AddWord({ handleClose, getItems }) {
  const [name, setName] = useState('');
  const [secretName, setSecretName] = useState('');
  const [isDisabled, setIsDisabled] = useState(true);
  const [open, setOpen] = React.useState(false);
  const saveDictionary = () => {
    axios.post('dictionaries', { name, secretName })
      .then(() => {
        setOpen(true);
        getItems();
        handleClose();
      }).catch((error) => {
        
      });
  };
  const handleAlertClose = (event, reason) => {
    if (reason === 'clickaway') {
      return;
    }

    setOpen(false);
  };
  useEffect(() => {
    setIsDisabled(!name.length || !secretName.length);
  }, [name, secretName]);
  return (
    <div>
      <DialogTitle id="form-dialog-title">Add dictionary</DialogTitle>
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
          autoComplete={false}
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
          autoComplete={false}
        />
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose} color="primary">
          Cancel
        </Button>
        <Button
          onClick={() => {
            saveDictionary();
          }}
          disabled={isDisabled}
          color="primary"
        >
          Add
        </Button>
      </DialogActions>
      <Snackbar open={open} autoHideDuration={6000} onClose={handleAlertClose}>
        <Alert onClose={handleAlertClose} severity="success">
          Dictionary added successfully!
        </Alert>
      </Snackbar>
    </div>
  );
}
