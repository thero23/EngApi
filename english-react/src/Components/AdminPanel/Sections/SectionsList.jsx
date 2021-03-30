import React, { useState, useEffect } from 'react';
import axios from '../../../axios';
import IconButton from '@material-ui/core/IconButton';
import { AddCircleTwoTone, DeleteTwoTone, MoreTwoTone } from '@material-ui/icons';
import { red, blue } from '@material-ui/core/colors';
import { TableCell, TableHead, TableRow } from '@material-ui/core';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableContainer from '@material-ui/core/TableContainer';
import Paper from '@material-ui/core/Paper';
import SearchBar from '../SearchBar';

const ExercisesList = ({ handleClickOpen, pageHandler }) => {
  const [sections, changeSections] = useState([]);
  const [search, setSearch] = useState([]);
  const getItems = () => {
    axios.get('/sections')
      .then(response => {
        const items = response.data;
        setSearch(items);
        changeSections(items);
      })
      .catch(error => {
        alert(error);
      })
  }
  const searchFilter = (items, text) => {
    return items.filter(section => section.name.toLowerCase().includes(text.toLowerCase()));
  }
  useEffect(() => {
    getItems();
  }, [])

  return (
    <>
      <div className='panel-header'>
        <IconButton aria-label="add" onClick={() => pageHandler({
          name: 'add',
          exercise: null,
        })} >
          <AddCircleTwoTone fontSize="large" style={{ color: blue[500] }} />
        </IconButton>
        <SearchBar
          allItems={sections}
          setSearch={setSearch}
          filter={searchFilter}
        />
      </div>
      <TableContainer component={Paper}>
        <Table stickyHeader>
          <TableHead>
            <TableRow>
              <TableCell />
              <TableCell>Name</TableCell>
              <TableCell />
            </TableRow>
          </TableHead>
          <TableBody>
            {search.map((row) => (
              <TableRow key={row.id}>
                <TableCell />
                <TableCell>{row.name}</TableCell>
                <TableCell align="right">
                  <IconButton aria-label="details" onClick={() => pageHandler({
                    name: 'details',
                    section: row,
                  })}>
                    <MoreTwoTone fontSize="default" style={{ color: blue[500] }} />
                  </IconButton>
                  <IconButton aria-label="delete" onClick={() => handleClickOpen('delete', row.id, getItems)}>
                    <DeleteTwoTone fontSize="default" style={{ color: red[500] }} />
                  </IconButton>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </>
  );
}

export default ExercisesList;