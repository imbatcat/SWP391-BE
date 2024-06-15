import React, { createContext, useState, useEffect, useContext } from 'react';

// Create the context
const UserContext = createContext();

// Provide context to the component tree
const UserProvider = ({ children }) => {
    const [user, setUser] = useState(() => {
        const data = localStorage.getItem("user");
        if (data) {
            const parseData = JSON.parse(data);

            var parsedData = {
                id: parseData.id,
                role: parseData.role,
            };
            console.log(parsedData);
            return parsedData;
        }
        return null;
    });

    return (
        <UserContext.Provider value={[user, setUser]}>
            {children}
        </UserContext.Provider>
    );
};

// Custom hook to use the UserContext
const useUser = () => useContext(UserContext);
export { useUser, UserProvider };
