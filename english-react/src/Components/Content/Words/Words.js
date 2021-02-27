import React, { useState, useEffect } from 'react';
import './Words.css';
import axios from '../../../axios';
import { DataGrid } from '@material-ui/data-grid';
import { useHistory } from 'react-router';

const columns = [
  { field: 'id', headerName: 'ID', width: 70 },
  { field: 'original', headerName: 'Original', width: 130 },
  { field: 'translate', headerName: 'Translate', width: 130 },
];
const Words = (props) => {
  const [words, changeWords] = useState([]);
  const history = useHistory();

  useEffect(() => {
    let toUrl = 'words';
    props.match.params.dictId ? toUrl = props.match.params.dictId + "/words" : toUrl = "words";
    axios.get("/dictionaries/" + toUrl)
      .then(response => {
        const words = response.data;
        changeWords(words);
      })
      .catch(error => {
        history.push("/authentication");
      })
  }, [])

  return (
    <div style={{ height: 400, width: '100%' }}>
      <DataGrid rows={words} columns={columns} pageSize={5} checkboxSelection />
    </div>
  );
}


export default Words;