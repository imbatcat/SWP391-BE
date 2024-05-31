import {
    MDBBtn,
    MDBInput,
} from 'mdb-react-ui-kit';
import { Link } from 'react-router-dom';

function ForgotPassForm() {
  return (
    <form >
        <p><MDBInput id='ownerEmail' label='Your Email' /></p>

        <Link to='/otp'>
        <MDBBtn type='submit' outline color='dark' block>
            Send Recovery Email
        </MDBBtn>
        </Link>
    </form>
  );
}

export default ForgotPassForm;