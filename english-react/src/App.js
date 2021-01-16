import './App.css';

import React from 'react';
import Layout from './Components/Layout/Layout';
import { BrowserRouter } from 'react-router-dom';
import { RecoilRoot} from 'recoil';
import axios from './axios';

axios.interceptors.request.use(
  (request) => {
    request.headers = {
      Authorization: `Bearer ${localStorage.getItem('TOKEN')}`,
    };
    return request;
  },
  (error) => error,
);



function App() {
  return (
    <BrowserRouter>
      <RecoilRoot>
        <div className="App">
          <Layout />
        </div>
      </RecoilRoot>
    </BrowserRouter>
  );
}

export default App;
