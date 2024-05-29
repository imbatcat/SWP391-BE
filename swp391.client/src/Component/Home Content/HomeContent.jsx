
import { Link } from 'react-router-dom';
import './HomeContent.css'
import {
  MDBRipple,
  MDBBtn,
  MDBCard,
  MDBCardBody,
  MDBRow,
  MDBCol,
}
from 'mdb-react-ui-kit';

function HomeContent() {
  return (
    

      <MDBCard>
        <MDBRow className='g-0'>
            
            <MDBCol md='6' className='imgside'>
                <MDBRipple>
                <img src={'./dc.png'} alt='logo'/> 
                </MDBRipple>
            </MDBCol>
        
          

          <MDBCol md='6'>
            <MDBCardBody className='d-flex flex-column'>

              <h5 className="fw-bold my-5 pb-2" style={{letterSpacing: '1px', textAlign:'center', fontSize:'50px'}}>What's Next </h5>
              <div className="WN-Content">1. Call us or schedule an appointment online.</div>
              <div className="WN-Content">2. Meet with a doctor for an initial exam.</div>
              <div className="WN-Content">3. Put a plan together for your pet.</div>
              <MDBBtn className="mb-4 px-5" color='green' style={{width:'200px'}}>Login</MDBBtn>

            </MDBCardBody>
          </MDBCol>

        </MDBRow>
      </MDBCard>


  );
}

export default HomeContent;