import { TextField } from '@material-ui/core'
import React, { useEffect, useState } from 'react'
import './style.css';

const SearchBar = ({ allItems, setSearch, filter }) => {
  const [text, setText] = useState('');

  useEffect(() => {
    if (text.length === 0) setSearch(allItems)
    else {
      const items = filter(allItems, text);
      setSearch(items);
    }
  }, [text, allItems])
  return (
    <div className='search-bar-wrapper'>
      <TextField
        variant='outlined'
        placeholder='Search'
        value={text}
        onChange={(event) => setText(event.target.value)}
      />
    </div>
  )
}

export default SearchBar;