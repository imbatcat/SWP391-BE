import {
    MDBBtn,
    MDBCard,
    MDBCardBody,
    MDBCol,
    MDBContainer,
    MDBInput,
    MDBRadio,
    MDBRow
} from 'mdb-react-ui-kit';
import { Link, useNavigate } from 'react-router-dom';
import './SignUp.css';
import { toast } from 'react-toastify';
import { useState } from 'react';

async function register(lastname, firstname, username, phonenumber, dateOfBirth, password, gender, email, navigate) {
    try {
        const response = await fetch('https://localhost:7206/api/ApplicationAuth/register', {
            method: 'POST', // *GET, POST, PUT, DELETE, etc.
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include',
            body: JSON.stringify(
                {
                    "userName": username,
                    "password": password,
                    "fullName": lastname + firstname,
                    "email": email,
                    "phoneNumber": phonenumber,
                    "isMale": gender,
                    "roleId": 1,
                    "dateOfBirth": dateOfBirth
                }
            ) // body data type must match "Content-Type" header
        });
        if (!response.ok) {
            throw new Error(response.message);
        }
        toast.info("Check your email to activate your account");
        navigate('/');
        console.log('ok');
    } catch (error) {
        toast.error('Login failed!');
        console.error(error.message);
    }
}
function SignUp() {
    const [firstname, setFirstname] = useState('');
    const [lastname, setLastname] = useState('');
    const [username, setUsername] = useState('');
    const [phonenumber, setPhonenumber] = useState('');
    const [email, setEmail] = useState('');
    const [dateOfBirth, setDateOfBirth] = useState('');
    const [password, setPassword] = useState('');
    const [gender, setGender] = useState();
    const [isDisabled, setIsDisabled] = useState();
    const navigate = useNavigate();

    const handleFirstNameChange = (e) => setFirstname(e.target.value);
    const handleLastNameChange = (e) => setLastname(e.target.value);
    const handleUsernameChange = (e) => setUsername(e.target.value);
    const handleEmailChange = (e) => setEmail(e.target.value);
    const handlePasswordChange = (e) => setPassword(e.target.value);
    const handlePhonenumberChange = (e) => setPhonenumber(e.target.value);
    const handleBirthdayChange = (e) => setDateOfBirth(e.target.value);
    const handleGenderChange = (e) => {
        e.target.value == 'Male' ? setGender(true) : setGender(false);
    }

    return (
        <div className='pageSignUp'>
            <MDBContainer fluid className='page-container'>

                <MDBRow className='justify-content-center align-items-center m-5'>

                    <MDBCard className='card'>
                        <MDBCardBody className='px-4'>

                            <h3 className="fw-bold mb-4 pb-2 pb-md-0 mb-md-5">Registration Form</h3>

                            <MDBRow>
                                <MDBCol md='6'>
                                    <MDBInput
                                        wrapperClass='mb-4'
                                        label='First Name'
                                        size='lg'
                                        id='form1'
                                        type='text'
                                        required
                                        onChange={handleFirstNameChange}
                                    />
                                </MDBCol>
                                <MDBCol md='6'>
                                    <MDBInput
                                        wrapperClass='mb-4'
                                        label='Last Name'
                                        size='lg'
                                        id='form2'
                                        type='text'
                                        required
                                        onChange={handleLastNameChange}
                                    />
                                </MDBCol>
                            </MDBRow>
                            <MDBRow>
                                <MDBCol md='12'>
                                    <MDBInput
                                        wrapperClass='mb-4'
                                        label='Username'
                                        size='lg'
                                        id='form3'
                                        type='text'
                                        required
                                        onChange={handleUsernameChange}
                                    />
                                </MDBCol>
                            </MDBRow>
                            <MDBRow>
                                <MDBCol md='12'>
                                    <MDBInput
                                        wrapperClass='mb-4'
                                        label='Password'
                                        size='lg'
                                        id='form4'
                                        type='password'
                                        required
                                        onChange={handlePasswordChange}
                                    />
                                </MDBCol>
                            </MDBRow>
                            <MDBRow>
                                <MDBCol md='6'>
                                    <MDBInput
                                        wrapperClass='mb-4'
                                        label='Birthday'
                                        size='lg'
                                        id='form5'
                                        type='date'
                                        required
                                        onChange={handleBirthdayChange}
                                    />
                                </MDBCol>
                                <MDBCol md='6' className='mb-4'>
                                    <h6 className="fw-bold">Gender: </h6>
                                    <MDBRadio
                                        name='inlineRadio'
                                        id='inlineRadio1'
                                        value='Female'
                                        label='Female'
                                        inline
                                        onChange={handleGenderChange}
                                    />
                                    <MDBRadio
                                        name='inlineRadio'
                                        id='inlineRadio2'
                                        value='Male'
                                        label='Male'
                                        inline
                                        onChange={handleGenderChange}
                                    />
                                </MDBCol>
                            </MDBRow>
                            <MDBRow>
                                <MDBCol md='6'>
                                    <MDBInput
                                        wrapperClass='mb-4'
                                        label='Email'
                                        size='lg'
                                        id='form6'
                                        type='email'
                                        required
                                        onChange={handleEmailChange} // Assuming you want to reuse the username handler for email
                                    />
                                </MDBCol>
                                <MDBCol md='6'>
                                    <MDBInput
                                        wrapperClass='mb-4'
                                        label='Phone Number'
                                        size='lg'
                                        id='form7'
                                        type='tel'
                                        required
                                        onChange={handlePhonenumberChange} // Assuming you want to reuse the username handler for phone number
                                    />
                                </MDBCol>
                            </MDBRow>
                            <MDBBtn className='mb-4' color='danger' size='lg'><a style={{ color: 'black' }}
                                onClick={(e) => { e.preventDefault(); register(lastname, firstname, username, phonenumber, dateOfBirth, password, gender, email, navigate) }}  >Submit</a></MDBBtn>
                            <div className='d-flex flex-row mt-2' style={{ justifyContent: 'end' }}>
                                <Link to="/"> <span className="h1 fw-bold mb-0" style={{ fontSize: '20px', color: 'black' }}>BACK</span></Link>
                            </div>
                        </MDBCardBody>
                    </MDBCard>

                </MDBRow>
            </MDBContainer>
        </div>
    );
}

export default SignUp;