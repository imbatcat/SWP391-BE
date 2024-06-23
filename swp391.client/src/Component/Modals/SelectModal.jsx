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
    MDBCardBody,
    MDBCard,
    MDBCardHeader,
} from 'mdb-react-ui-kit';
import { useState } from 'react';
import PetModal from './PetModal';
import AppointmentModal from './AppointmentModal';
import CheckAuth from '../../Helpers/CheckAuth';
import img3 from '../../assets/images/hero3.png'

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
        <CheckAuth>
            <MDBModalDialog>
                <MDBModalContent>
                    <MDBModalHeader>
                        <MDBModalTitle style={{ fontSize: '24px' }}>Select which to create</MDBModalTitle>
                        <MDBBtn className='btn-close' color='none' onClick={toggleOpen}></MDBBtn>
                    </MDBModalHeader>
                    <MDBModalBody>
                        <MDBContainer>
                            <MDBRow>
                                <MDBCol className='mb-5'>
                                    <MDBCard alignment='center'>
                                        <MDBCardHeader>Create New Pet</MDBCardHeader>
                                            <MDBCardBody>
                                            <img src={img3} className='w-100' alt='...' />
                                                <MDBBtn onClick={choosePet} color='danger'>
                                                    PET
                                                </MDBBtn>
                                            </MDBCardBody>
                                        
                                    </MDBCard>
                                    
                                </MDBCol>
                                <MDBCol  className='mb-5 col-6'>
                                    <MDBBtn onClick={chooseApp} color='danger'>
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
        </CheckAuth >

    );
}

export default SelectModal;