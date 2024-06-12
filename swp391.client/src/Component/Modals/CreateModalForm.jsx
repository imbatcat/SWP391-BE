import {
    MDBBtn,
    MDBCol,
    MDBInput,
    MDBRadio,
    MDBRow
} from 'mdb-react-ui-kit';
import { useState } from 'react';
import { toast } from 'react-toastify';

function CreateModalForm() {
    const [firstname, setFirstname] = useState('');
    const [lastname, setLastname] = useState('');
    const [username, setUsername] = useState('');
    const [phonenumber, setPhonenumber] = useState('');
    const [email, setEmail] = useState('');
    const [dateOfBirth, setDateOfBirth] = useState('');
    const [password, setPassword] = useState('');
    const [roleid, setRoleId] = useState();
    const [gender, setGender] = useState();

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
    const handleRoleidChange = (e) => {
        e.target.value == 'Veternary' ? setRoleId('3') : setRoleId('4');
    }
    async function register() {
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
                        "roleId": roleid,
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
  return (
    <form>
        <MDBRow >
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

        <MDBRow className='mb-4'>
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
     <MDBCol md='6'>
            <MDBInput
            wrapperClass='mb-2'
            label='Birthday'
            size='lg'
            id='form5'
            type='date'
            required
            onChange={handleBirthdayChange}
        />
     </MDBCol>
     <MDBCol md='6'>
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
        <MDBRow>
            <MDBCol md='6'></MDBCol>
            <MDBCol md='6'>
            <h6 className="fw-bold">Role: </h6>
         <MDBRadio
            name='inlineRadio1'
            id='inlineRadio3'
            value='User'
            label='User'
            inline
            onChange={handleRoleidChange}
         />
         <MDBRadio
             name='inlineRadio1'
             id='inlineRadio4'
             value='Veternary'
             label='Veternary'
             inline
             onChange={handleRoleidChange}
         />
            </MDBCol>
        </MDBRow>
        <MDBBtn className='mb-4' color='danger' size='lg'><a style={{ color: 'black' }}
                                onClick={(e) => { e.preventDefault(); register() }}  >Submit</a></MDBBtn>
    </form>
  );
}

export default CreateModalForm;