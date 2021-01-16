import axios from 'axios';



const instance = axios.create({
    baseURL:'https://10.188.8.26:5001/api/' 
});

export default instance;