import React, { createContext, useState, useEffect, useContext } from 'react';
import Cookies from 'js-cookie';

// Create the context
const UserContext = createContext();

// Provide context to the component tree
const UserProvider = ({ children }) => {
    const [user, setUser] = useState({
        id: '',
        role: '',
    });

    useEffect(() => {
        const data = localStorage.getItem("user");
        if (data) {
            const parseData = JSON.parse(data);
            setUser({
                id: parseData.id,
                role: parseData.role,
            });
        }
    }, []);
    return (
        <UserContext.Provider value={[user, setUser]}>
            {children}
        </UserContext.Provider>
    );
};

// Custom hook to use the UserContext
const useUser = () => useContext(UserContext);
export { useUser, UserProvider };
