import { Link } from 'react-router-dom';
import './Login.css'
import {
  MDBBtn,
  MDBContainer,
  MDBCard,
  MDBCardBody,
  MDBRow,
  MDBCol,
  MDBInput,
  MDBModal,
  MDBModalBody,
  MDBModalContent,
  MDBModalDialog,
  MDBModalHeader,
  MDBModalTitle
} from 'mdb-react-ui-kit';
import { useState } from 'react';
import ForgotPassForm from '../../Component/ForgotPass/ForgotPassForm';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

async function fetchData(setData) {
  try {
    const response = await fetch('https://localhost:7206/api/Accounts', {
      method: 'GET', // *GET, POST, PUT, DELETE, etc.
      credentials: 'include',
      headers: {
        // 'Content-Type': 'application/x-www-form-urlencoded',
      }
      // body data type must match "Content-Type" header
    });
    if (!response.ok) {
      throw new Error("Error fetching data");
    }
    const json = await response.json();
    setData(json); // Ensure setData is correctly referenced
    console.log(json);
  } catch (error) {
    console.error(error.message);
  }
}

async function loginapi(username, passwd, rememberMe, setLoginSuccess) {
  try {
    const response = await fetch('https://localhost:7206/api/ApplicationAuth/login', {
      method: 'POST', // *GET, POST, PUT, DELETE, etc.
      headers: {
        'Content-Type': 'application/json'
      },
      credentials: 'include',
      body: JSON.stringify(
        {
          "userName": username,
          "password": passwd,
          "rememberMe": rememberMe
        }
      ) // body data type must match "Content-Type" header
    });
    if (!response.ok) {
      throw new Error("Error fetching data");
    }
    setLoginSuccess(true);
    toast.success('Login successful!');
    console.log('ok');
  } catch (error) {
    setLoginSuccess(false);
    toast.error('Login failed!');
    console.error(error.message);
  }
}

function Login() {
  const [basicModal, setBasicModal] = useState(false);
  const [loginSuccess, setLoginSuccess] = useState(false);
  const [userName, setUserName] = useState('');
  const [password, setPassWord] = useState('');

  const toggleOpen = () => setBasicModal(!basicModal);

  const handleOnChangeUsername = (e) => {
    setUserName(e.target.value);
  }

  const handleOnChangePassWord = (e) => {
    setPassWord(e.target.value);
  }

  const handleLoginClick = () => {
    loginapi(userName, password, true, setLoginSuccess);
  }

  return (
      <MDBContainer className="my-5 d-10 justify-content-center">
          <MDBCard className='login-card'>
              <MDBRow className='g-0' >
                  <MDBCol md='6' className='imgside'></MDBCol>
                  <MDBCol md='6' >
                      <MDBCardBody className='d-flex flex-column' >

                          <h5 className="fw-bold my-5 pb-2" style={{ letterSpacing: '1px', textAlign: 'center', fontSize: '30px' }}>Sign into your account</h5>

                          <MDBInput wrapperClass='mb-4' label='Email address' id='formControlLg' onChange={(e) => handleOnChangeUsername(e)} value={userName} type='email' size="lg" />
                          <MDBInput wrapperClass='mb-4' label='Password' id='formControlLg' onChange={(e) => handleOnChangePassWord(e)} value={password} type='password' size="lg" />

                          <MDBBtn className="mb-4 px-5" color='blue' size='lg' onClick={() => loginapi(userName, password, true, setLoginSuccess)}>Login</MDBBtn>

                          <a className="small text-muted" style={{ textAlign: 'end' }} onClick={toggleOpen}>Forgot password?</a>
                          <MDBModal open={basicModal} onClose={() => setBasicModal(false)} tabIndex='-1'>
                            <MDBModalDialog>
                                <MDBModalContent>
                                  <MDBModalHeader >
                                    <MDBModalTitle>
                                    <h6>Forgot your account’s password?</h6>
                                     <h3 style={{fontSize:'20px'}}>Enter your email address and we’ll send you a recovery link.</h3>
                                     </MDBModalTitle>
                                        <MDBBtn className='btn-close' color='none' onClick={toggleOpen}></MDBBtn>
                                    </MDBModalHeader>
                            <MDBModalBody>
                            <ForgotPassForm/>
                            </MDBModalBody>
                        </MDBModalContent>
                            </MDBModalDialog>
                            </MDBModal>

                          <p className="mb-5 pb-lg-2" style={{ color: 'Black' }}>Don't have an accounts ?
                          <Link to="/signUp"><a style={{ color: '#393f81' }}>Register here</a></Link>
                          </p>

                          <div className='d-flex flex-row justify-content-start'>
                              <a href="#!" className="small text-muted me-1">Terms of use.</a>
                              <a href="#!" className="small text-muted">Privacy policy</a>
                          </div>


                          <div className='d-flex flex-row mt-2' style={{ justifyContent: 'end' }}>
                              <Link to="/"> <span className="h1 fw-bold mb-0" style={{ fontSize: '20px', color: 'black' }}>BACK</span></Link>
                          </div>

                      </MDBCardBody>
                  </MDBCol>
              </MDBRow>
          </MDBCard>
      </MDBContainer>
  );
}

export default Login;
