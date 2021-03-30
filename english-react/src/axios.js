import axios from 'axios';

const instance = axios.create({
     baseURL:'https://arreis.ru:2540/api/' 
    /*  baseURL: 'https://127.0.0.1:5001/api/'  */
});

export default instance;