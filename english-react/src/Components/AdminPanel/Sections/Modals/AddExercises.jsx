import React, { useEffect, useState } from 'react';
import Button from '@material-ui/core/Button';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import { IconButton, Paper } from '@material-ui/core';
import axios from '../../../../axios';
import { AddCircleTwoTone } from '@material-ui/icons';
import { blue } from '@material-ui/core/colors';

const AddExercises = ({ handleClose, subsectionId }) => {
  const [exercises, setExersises] = useState([]);

  const getExersises = () => {
    axios.get(`subsections/${subsectionId}/notexercises`)
      .then((response) => {
        setExersises(response.data)
      }).catch(error => {
        alert(error)
      })
  }

  const addExercise = (id) =>{
    axios.post(`subsections/${subsectionId}/exercise/${id}`)
      .then(() => {
        getExersises();
      }).catch(error => {
        alert(error)
      })
  }

  useEffect(() => {
    getExersises();
  }, [])
  return (
    <div>
      <DialogTitle id="alert-dialog-title">
        Add exercises
        </DialogTitle>
      <DialogContent>
        {exercises.map((ex) => {
          return (
            <Paper key={ex.id}>
              {ex.title}
              <IconButton aria-label="add" onClick={() => addExercise(ex.id)} >
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

export default AddExercises;