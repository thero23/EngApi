import React, { useState } from 'react';
import DeleteExercise from './DeleteExercise';
import AddExercise from './AddExercise';
import ExerciseDetails from './ExerciseDetails';
import ExercisesList from './ExercisesList';
import { Dialog } from '@material-ui/core';


const Exercises = (props) => {
  const [open, setOpen] = useState(false);
  const [modal, setModal] = useState({});
  const [page, setPage] = useState({
    name: 'list',
    exercise: null,
  });
  const handleClickOpen = (name, id, getItems) => {
    switch (name) {
      case 'delete': setModal(<DeleteExercise handleClose={handleClose} exerciseId={id} getItems={getItems}/>)
        break;
      default:
        break;
    }
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };



  const actionPage = () => {
    switch (page.name) {
      case 'details': return <ExerciseDetails exercise={page.exercise}  pageHandler={setPage} />
      case 'list': return <ExercisesList  handleClickOpen={handleClickOpen} pageHandler={setPage} />
      case 'add': return <AddExercise  pageHandler={setPage}  exercise={page.exercise} />
      default:
        return null;
    }
  }

    return (
      <>
        <div style={{ minHeight: '68vh'}}>
          {actionPage()}
        </div>
        <Dialog open={open} onClose={handleClose} aria-labelledby="form-dialog-title">
          {modal}
        </Dialog>
      </>
    );
  };
  export default Exercises;
