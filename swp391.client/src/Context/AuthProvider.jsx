import React, { createContext, useState, useEffect, useContext } from 'react';
import Cookies from 'js-cookie';

// Create the context
const AuthContext = createContext();

// Provide context to the component tree
const AuthProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(() => {
        var userToken = Cookies.get('AspNetLogin');
        return userToken ? true : false;
    });

    return (
        <AuthContext.Provider value={[isAuthenticated, setIsAuthenticated]}>
            {children}
        </AuthContext.Provider>
    );
};

// Custom hook to use the AuthContext
const useAuth = () => useContext(AuthContext);
export { useAuth, AuthProvider };
