import {
    atom
  } from 'recoil';


  const isAuthState = atom({
    key: 'isAuthState',
    default: null
  });

  export default isAuthState;