import { useAuth } from "../Context/AuthProvider";
import { Navigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import React, { useState, useEffect } from 'react';
import { useUser } from "../Context/UserContext";

function CheckAuth({ children }) {
    const [isAuthenticated, setIsAuthenticated] = useAuth();
    const [user, setUser] = useUser();

    if (!isAuthenticated) {
        toast.info('Please login');
        switch (user?.role) {
            case 'Admin':
                return <Navigate to="/admin/customers"></Navigate>;
            case 'Customer':
                return <Navigate to="/"></Navigate>;
            case 'Vet':
                return <Navigate to="/login" />;
            case 'Staff':
                return <Navigate to="/" />;
        }
    }
    else return (<>{children}</>);

}
export default CheckAuth;