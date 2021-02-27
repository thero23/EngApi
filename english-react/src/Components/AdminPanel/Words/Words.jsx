import React, { useState, useEffect } from 'react';
import axios from '../../../axios';
import { useHistory } from 'react-router';
import IconButton from '@material-ui/core/IconButton';
import { AddCircleTwoTone, DeleteTwoTone, EditTwoTone } from '@material-ui/icons';
import { red, yellow, blue } from '@material-ui/core/colors';
import DeleteWord from './DeleteWord';
import EditWord from './EditWord';
import AddWord from './AddWord';
import { Dialog, TableCell, TableHead, TableRow } from '@material-ui/core';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableContainer from '@material-ui/core/TableContainer';
import Paper from '@material-ui/core/Paper';

const Words = (props) => {
  const [words, changeWords] = useState([]);
  const history = useHistory();
  const [open, setOpen] = useState(false);
  const [modal, setModal] = useState({});
  const handleClickOpen = (name, id) => {
    switch (name) {
      case 'add': setModal(<AddWord handleClose={handleClose} getItems={getItems} />)
        break;
      case 'edit': setModal(<EditWord handleClose={handleClose} getItems={getItems} wordId={id} />)
        break;
      case 'delete': setModal(<DeleteWord handleClose={handleClose} getItems={getItems} wordId={id} />)
        break;
      default:
        break;
    }
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const getItems = () => {
    axios.get('/dictionaries/words')
      .then(response => {
        const words = response.data;
        changeWords(words);
      })
      .catch(error => {
        history.push("/authentication");
      })
  }
  useEffect(() => {
    getItems();
  }, [])

  return (
    <>
      <div style={{ height: 400, width: '80%' }}>
        <>
          <IconButton aria-label="add" onClick={() => handleClickOpen('add')} >
            <AddCircleTwoTone fontSize="large" style={{ color: blue[500] }} />
          </IconButton>
          <TableContainer component={Paper}>
            <Table aria-label="collapsible table">
              <TableHead>
                <TableRow>
                  <TableCell />
                  <TableCell>Original</TableCell>
                  <TableCell>Translate</TableCell>
                  <TableCell />
                </TableRow>
              </TableHead>
              <TableBody>
                {words.map((row) => (
                  <TableRow key={row.id}>
                    <TableCell />
                    <TableCell>{row.original}</TableCell>
                    <TableCell>{row.translate}</TableCell>
                    <TableCell align="right">
                      <IconButton aria-label="edit" onClick={() => handleClickOpen('edit', row.id)}>
                        <EditTwoTone fontSize="default" style={{ color: yellow[500] }} />
                      </IconButton>
                      <IconButton aria-label="delete" onClick={() => handleClickOpen('delete', row.id)}>
                        <DeleteTwoTone fontSize="default" style={{ color: red[500] }} />
                      </IconButton>
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        </>
      </div>
      <Dialog open={open} onClose={handleClose} aria-labelledby="form-dialog-title">
        {modal}
      </Dialog>
    </>
  );
}


export default Words;