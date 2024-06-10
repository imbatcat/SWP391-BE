import { useAuth } from "../Context/AuthProvider";
import { useNavigate, useLocation } from 'react-router-dom';
import { toast } from 'react-toastify';
import React, { useState, useEffect } from 'react';

function CheckAuth() {
    const [isAuthenticated, setIsAuthenticated] = useAuth();
    const navigate = useNavigate();
    const location = useLocation();

    useEffect(() => {
        console.log(isAuthenticated);
        if (!isAuthenticated) {
            toast.info('User session expired, please login');
            navigate('/login');
        }
    }, [])
    return (
        <></>
    );
}

export default CheckAuth;