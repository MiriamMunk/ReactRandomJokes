import React, { useState } from 'react';
import axios from 'axios';
import { Link, useHistory } from 'react-router-dom';
import { useUserContext } from '../UserContext';

const LogIn = () => {

    const history = useHistory();
    const [FormData, SetFormData] = useState({ Email: '', Password: '' });
    const [isValidLogin, SetIsValidLogIn] = useState(true);
    const { setUser } = useUserContext();

    const onTextChange = e => {
        const copy = { ...FormData };
        copy[e.target.name] = e.target.value;
        SetFormData(copy);
    }

    const onSubmitClick = async e => {
        e.preventDefault();
        const { data } = await axios.post('/api/account/logIn', FormData);
        const isValid = !!data;
        SetIsValidLogIn(isValid);
        if (isValid) {
            setUser(data);
            history.push('/');
        }
    }

    return (
        <div className="col-md-6 offset-md-3 card card-body bg-light">
            <h3>Log in to your account</h3>
            {!isValidLogin && <span className='text-danger'>Invalid username/password. Please try again.</span>}
            <form onSubmit={onSubmitClick} >
                <input type="text" name="email" placeholder="Email" className="form-control" onChange={onTextChange} />
                <br />
                <input type="password" name="password" placeholder="Password" className="form-control" onChange={onTextChange} />
                <br />
                <button className="btn btn-primary">Login</button>
            </form>
            <Link to="/signup">Sign up for a new account</Link>
        </div>)
}
export default LogIn;