import React, { useEffect, useState } from 'react';
import IconButton from '@material-ui/core/IconButton';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import axios from '../../../axios';
import { AddCircleTwoTone } from '@material-ui/icons';
import { blue } from '@material-ui/core/colors';
import AddDictionary from './AddDictionary';
import EditDictionary from './EditDictionary';
import DeleteDictionary from './DeleteDictionary';
import { Dialog } from '@material-ui/core';
import Rows from './words/Rows';

const Dictionaries = () => {
  const [dictionaries, changeDictionaries] = useState([]);
  const [open, setOpen] = useState(false);
  const [modal, setModal] = useState({});
  const handleClickOpen = (name, id) => {
    switch (name) {
      case 'add': setModal(<AddDictionary handleClose={handleClose} getItems={getItems} />)
        break;
      case 'edit': setModal(<EditDictionary handleClose={handleClose} getItems={getItems} dictionaryId={id} />)
        break;
      case 'delete': setModal(<DeleteDictionary handleClose={handleClose} getItems={getItems} dictionaryId={id} />)
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
    axios.get(`/dictionaries/`)
      .then(response => {
        const dictionaries = response.data;
        changeDictionaries(dictionaries);
      })
      .catch(error => {
        alert(error);
      })
  }
  useEffect(() => {
    getItems();
  }, [])

  return (
    <>
      <IconButton aria-label="add" onClick={() => handleClickOpen('add')} >
        <AddCircleTwoTone fontSize="large" style={{ color: blue[500] }} />
      </IconButton>
      <TableContainer component={Paper}>
        <Table aria-label="collapsible table">
          <TableHead>
            <TableRow>
              <TableCell />
              <TableCell>Name</TableCell>
              <TableCell>Secret Name</TableCell>
              <TableCell />
            </TableRow>
          </TableHead>
          <TableBody>
            {dictionaries.map((row) => (
              <Rows key={row.id} row={row} actions={handleClickOpen} setModal={setModal} setOpen={setOpen} handleClose={handleClose} />
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      <Dialog open={open} onClose={handleClose} aria-labelledby="form-dialog-title">
        {modal}
      </Dialog>
    </>
  );
}

export default Dictionaries;