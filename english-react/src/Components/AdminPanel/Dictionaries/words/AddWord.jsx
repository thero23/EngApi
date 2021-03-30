import React, { useEffect, useState } from 'react';
import Button from '@material-ui/core/Button';
import DialogActions from '@material-ui/core/DialogActions';
import DialogTitle from '@material-ui/core/DialogTitle';
import axios from '../../../../axios';
import IconButton from '@material-ui/core/IconButton';
import { AddCircleTwoTone } from '@material-ui/icons';
import { blue } from '@material-ui/core/colors';
import { TableCell, TableHead, TableRow } from '@material-ui/core';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableContainer from '@material-ui/core/TableContainer';
import Paper from '@material-ui/core/Paper';


export default function AddWord({ handleClose, getItems, dictionaryId }) {
  const [words, setWords] = useState([]);
  const [newWords, setNewWords] = useState([]);

  const saveWord = (wordId) => {
    axios.post(`dictionaries/${dictionaryId}/words/${wordId}`)
      .then((response) => {
        getItems();
        alert('Word has been added')
      }).catch(error => {
        alert('Word already in dictionary');
      })
  }

  const getDictionaryWords = () => {
    axios.get(`dictionaries/${dictionaryId}/words`)
      .then((response) => {
        setNewWords(response.data);
      }).catch(error => {
      })
  }
  useEffect(() => {
    getDictionaryWords();
    getNewWords();
  }, [words])

  const getNewWords = () => {
    axios.get(`dictionaries/words`)
    .then((response) => {
      const data = response.data;
      const myArray = data.filter(el => !newWords.includes(el));
      setWords(myArray);
    }).catch(error => {
    })
  }
  useEffect(() => {
    getDictionaryWords();
    getNewWords();
  }, [])

  return (
    <div>
      <DialogTitle id="form-dialog-title">Add Word</DialogTitle>
      <TableContainer component={Paper}>
        <Table aria-label="collapsible table">
          <TableHead>
            <TableRow>
              <TableCell>Original</TableCell>
              <TableCell>Translate</TableCell>
              <TableCell />
            </TableRow>
          </TableHead>
          <TableBody>
            {words.map((row) => (
              <TableRow key={row.id}>
                <TableCell>{row.original}</TableCell>
                <TableCell>{row.translate}</TableCell>
                <TableCell>
                  <IconButton aria-label="edit" onClick={() => saveWord(row.id)}>
                    <AddCircleTwoTone fontSize="default" style={{ color: blue[500] }} />
                  </IconButton>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      <DialogActions>
        <Button onClick={handleClose} color="primary">
          Cancel
        </Button>
      </DialogActions>
    </div>
  );
}
