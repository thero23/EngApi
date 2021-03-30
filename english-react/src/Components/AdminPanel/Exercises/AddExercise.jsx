import React, { useEffect, useState } from 'react';
import { Button, Paper, TextField } from '@material-ui/core';
import './style.css';
import Answer from './Answer';
import axios from '../../../axios';

const regexp = '%%{\\d+}%%';
const regexp2 = '\\d+';
const AddExercise = ({ pageHandler, exercise }) => {
  const [answers, setAnswers] = useState([]);
  const [text, setText] = useState('');
  const [title, setTitle] = useState('');
  const [counter, setCounter] = useState(1);

  useEffect(() => {
    if (exercise) {
      setAnswers(exercise.answers);
      setText(exercise.text);
      setTitle(exercise.title);
    }
  }, []);

  const getAnswers = () => {
    const results = [...text.matchAll(regexp)].flat().map((item) => {
      return {
        order: item.match(regexp2)[0],
        text: '',
      };
    })
    setAnswers(results);
  }

  const saveExercises = () => {
    const addedExercise = {
      title,
      text,
      answers,
    };
    axios.post('/exercises', addedExercise)
      .then(response => {
        pageHandler({
          name: 'list',
          exercise: null,
        })
      })
      .catch(error => {
        alert(error)
      })
  }

  const updateExercise = () => {
    const exerciseToUpdate = {
      id: exercise.id,
      title,
      text,
      answers,
    };
    axios.put('/exercises', exerciseToUpdate)
      .then(response => {
        pageHandler({
          name: 'details',
          exercise: response.data,
        })
      })
      .catch(error => {
        alert(error)
      })
  }
  return (
    <div className="exersiceEdit">
      <h1> {(!exercise && 'Add exercise') || ('Edit exercise')}</h1>
      <TextField
        id="outlined-multiline-static"
        label="Title"
        multiline
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        className="title-input"
        variant="outlined"
      />
      <TextField
        id="outlined-multiline-static"
        label="Text"
        value={text}
        onChange={(e) => {
          setText(e.target.value);
        }}
        multiline
        rowsMax={40}
        className="text-input"
        variant="outlined"
      />
      <Button onClick={() => {
        setAnswers([]);
        setCounter(1);
      }}>
        Clear answers
      </Button>
     <Paper>
     <Button onClick ={() => {
        setText(text.concat(`%%{${counter}}%%`));
      }}>
        Add
      </Button>
      <TextField
      type='number'
      value={counter}
      onChange={(e) => {
        setCounter(+e.target.value);
      }
      
      } />
     </Paper>
      <Button onClick={() => {
        getAnswers();
      }}>
        Add answers
      </Button>

      <div className="answers-wrapper">
        <div className="answer-inputs">
          <h2 align='left'>Answers:</h2>
          {(!!answers.length && answers.map((answer) => {
            return <Answer item={answer} setAnswers={setAnswers} answers={answers} />
          })) || ('There are no answers :C')}
        </div>

      </div>
      <Button
        color="primary"
        onClick={() => exercise ? updateExercise() : saveExercises()}
      >
        Save
      </Button>
      <Button
        color="primary"
        onClick={() => {
          if (!exercise) {
            pageHandler({
              name: 'list',
              exercise: null,
            })
          } else {
            pageHandler({
              name: 'details',
              exercise
            })
          }
        }}
      >
        Cancel
      </Button>
    </div >

  );
}

export default AddExercise;