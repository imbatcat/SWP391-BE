import {
    MDBBtn,
    MDBModalBody,
    MDBModalContent,
    MDBModalDialog,
    MDBModalHeader,
    MDBModalTitle
} from 'mdb-react-ui-kit';
import AssignServiceForm from './AssignServiceForm';

function AssignServiceModal({ petData, ownerData, vetData, toggleOpen }) {

    return (
        <>
            <MDBModalDialog style={{ minWidth: 'fit-content' }}>
                <MDBModalContent>
                    <MDBModalHeader >
                        <MDBModalTitle>Assign Medical Service Form</MDBModalTitle>
                        <MDBBtn className='btn-close' color='none' onClick={toggleOpen}></MDBBtn>
                    </MDBModalHeader>
                    <MDBModalBody>
                        <AssignServiceForm petData={petData} ownerData={ownerData} vetData={vetData} toggleOpen={toggleOpen} />
                    </MDBModalBody>
                </MDBModalContent>
            </MDBModalDialog>
        </>
    );
}
export default AssignServiceModal;