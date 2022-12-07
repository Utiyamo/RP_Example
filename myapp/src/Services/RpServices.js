'use strict'

const url = process.env.REACT_APP_SERVICES_URI ? process.env.REACT_APP_SERVICES_URI : 'http://localhost:44369';
export class RpServices{
    async getUsers(user){
        return axios.get(url + '/')
    }
}