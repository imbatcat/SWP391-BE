
import { Link } from 'react-router-dom';
import './Login.css'
import {
  MDBBtn,
  MDBContainer,
  MDBCard,
  MDBCardBody,
  MDBCardImage,
  MDBRow,
  MDBCol,
  MDBInput
}
from 'mdb-react-ui-kit';

function Login() {
  return (
      <MDBContainer className="my-5 d-10 justify-content-center">
          <MDBCard className='login-card'>
              <MDBRow className='g-0' >
                  <MDBCol md='6' className='imgside'></MDBCol>
                  <MDBCol md='6' >
                      <MDBCardBody className='d-flex flex-column' >

                          <h5 className="fw-bold my-5 pb-2" style={{ letterSpacing: '1px', textAlign: 'center', fontSize: '30px' }}>Sign into your account</h5>

                          <MDBInput wrapperClass='mb-4' label='Email address' id='formControlLg' type='email' size="lg" />
                          <MDBInput wrapperClass='mb-4' label='Password' id='formControlLg' type='password' size="lg" />

                          <MDBBtn className="mb-4 px-5" color='blue' size='lg'>Login</MDBBtn>
                          <a className="small text-muted" style={{ textAlign: 'end' }} href="#!">Forgot password?</a>
                          <p className="mb-5 pb-lg-2" style={{ color: 'Black' }}>Don't have an accounts
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