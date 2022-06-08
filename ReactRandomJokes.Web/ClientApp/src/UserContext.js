import React, { createContext, useContext, useEffect, useState } from 'react';
import axios from 'axios';

const UserContext = createContext();

const UserContextComponent = ({ children }) => {

    const [user, setUser] = useState(null);

    useEffect(() => {
        const getUser = async () => {
            const { data } = await axios.get('/api/account/getcurrentuser');
            setUser(data);
        }

        getUser();
    }, []);


    return <UserContext.Provider value={{ user, setUser }}>
        {children}
    </UserContext.Provider>

}

const useUserContext = () => useContext(UserContext);

export { UserContextComponent, useUserContext };