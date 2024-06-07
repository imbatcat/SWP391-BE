import { Link, useNavigate } from 'react-router-dom';
import './usePasswordToggle.css'
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
import toast, { Toaster } from 'react-hot-toast';
import 'react-toastify/dist/ReactToastify.css';
import usePasswordToggle from './usePasswordToggle';

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

async function loginapi(username, passwd, rememberMe, setLoginSuccess, navigate) {
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
      
      toast.success('Login successful!');
      setLoginSuccess(true);
      console.log('ok');
      navigate('/');
    
    
  } catch (error) {
    toast.error('Login failed!');
    setLoginSuccess(false);
    console.error(error.message);
    
  }
}

function Login() {
  const [basicModal, setBasicModal] = useState(false);
  const [loginSuccess, setLoginSuccess] = useState(false);
  const [userName, setUserName] = useState('');
  const [password, setPassWord] = useState('');
  const [PasswordInputType, ToggleIcon] = usePasswordToggle();
  const navigate = useNavigate();
  const toggleOpen = () => setBasicModal(!basicModal);

  const handleOnChangeUsername = (e) => {
    setUserName(e.target.value);
  }

    const handleOnChangePassWord = (e) => {
        setPassWord(e.target.value);
    }

    const handleLoginClick = () => {
       if (!userName || !password) {
           toast.error("Email/Password is required");
           return;
       }
       loginapi(userName, password, true, setLoginSuccess, navigate);
    }

  return (
      <MDBContainer className="my-5 d-10 justify-content-center">
          <MDBCard className='login-card'>
              <MDBRow className='g-0' >
                  <MDBCol md='6' className='imgside'></MDBCol>
                  <MDBCol md='6' >
                      <MDBCardBody className='d-flex flex-column' >
                          <div>
                          <Toaster
                           position="top-right"
                            reverseOrder={false}
                          />
                          </div>
                          <h5 className="fw-bold my-5 pb-2" style={{ letterSpacing: '1px', textAlign: 'center', fontSize: '30px' }}>Sign into your account</h5>

                          <MDBInput wrapperClass='mb-4' label='Email address' id='formControlLg' 
                                    onChange={(e) => handleOnChangeUsername(e)} 
                                    value={userName} 
                                    type='email' 
                                    size="lg"                                    
                          />
                        <div className='password-input-container'>
                          <MDBInput wrapperClass='mb-4' label='Password' id='formControlLg' 
                                    onChange={(e) => handleOnChangePassWord(e)} 
                                    value={password} 
                                    type={PasswordInputType} 
                                    size="lg"                                    
                          />
                          <span className='password-toggle-icon'>
                            {ToggleIcon}
                          </span>
                        </div>

                            <MDBBtn className="mb-4 px-5" color='blue' size='lg' onClick={handleLoginClick}>Login</MDBBtn>

                            <a className="small text-muted" style={{ textAlign: 'end' }} onClick={toggleOpen}>Forgot password?</a>
                            <MDBModal open={basicModal} onClose={() => setBasicModal(false)} tabIndex='-1'>
                                <MDBModalDialog>
                                    <MDBModalContent>
                                        <MDBModalHeader >
                                            <MDBModalTitle>
                                                <h6 style={{fontSize:'3vw'}}>Forgot your account’s password?</h6>
                                                <h3 style={{ fontSize: '20px' }}>Enter your email address and we’ll send you a recovery link.</h3>
                                            </MDBModalTitle>
                                            <MDBBtn className='btn-close' color='none' onClick={toggleOpen}></MDBBtn>
                                        </MDBModalHeader>
                                        <MDBModalBody>
                                            <ForgotPassForm />
                                        </MDBModalBody>
                                    </MDBModalContent>
                                </MDBModalDialog>
                            </MDBModal>

                            <p className="mb-5 pb-lg-2" style={{ color: 'Black', fontSize:'1vw' }}>Dont have an accounts ?
                                <Link to="/signUp"><a style={{ color: '#393f81', fontSize:'1vw'  }}>Register here</a></Link>
                            </p>

                            <div className='d-flex flex-row justify-content-start'>
                                <a href="#!" className="small text-muted me-1" style={{fontSize:'1vw'}}>Terms of use.</a>
                                <a href="#!" className="small text-muted" style={{fontSize:'1vw'}}>Privacy policy</a>
                            </div>


                            <div className='d-flex flex-row mt-2' style={{ justifyContent: 'end' }}>
                                <Link to="/"> <span className="h1 fw-bold mb-0" style={{ fontSize: '1.5vw', color: 'black' }}>BACK</span></Link>
                            </div>

                      </MDBCardBody>
                  </MDBCol>
              </MDBRow>
          </MDBCard>
      </MDBContainer>
  );
}

export default Login;
