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
import StaffModalForm from './StaffModalForm';

function StaffModal({ toggleOpen }) {
    return (
        <>
            <MDBModalDialog>
                <MDBModalContent>
                    <MDBModalHeader >
                        <MDBModalTitle>Staff Information</MDBModalTitle>
                        <MDBBtn className='btn-close' color='none' onClick={toggleOpen}></MDBBtn>
                    </MDBModalHeader>
                    <MDBModalBody>
                        <StaffModalForm></StaffModalForm>
                    </MDBModalBody>
                </MDBModalContent>
            </MDBModalDialog>
        </>
    );
}

export default StaffModal;