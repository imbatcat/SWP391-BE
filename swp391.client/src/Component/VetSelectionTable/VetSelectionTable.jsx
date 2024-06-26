import React, { useState, useEffect } from 'react';
import { MDBBtn, MDBTable, MDBTableBody, MDBTableHead } from 'mdb-react-ui-kit';

const VetSelectionTable = ({ vetList, formData, handleChange }) => {
    const [selectedVet, setSelectedVet] = useState(formData.veterinarianAccountId || '');

    const handleVetSelect = (vetAccountId) => {
        console.log(vetAccountId);
        setSelectedVet(vetAccountId);
        handleChange({ target: { name: 'veterinarianAccountId', value: vetAccountId } });
    };

    useEffect(() => {
        setSelectedVet(formData.veterinarianAccountId);
    }, [formData.veterinarianAccountId]);

    return (
        <MDBTable bordered>
            <MDBTableHead>
                <tr>
                    <th>Select</th>
                    <th>Veterinarian Name</th>
                </tr>
            </MDBTableHead>
            <MDBTableBody>
                {vetList.map((vet, index) => (
                    <tr key={index}>
                        <td>
                            <MDBBtn
                                type='button'
                                color={selectedVet === vet.accountId ? 'primary' : 'secondary'}
                                onClick={() => handleVetSelect(vet.accountId)}
                            >
                                {selectedVet === vet.accountId ? 'Selected' : 'Select'}
                            </MDBBtn>
                        </td>
                        <td>{vet.fullName}</td>
                    </tr>
                ))}
            </MDBTableBody>
        </MDBTable>
    );
};

export default VetSelectionTable;
