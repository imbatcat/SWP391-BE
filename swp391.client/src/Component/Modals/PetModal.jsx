import {
    MDBBtn,
    MDBModal,
    MDBModalDialog,
    MDBModalContent,
    MDBContainer,
    MDBRow,
    MDBCol,
    MDBModalHeader,
    MDBModalTitle,
    MDBModalBody,
    MDBModalFooter,
} from 'mdb-react-ui-kit';
import { useState } from 'react';
import AppointmentForm from './AppointmentModalForm';

function PetModal({ toggleOpen }) {
    return (
        <>
            <MDBModalDialog>
                <MDBModalContent>
                    <MDBModalHeader >
                        <MDBModalTitle>Pet Information</MDBModalTitle>
                        <MDBBtn className='btn-close' color='none' onClick={toggleOpen}></MDBBtn>
                    </MDBModalHeader>
                    <MDBModalBody>
                        <AppointmentForm></AppointmentForm>
                    </MDBModalBody>
                </MDBModalContent>
            </MDBModalDialog>
        </>
    );
}

export default PetModal;