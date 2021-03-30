import React, { useEffect, useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Stepper from '@material-ui/core/Stepper';
import Step from '@material-ui/core/Step';
import StepButton from '@material-ui/core/StepButton';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import Answer from './Answer';


const regexp = new RegExp('%%{[0-9]+}%%', 'g');

const useStyles = makeStyles((theme) => ({
  root: {
    width: '100%',
  },
  button: {
    marginRight: theme.spacing(1),
  },
  completed: {
    display: 'inline-block',
  },
  instructions: {
    marginTop: theme.spacing(1),
    marginBottom: theme.spacing(1),
  },
}));


const ExerciseStepper = ({ exercises }) => {
  const classes = useStyles();
  const [activeStep, setActiveStep] = React.useState(0);
  const [completed, setCompleted] = React.useState({});
  const steps = exercises.map(e => e.title);
  const [answers, setAnswers] = useState([]);
  const [result, setResult] = useState(0);

  const totalSteps = () => {
    return steps.length;
  };

  const getStepContent = (step) => (
    <>
      <div>{exercises[step]?.text.replaceAll(regexp, '______')}</div>
      <hr />
      {exercises.length ? exercises[step].answers.map(answ => {
        return (
          <>
            <Answer item={answ} exerciseId={exercises[step].id} answers={answers} setAnswers={setAnswers} />
            <br />
          </>);
      }) : "there are no exercises" }
    </>);

  const completedSteps = () => {
    return Object.keys(completed).length;
  };

  const isLastStep = () => {
    return activeStep === totalSteps() - 1;
  };

  const allStepsCompleted = () => {
    return completedSteps() === totalSteps();
  };

  const handleNext = () => {
    const newActiveStep =
      isLastStep() && !allStepsCompleted()
        ?
        steps.findIndex((step, i) => !(i in completed))
        : activeStep + 1;
    setActiveStep(newActiveStep);
  };

  const handleBack = () => {
    setActiveStep((prevActiveStep) => prevActiveStep - 1);
  };

  const handleStep = (step) => () => {
    setActiveStep(step);
  };

  const handleComplete = () => {
    const newCompleted = completed;
    newCompleted[activeStep] = true;
    let count  = 0;
    exercises[activeStep].answers.forEach((a) => {
      answers.forEach(a2 => {
        if(a.order === a2.order && a.text.toLowerCase() === a2.text.toLowerCase())
        {
          count +=1;
        }
      })
      
    })
    setResult( result + (count / exercises[activeStep].answers.length) );
    setCompleted(newCompleted);
    handleNext();
  };



  return (
    <div className={classes.root}>
      <Stepper nonLinear activeStep={activeStep}>
        {steps.map((label, index) => (
          <Step key={label}>
            <StepButton onClick={handleStep(index)} completed={completed[index]}>
              {label}
            </StepButton>
          </Step>
        ))}
      </Stepper>
      {exercises.length ? <div>
        {allStepsCompleted()  ? (
          <div>
            <Typography className={classes.instructions}>
              All steps completed - you&apos;re finished
              Your result is {Math.round((result / exercises.length) * 10)}
            </Typography>
          </div>
        ) : (
          <div>
            <Typography className={classes.instructions}>{getStepContent(activeStep)}</Typography>
            <div>
              <Button disabled={activeStep === 0} onClick={handleBack} className={classes.button}>
                Back
              </Button>
              <Button
                variant="contained"
                color="primary"
                onClick={() => {
                
                  handleNext();
                  }}
                className={classes.button}
              >
                Next
              </Button>
              {activeStep !== steps.length &&
                (completed[activeStep] ? (
                  <Typography variant="caption" className={classes.completed}>
                    Step {activeStep + 1} already completed
                  </Typography>
                ) : (
                  <Button variant="contained" color="primary" onClick={handleComplete}>
                    {completedSteps() === totalSteps() - 1 ? 'Finish' : 'Complete Step'}
                  </Button>
                ))}
            </div>
          </div>
        )}
      </div> : <div> There are no exercises </div>}
    </div>
  );
}

export default ExerciseStepper;