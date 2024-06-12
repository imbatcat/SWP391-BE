import {
    MDBBtn,
    MDBModalBody,
    MDBModalContent,
    MDBModalDialog,
    MDBModalHeader,
    MDBModalTitle
} from 'mdb-react-ui-kit';
import CreateModalForm from './CreateModalForm';

function CreateModal({ toggleOpen }) {

    return (
        <>
            <MDBModalDialog>
                <MDBModalContent>
                    <MDBModalHeader >
                        <MDBModalTitle>Appointment Information</MDBModalTitle>
                        <MDBBtn className='btn-close' color='none' onClick={toggleOpen}></MDBBtn>
                    </MDBModalHeader>
                    <MDBModalBody>
                        <CreateModalForm/>
                    </MDBModalBody>
                </MDBModalContent>
            </MDBModalDialog>
        </>
    );
}
export default CreateModal;