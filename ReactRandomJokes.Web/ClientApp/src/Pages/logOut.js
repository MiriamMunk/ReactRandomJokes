import axios from 'axios';
import React, { useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { useUserContext } from '../UserContext';

const Logout = () => {

    const history = useHistory();
    const { setUser } = useUserContext();

    useEffect(() => {
        const doLogout = async () => {
            setUser(null);
            await axios.post('/api/account/logout');
        }

        doLogout();
        history.push('/login');
    });

    return <></>;
}

export default Logout;