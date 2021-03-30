import React, { useEffect, useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import { Accordion, AccordionActions, AccordionSummary, Button, Container, TextField, Typography } from '@material-ui/core';
import axios from '../../../../axios';



const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
    width: '100%',
  },
  paper: {
    padding: theme.spacing(2),
    textAlign: 'center',
    color: theme.palette.text.secondary,
    margin: 3,
  },
  heading: {
    fontSize: theme.typography.pxToRem(15),
  },
  secondaryHeading: {
    fontSize: theme.typography.pxToRem(15),
    color: theme.palette.text.secondary,
  },
  icon: {
    verticalAlign: 'bottom',
    height: 20,
    width: 20,
  },
  details: {
    alignItems: 'center',
  },
  column: {
    flexBasis: '33.33%',
  },
  helper: {
    borderLeft: `2px solid ${theme.palette.divider}`,
    padding: theme.spacing(1, 2),
  },
  link: {
    color: theme.palette.primary.main,
    textDecoration: 'none',
    '&:hover': {
      textDecoration: 'underline',
    },
  },
}));

const AddSection = ({ pageHandler, handleClickOpen }) => {
  const [section, setSection] = useState({
    name: '',
    order: 1,
  });

  const addSection = () => {
    axios.post('/sections', section)
      .then(response => {
        pageHandler({
          name: 'list',
          section: null,
        })
      })
      .catch(error => {
        alert(error);
      })
  }
  return (
    <Grid container xs={12} spacing={3}>
      <Grid item xs={12}>
        <TextField
          value={section.name}
          onChange={(e) => setSection({
            ...section,
            name: e.target.value,
          })}
          required
          id="outlined-multiline-static"
          label="Title"
          variant="outlined"
        />
        
      </Grid>
      <Grid item xs={12}>
        <TextField
          value={section.order}
          onChange={(e) => setSection({
            ...section,
            order: +e.target.value < 1 ? 1 : +e.target.value ,
          })}
          type="number"
          required
          id="outlined-multiline-static"
          label="Order"
          variant="outlined"
        />
        
      </Grid>
      <Button
        onClick={() => pageHandler({
          name: 'list',
          section: null,
        })} color="primary">
        Back to sections
      </Button>
      <Button
        onClick={() => addSection()} color="primary" autoFocus>
        Save
      </Button>
    </Grid>
  )
}


export default AddSection;
