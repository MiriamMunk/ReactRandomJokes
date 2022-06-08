import React, { useState } from 'react';
import axios from 'axios';
import { useHistory } from "react-router-dom";

const SignUp = () => {

    const history = useHistory();
    const [User, setUser] = useState({ firstName: '', lastName: '', email: '', password: '' });

    const onTextChange = e => {
        const copy = { ...User };
        copy[e.target.name] = e.target.value;
        setUser(copy);
    }

    const onSubmitClick = async e  => {
        e.preventDefault();
        await axios.post('/api/account/signUp', User);
        history.push('/login');
    }

    return (<div className="col-md-6 offset-md-3 card card-body bg-light">
        <h3>Sign up for a new account</h3>
        <form onSubmit={onSubmitClick}>
            <input type="text" name="firstName" placeholder="First Name" className="form-control" onChange={onTextChange} />
            <br />
            <input type="text" name="lastName" placeholder="Last Name" className="form-control" onChange={onTextChange} />
            <br />
            <input type="text" name="email" placeholder="Email" className="form-control" onChange={onTextChange} />
            <br />
            <input type="password" name="password" placeholder="Password" className="form-control" onChange={onTextChange} />
            <br />
            <button className="btn btn-primary">Signup</button>
        </form>
    </div>)
}
export default SignUp;