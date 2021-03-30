import React, { useState, useEffect } from 'react';
import './Words.css';
import axios from '../../../axios';
import { DataGrid } from '@material-ui/data-grid';
import { Button } from '@material-ui/core';

const columns = [
  { field: 'id', headerName: 'ID', width: 70 },
  { field: 'original', headerName: 'Original', width: 130 },
  { field: 'translate', headerName: 'Translate', width: 130 },
];
const Words = ({dictionaryId, setSelectedDict}) => {
  const [words, changeWords] = useState([]);

  useEffect(() => {
    axios.get(`dictionaries/${dictionaryId}/words`)
      .then(response => {
        const words = response.data;
        changeWords(words);
      })
      .catch(error => {
        alert(error);
      })
  }, [])

  return (
    <div style={{ height: 400, width: '100%' }}>
      <DataGrid rows={words} columns={columns} pageSize={5} checkboxSelection />
      <Button onClick={() => setSelectedDict(null)}>Back</Button>
    </div>
  );
}


export default Words;