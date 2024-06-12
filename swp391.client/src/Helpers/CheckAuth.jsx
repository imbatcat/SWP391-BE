import { useAuth } from "../Context/AuthProvider";
import { Navigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import React, { useState, useEffect } from 'react';

function CheckAuth({ children }) {
    const [isAuthenticated, setIsAuthenticated] = useAuth();
    //const [isToasted, setIsToasted] = useState(false);

    if (!isAuthenticated) {
        //if (!isToasted) {
        toast.info('Please login');
        //setIsToasted(true);
        //}
        return <Navigate to="/" />;
    }

    return children;
}
export default CheckAuth;