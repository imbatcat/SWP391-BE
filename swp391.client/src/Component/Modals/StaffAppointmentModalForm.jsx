import {
    MDBBtn,
    MDBCheckbox,
    MDBCol,
    MDBInput,
    MDBRow
} from 'mdb-react-ui-kit';

function StaffAppointmentModalForm() {
    return (
        <form>
            <MDBRow className='mb-4'>
                <MDBCol>
                    <MDBInput id='ownerName' label='Owner Name' />
                </MDBCol>
                <MDBCol>
                    <MDBInput id='phone' label='Phone Number' type='tel' />
                </MDBCol>
            </MDBRow>

            <MDBRow className='mb-4'>
                <MDBCol>
                    <MDBInput id='petName' label='Pet Name' />
                </MDBCol>
                <MDBCol>
                    <MDBInput id='petAge' label='Pet Age' type='number' min='0' />
                </MDBCol>
                <MDBCol>
                    <MDBInput id='petAge' label='Pet breed' type='text' />
                </MDBCol>
                <MDBCol>
                    <MDBCheckbox label="Is male"></MDBCheckbox>
                </MDBCol>

            </MDBRow>
            <MDBRow className='mb-4'>
                <MDBCol>
                    <MDBInput id='timeSlot' label='Meet time' type='time' defaultValue={'00:00'}></MDBInput>
                </MDBCol>
                <MDBCol>
                    <MDBInput id='appDate' label='Appointment Date' type='date' ></MDBInput>
                </MDBCol>
            </MDBRow>
            <MDBRow className='mb-4'>
                <MDBCol>
                    <MDBInput id='appNotes' label='Additional Notes' type='text' size='lg'></MDBInput>
                </MDBCol>
            </MDBRow>
            <MDBBtn type='submit' outline color='dark' className='mb-4' block>
                Submit
            </MDBBtn>
        </form>
    );
}

export default StaffAppointmentModalForm;