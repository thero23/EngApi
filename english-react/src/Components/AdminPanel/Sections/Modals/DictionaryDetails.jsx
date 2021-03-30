import React, { useEffect, useState } from 'react';
import Button from '@material-ui/core/Button';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import { Paper } from '@material-ui/core';
import axios from '../../../../axios';

const DictionaryDetails = ({ handleClose, dictionary }) => {
  const [words, setWords] = useState([]);

  useEffect(() => {
    axios.get(`dictionaries/${dictionary.id}/words`)
      .then((response) => {
        setWords(response.data);
      }).catch(error => {
        alert(error)
      })
  }, []);
  return (
    <div>
      <DialogTitle id="alert-dialog-title">
        {dictionary.name}
      </DialogTitle>
      <DialogContent>
      {words.map((word) => {
        return (
          <Paper key={word.id}>
            {word.original}___{word.translate}
          </Paper>
        )
      })}
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose} color="primary" autoFocus>
          Back
        </Button>
      </DialogActions>
    </div>
  );
}

export default DictionaryDetails;