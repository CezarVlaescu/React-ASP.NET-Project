import axios, { AxiosError, AxiosResponse } from "axios";
// import { toast } from "react-toastify";


axios.defaults.baseURL = 'http://localhost:5054/api/';

const responseBody = (response : AxiosResponse) => response.data;

axios.interceptors.response.use(response => {
    return response
}, (error: AxiosError) => {
    // const {data, status} = error.response;
    // switch(status){
    //     case 400:
    //         toast.error(data.title);
    //         break;
    //     case 401:
    //         toast.error(data.title);
    //         break;
    //     case 500:
    //         toast.error(data.title);
    //         break;
    //     default : break;
    // }
    return Promise.reject(error.response)
})

const requests = {
    get : (url : string) => axios.get(url).then(responseBody),
    post : (url : string, body: {}) => axios.get(url, body).then(responseBody),
    put : (url : string, body: {}) => axios.get(url, body).then(responseBody),
    delete : (url : string) => axios.get(url).then(responseBody),
}

const Catalog = {
    list : () => requests.get('products'),
    details : (id:number) => requests.get(`products/${id}`)
}

const TestErrors = {
    get400Error : () => requests.get('buggy/bad-request'),
    get401Error : () => requests.get('buggy/unathorised'),
    get404Error : () => requests.get('buggy/not-found'),
    get500Error : () => requests.get('buggy/server-error'),
    getValidationError : () => requests.get('buggy/validation-error'),
}

const agent = {
    Catalog,
    TestErrors
}

export default agent;
