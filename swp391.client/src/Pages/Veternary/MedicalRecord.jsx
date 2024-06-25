import React, { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import {
    MDBCard,
    MDBCardBody,
    MDBCardTitle,
    MDBCardText,
    MDBRow,
    MDBCol,
    MDBBtn,
    MDBCardHeader
} from 'mdb-react-ui-kit';

async function fetchOwnerAndPetData(accountId, petId) {
    try {
        const [accountResponse, petResponse] = await Promise.all([
            fetch(`https://localhost:7206/api/Accounts/${accountId}`, {
                method: 'GET',
                credentials: 'include',
            }),
            fetch(`https://localhost:7206/api/Pets/${petId}`, {
                method: 'GET',
                credentials: 'include',
            })
        ]);

        if (!accountResponse.ok || !petResponse.ok) {
            throw new Error("Error fetching data");
        }

        const accountData = await accountResponse.json();
        const petData = await petResponse.json();

        return { accountData, petData };
    } catch (error) {
        console.error(error.message);
        return { accountData: null, petData: null };
    }
}

function MedicalRecord() {
    const location = useLocation();
    const appointment = location.state;
    const [ownerData, setOwnerData] = useState(null);
    const [petData, setPetData] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    
    useEffect(() => {
        console.log(appointment);
        if (appointment) {
            fetchOwnerAndPetData(appointment.accountId, appointment.petId)
                .then(({ accountData, petData }) => {
                    setOwnerData(accountData);
                    setPetData(petData);
                    setLoading(false);
                })
                .catch((error) => {
                    setError(error.message);
                    setLoading(false);
                });
        } else {
            setError("No appointment data provided");
            setLoading(false);
        }
    }, [appointment]);

    if (loading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>Error: {error}</div>;
    }

    if (!ownerData || !petData) {
        return <div>No data available</div>;
    }

    return (
        <MDBCard style={{ minHeight: '60vw', maxWidth: '50vw', margin: 'auto', marginTop: '50px' }}>
            <MDBCardHeader style={{ textAlign: 'center', fontSize: '3vw' }}>Medical Record</MDBCardHeader>
            <MDBCardBody>
                <MDBRow style={{ marginLeft: '15px', marginRight: '15px' }}>
                    <MDBCol sm='6'>
                        <MDBCard>
                            <MDBCardBody>
                                <MDBCardTitle>Owner Information</MDBCardTitle>
                                <MDBCardText>
                                    <p>Owner Name: {ownerData.name}</p>
                                    <p>Owner Number: {ownerData.phoneNumber}</p>
                                </MDBCardText>
                            </MDBCardBody>
                        </MDBCard>
                    </MDBCol>
                    <MDBCol sm='6'>
                        <MDBCard>
                            <MDBCardBody>
                                <MDBCardTitle>Pet Information</MDBCardTitle>
                                <MDBCardText>
                                    <p>Pet Name: {petData.name}</p>
                                    <p>Pet Breed: {petData.breed}</p>
                                </MDBCardText>
                            </MDBCardBody>
                        </MDBCard>
                    </MDBCol>
                </MDBRow>
            </MDBCardBody>
        </MDBCard>
    );
}

export default MedicalRecord;
