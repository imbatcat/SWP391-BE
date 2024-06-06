import React, { useEffect, useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

const ConfirmEmail = () => {
    const navigate = useNavigate();
    const query = useQuery();
    const [confirmationResult, setConfirmationResult] = useState(null);

    useEffect(() => {
        const userId = query.get('userId');
        const token = query.get('token');

        if (userId && token) {
            confirmEmail(userId, token);
        }
    }, [query]);

    const confirmEmail = async (userId, token) => {
        try {
            const response = await fetch(`https://localhost:7206/api/ApplicationAuth/confirmemail?userId=${userId}&token=${token}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                },
                credentials: 'include', // Include credentials if needed
            });
            if (response.ok) {
                navigate('/');
            }
        } catch (error) {
            console.error('Error confirming email:', error);
        }
    };

    return (
        <div>
            Please wait while we are confirming your email, you will be redirected to homepage shortly...
        </div>
    );
};

export default ConfirmEmail;
