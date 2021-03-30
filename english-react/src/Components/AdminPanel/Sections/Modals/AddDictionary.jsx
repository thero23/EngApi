import React, { useEffect, useState } from 'react';
import Button from '@material-ui/core/Button';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import { IconButton, Paper } from '@material-ui/core';
import axios from '../../../../axios';
import { AddCircleTwoTone } from '@material-ui/icons';
import { blue } from '@material-ui/core/colors';

const AddDictionary = ({ handleClose, sectionId }) => {
  const [dictionaries, changeDictionaries] = useState([]);

  const getDictionaries = () => {
    axios.get(`sections/${sectionId}/notdictionaries`)
      .then((response) => {
        changeDictionaries(response.data)
      }).catch(error => {
        alert(error)
      })
  }

  const addDictionary = (id) =>{
    axios.post(`sections/${sectionId}/dictionaries/${id}`)
      .then(() => {
        getDictionaries();
      }).catch(error => {
        alert(error)
      })
  }

  useEffect(() => {
    getDictionaries();
  }, [])
  return (
    <div>
      <DialogTitle id="alert-dialog-title">
        Add dictionaries
        </DialogTitle>
      <DialogContent>
        {dictionaries.map((dict) => {
          return (
            <Paper key={dict.id}>
              {dict.name}___{dict.secretName}
              <IconButton aria-label="add" onClick={() => addDictionary(dict.id)} >
                <AddCircleTwoTone fontSize="default" style={{ color: blue[500] }} />
              </IconButton>
            </Paper>
          );
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

export default AddDictionary;