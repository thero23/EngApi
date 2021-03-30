import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import { Button } from '@material-ui/core';

const regexp = new RegExp('%%{[0-9]+}%%', 'g');

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
  },
  paper: {
    padding: theme.spacing(2),
    textAlign: 'center',
    color: theme.palette.text.secondary,
    margin: 3,
  },
}));

const ExerciseDetails = ({ exercise, pageHandler }) => {
  const classes = useStyles();
  return (
    <div aclassName={classes.root}>
      <h1>{exercise.title}</h1>
      <Grid container spacing={3}>
        <Grid item xs={12}>
          <Paper className={classes.paper}>{exercise.text.replaceAll(regexp, '______')}</Paper>
        </Grid>
        <Grid item xs={9}>
          {exercise.answers.map((answer) => {
            return (<Paper key={answer.id} elevation={3} className={classes.paper}>{answer.text}</Paper>);
          })}
        </Grid>
      </Grid>
      <Button
        onClick={() => pageHandler({
          name: 'add',
          exercise
        })} color="primary" autoFocus>
        Edit
      </Button>
      <Button
        onClick={() => pageHandler({
          name: 'list',
          exercise: null,
        })} color="primary" autoFocus>
        Back to exercises
      </Button>
    </div>
  );
}

export default ExerciseDetails;