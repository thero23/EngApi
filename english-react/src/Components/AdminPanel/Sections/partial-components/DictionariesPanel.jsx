import React, { useEffect, useState } from 'react';
import Grid from '@material-ui/core/Grid';
import { Paper } from '@material-ui/core';
import axios from '../../../../axios';
import IconButton from '@material-ui/core/IconButton';
import { AddCircleTwoTone, DeleteTwoTone, MoreTwoTone } from '@material-ui/icons';
import { red, blue } from '@material-ui/core/colors';


export const DictionariesPanel = ({ editMode, sectionId, handleClickOpen, isOpen }) => {
  const [dictionaries, changeDictionaries] = useState([]);

  const getUsers = () => {
    axios.get(`/sections/${sectionId}/dictionaries`)
      .then(response => {
        const item = response.data;
        changeDictionaries(item);
      })
      .catch(error => {
        alert(error);
      })
  }
  
  useEffect(() => {
    getUsers();
  }, [isOpen]);

  return (
    <Grid item xs={6}>
      <h2>Dictionaries</h2>
      <Paper>
        {dictionaries.map((dict) => {
          return (
            <Paper key={dict.id}>
              Name: {dict.name}
              SecretName: {dict.secretName}
              <IconButton aria-label="details" >
                <MoreTwoTone fontSize="default" style={{ color: blue[500] }} onClick={() => handleClickOpen('dictionaryDetails', dict)} />
              </IconButton>
              {editMode && (
              <IconButton aria-label="delete" >
                <DeleteTwoTone fontSize="default" style={{ color: red[500] }} onClick={() => handleClickOpen('deleteDictionary', dict.id)} />
              </IconButton>)}
            </Paper>
          )
        })}

      </Paper>
      {editMode && (<IconButton aria-label="delete"  onClick={() => handleClickOpen('addDictionary', sectionId)} >
        <AddCircleTwoTone fontSize="default" style={{ color: blue[500] }} />
      </IconButton>)}
    </Grid>
  );
}