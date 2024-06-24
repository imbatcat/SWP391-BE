import {
    MDBBtn,
    MDBModal,
    MDBModalBody,
    MDBModalContent,
    MDBModalDialog,
    MDBModalHeader,
    MDBModalTitle,
    MDBModalFooter
} from 'mdb-react-ui-kit';
import { useEffect, useState } from 'react';

function PaymentModal({ toggleOpen }) {

    return (
        <>
            <MDBModalHeader >
                <MDBModalTitle style={{ fontSize: '24px' }}>Payment</MDBModalTitle>
                <MDBBtn className='btn-close' color='none' onClick={toggleOpen}></MDBBtn>
            </MDBModalHeader>
            <MDBModalBody>

            </MDBModalBody>
        </>
    );
}
export default PaymentModal;