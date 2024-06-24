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
    MDBModalFooter
} from 'mdb-react-ui-kit';
import { useState } from 'react';
import VetModal from './VetModal';
import StaffModal from './StaffModal';
import CheckAuth from '../../Helpers/CheckAuth';

function SelectAccountModal({ toggleOpen }) {
    const [isVetModal, setIsVetModal] = useState(false);
    const [isStaffModal, setIsStaffModal] = useState(false);

    const chooseVet = () => {
        setIsVetModal(!isVetModal);
    };
    const chooseStaff = () => {
        setIsStaffModal(!isStaffModal);
    };
    if (isVetModal) {
        return (<VetModal></VetModal>);
    }
    if (isStaffModal) {
        return (<StaffModal></StaffModal>);
    }
    return (
        <CheckAuth>
            <MDBModalDialog>
                <MDBModalContent>
                    <MDBModalHeader>
                        <MDBModalTitle style={{ fontSize: '24px' }}>Select which to create</MDBModalTitle>
                        <MDBBtn className='btn-close' color='none' onClick={toggleOpen} />
                    </MDBModalHeader>
                    <MDBModalBody>
                        <MDBContainer>
                                <MDBCol className='mb-5'>
                                    <MDBBtn onClick={chooseVet} color='none'>Veterinarian</MDBBtn>
                                </MDBCol>
                                <MDBCol className='mb-5'>
                                    <MDBBtn onClick={chooseStaff} color='none'>Staff</MDBBtn>
                                </MDBCol>
                        </MDBContainer>
                    </MDBModalBody>

                    <MDBModalFooter>
                        <MDBBtn color='secondary' onClick={toggleOpen}>Close</MDBBtn>
                    </MDBModalFooter>
                </MDBModalContent>
            </MDBModalDialog>
        </CheckAuth>
    );
}

export default SelectAccountModal;