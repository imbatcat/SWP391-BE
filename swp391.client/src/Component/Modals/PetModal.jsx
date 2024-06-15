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
import PetModalForm from './PetModalForm';

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
                        <PetModalForm></PetModalForm>
                    </MDBModalBody>
                </MDBModalContent>
            </MDBModalDialog>
        </>
    );
}

export default PetModal;