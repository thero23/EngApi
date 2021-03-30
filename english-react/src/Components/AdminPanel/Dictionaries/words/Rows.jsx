import React, { useEffect, useState } from 'react';
import Box from '@material-ui/core/Box';
import Collapse from '@material-ui/core/Collapse';
import IconButton from '@material-ui/core/IconButton';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Typography from '@material-ui/core/Typography';
import KeyboardArrowDownIcon from '@material-ui/icons/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@material-ui/icons/KeyboardArrowUp';
import axios from '../../../../axios';
import { AddCircleTwoTone, DeleteTwoTone, EditTwoTone } from '@material-ui/icons';
import { red, yellow, blue } from '@material-ui/core/colors';
import { makeStyles } from '@material-ui/core';
import DeleteWord from './DeleteWord';
import EditWord from './EditWord';
import AddWord from './AddWord';

const useRowStyles = makeStyles({
  root: {
    '& > *': {
      borderBottom: 'unset',
    },
  },
});

const Rows = (props) => {
  const { dictionary } = props;
  const [open, setOpen] = useState(false);
  const classes = useRowStyles();

  const [words, changeWords] = useState([]);
  useEffect(() => {
    getItems();
  }, [])

  const getItems = () => {
    axios.get(`/dictionaries/${dictionary.id}/words`)
      .then(response => {
        const words = response.data;
        changeWords(words);
      })
      .catch(error => {
        alert(error);
      })
  }

  const handleClickOpen = (name, id, dictionaryId) => {
    switch (name) {
      case 'add': props.setModal(<AddWord handleClose={props.handleClose} getItems={getItems} dictionaryId={dictionaryId} />)
        break;
      case 'edit': props.setModal(<EditWord handleClose={props.handleClose} getItems={getItems} wordId={id} />)
        break;
      case 'delete': props.setModal(<DeleteWord handleClose={props.handleClose} getItems={getItems} wordId={id}  dictionaryId={dictionaryId} />)
        break;
      default:
        break;
    }
    props.setOpen(true);
  };

  useEffect(() => {
    getItems();
  }, [])

  return (
    <React.Fragment>
      <TableRow className={classes.root}>
        <TableCell>
          <IconButton aria-label="expand row" size="small" onClick={() => setOpen(!open)}>
            {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
          </IconButton>
        </TableCell>
        <TableCell>{dictionary.name}</TableCell>
        <TableCell>{dictionary.secretName}</TableCell>
        <TableCell align="right">
          <IconButton aria-label="edit" onClick={() => props.actions('edit', dictionary.id)}>
            <EditTwoTone fontSize="large" style={{ color: yellow[500] }} />
          </IconButton>
          <IconButton aria-label="delete" onClick={() => props.actions('delete', dictionary.id)}>
            <DeleteTwoTone fontSize="large" style={{ color: red[500] }} />
          </IconButton>
        </TableCell>
      </TableRow>
      <TableRow>
        <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <Box margin={1}>
              <Typography variant="h6" gutterBottom component="div">
                Words
              </Typography>
              <Table size="small" aria-label="purchases">
                <TableHead>
                  <TableRow>
                    <TableCell></TableCell>
                    <TableCell>Original</TableCell>
                    <TableCell>Translate</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  <IconButton aria-label="add" onClick={() => handleClickOpen('add', null, dictionary.id)} >
                    <AddCircleTwoTone fontSize="small" style={{ color: blue[500] }} />
                  </IconButton>
                  {words.map((word) => (
                    <TableRow key={word.id}>
                      <TableCell></TableCell>
                      <TableCell>{word.original}</TableCell>
                      <TableCell>{word.translate}</TableCell>
                      <TableCell align="right">
                        <IconButton aria-label="edit" onClick={() => handleClickOpen('edit', word.id) }>
                          <EditTwoTone fontSize="small" style={{ color: yellow[500] }} />
                        </IconButton>
                        <IconButton aria-label="delete" onClick={() => handleClickOpen('delete', word.id, dictionary.id)}>
                          <DeleteTwoTone fontSize="small" style={{ color: red[500] }} />
                        </IconButton>
                      </TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </Box>
          </Collapse>
        </TableCell>
      </TableRow>
    </React.Fragment>
  );
}

export default Rows;