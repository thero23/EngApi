import React, { useState } from 'react';
import { InputAdornment, TextField } from '@material-ui/core';

const Answer = ({ setAnswers, answers, item }) => {
  const [text, setText] = useState(item.text || '');

  const onBlurHandler = () => {
    
    const index = answers.findIndex((obj => obj.order === item.order));
    const buff = [...answers];
    buff.splice(index, 1)
    buff.push({ order: item.order, text: text});
    setAnswers(buff.sort((a, b) => a.order - b.order)); 
  }
  return (
    <TextField
      key={item.order}
      InputProps={{
        startAdornment: <InputAdornment position="start">{item.order}</InputAdornment>,
      }}
      className="title-input"
      value={text}
      onChange={(e) => setText(e.target.value)}
      onBlur={() => onBlurHandler()}
      variant="outlined"
    />); 
}

export default Answer;