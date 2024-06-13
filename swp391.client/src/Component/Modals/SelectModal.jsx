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
import PetModal from './PetModal';
import AppointmentModal from './AppointmentModal';

function SelectModal({ toggleOpen }) {
    const [isPetModal, setIsPetModal] = useState(false);
    const [isAppModal, setIsAppModal] = useState(false);

    const choosePet = () => {
        setIsPetModal(!isPetModal);
    };
    const chooseApp = () => {
        setIsAppModal(!isAppModal);
    };
    if (isPetModal) {
        return (<PetModal></PetModal>);
    }
    if (isAppModal) {
        return (<AppointmentModal></AppointmentModal>);
    }
    return (
        <MDBModalDialog>
            <MDBModalContent>
                <MDBModalHeader>
                    <MDBModalTitle>Select which to create</MDBModalTitle>
                    <MDBBtn className='btn-close' color='none' onClick={toggleOpen}></MDBBtn>
                </MDBModalHeader>
                <MDBModalBody>
                    <MDBContainer>
                        <MDBRow>
                            <MDBCol className='mb-5'>
                                <MDBBtn onClick={choosePet} color='none'>
                                    PET
                                </MDBBtn>
                            </MDBCol>
                            <MDBCol className='mb-5'>
                                <MDBBtn onClick={chooseApp} color='none'>
                                    APPOINTMENT
                                </MDBBtn>
                            </MDBCol>
                        </MDBRow>
                    </MDBContainer>
                </MDBModalBody>

                <MDBModalFooter>
                    <MDBBtn color='secondary' onClick={toggleOpen}>
                        Close
                    </MDBBtn>
                </MDBModalFooter>
            </MDBModalContent>
        </MDBModalDialog>
    );
}

export default SelectModal;