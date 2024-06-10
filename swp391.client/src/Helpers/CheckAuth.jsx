import { useAuth } from "../Context/AuthProvider";
import { Navigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import React, { useState, useEffect } from 'react';

function CheckAuth({ children }) {
    const [isAuthenticated, setIsAuthenticated] = useAuth();

    if (!isAuthenticated) {
        toast.info('User session expired, please login');
        return <Navigate to="/" />;
    }

    return children;
}
export default CheckAuth;