import {
    MDBBtn,
    MDBModal,
    MDBModalBody,
    MDBModalContent,
    MDBModalDialog,
    MDBModalHeader,
    MDBModalTitle
} from 'mdb-react-ui-kit';
import { useEffect, useState } from 'react';
import AppointmentForm from '../Modals/AppointmentModalForm';
import PaymentModal from './PaymentModal';
import DotsMobileStepper from './Stepper';


function AppointmentModal({ toggleOpen }) {
    // This represents the step count from stepper
    const [stepData, setStepData] = useState(0);
    const handleStepChange = (data) => {
        setStepData(data);
    };

    return (
        <>
            <MDBModalDialog>
                <MDBModalContent>
                    {stepData == 0 && <AppointmentForm toggleOpen={toggleOpen}></AppointmentForm>}
                    {stepData == 1 && <PaymentModal toggleOpen={toggleOpen}></PaymentModal>}
                    {stepData == 2 && <>helo</>}

                    <DotsMobileStepper onStepChange={handleStepChange}></DotsMobileStepper>

                </MDBModalContent>
            </MDBModalDialog>
        </>);
}
export default AppointmentModal;