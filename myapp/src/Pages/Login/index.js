import React, { useState } from 'react';

import { alertEnum } from '../../Components/AlertsComponents/AlertServices';

import './tstyle.css';

export default function Login() {
    const [user, setUser] = useState();

    const handleChange = (event) => {
        const aux = {...user};
        aux[event.target.name] = event.target.value;
        setUser(aux);
    };

    const handleSubmit = (event) => {
        const message = 'Json User ' + JSON.stringify(user);
        console.log(message);
    }

    const fetchData = async () => {
        try{
            
        }
    }

    return (
        <div className='login'>
            <form className='login-form'>
                <h1>Login</h1>
                <div className='form-input-material'>
                    <label htmlFor='username'>Username</label><br/>
                    <input type='text' name='username' id='username' placeholder=' ' autoComplete='off' className='form-control-material' onChange={handleChange} required />
                </div>
                <div className='form-input-material'>
                    <label htmlFor='password'>Password</label><br/>
                    <input type='password' name='password' id='password' placeholder=' ' autoComplete='off' className='form-control-material' onChange={handleChange} required />
                </div>
                <button type='button' className='btn btn-primary btn-ghost' onClick={handleSubmit}>Login</button>
            </form>
        </div>
    );
}