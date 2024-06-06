import {
    MDBBtn,
    MDBInput,
} from 'mdb-react-ui-kit';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';

const SendResetPassword = async(email, navigate) => {
    try {
        const response = await fetch(`https://localhost:7206/api/ApplicationAuth/forgot-password`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            credentials: 'include', // Include credentials if needed
            body: JSON.stringify({
                "email": email
            })
        });
        if (response.ok) {
            toast.info("Check your emails");
            navigate('')
        } else {
            console.log(response.message);
        }
    } catch (error) {
        console.log("Something went wrong");
    }
}

function ForgotPassForm() {
    const [email, setEmail] = useState('');
    const handleOnChange = (e) => {
        setEmail(e.target.value);
    }
  var navigate = useNavigate();
  return (
    <form >
        <p><MDBInput id='ownerEmail' label='Your Email' onChange={(e) => handleOnChange(e)} /></p>

          <MDBBtn type='submit' onClick={(e) => { e.preventDefault(); SendResetPassword(email, navigate)}} outline color='dark' block>
            Send Recovery Email
        </MDBBtn>
    </form>
  );
}

export default ForgotPassForm;