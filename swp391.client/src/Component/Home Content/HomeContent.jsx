
import { Link } from 'react-router-dom';
import './HomeContent.css'
import {
  MDBRipple,
  MDBBtn,
  MDBCard,
  MDBCardBody,
  MDBRow,
  MDBCol,
  MDBModal,
  MDBModalBody,
  MDBModalContent,
  MDBModalDialog,
  MDBModalHeader,
  MDBModalTitle
}
from 'mdb-react-ui-kit';
import { useState } from 'react';
import AppointmentForm from '../Modals/AppointmentForm';
function HomeContent() {
  const [basicModal, setBasicModal] = useState(false);

  const toggleOpen = () => setBasicModal(!basicModal);

  return (
    

      <MDBCard>
      <div className='page-about-container'>
        <div>
            <h1 style={{textAlign:'center', marginTop:'30px '}}>About Our Service</h1>
            <p className='page-about-content'>sjdhaskldslkdsakdjs
            asdsjadsajkdhasjkdsajkhdaa
            sdasdhajkshjdas
            dsajdhsajkdhasjkdhajshdjkahdajksdhk
            sdasdasdasdadasdsad
            sdadsadasdsadsadsa
            addsadadjashdhsjakdhsajdhsajdhs
            </p>
        </div>
      </div>
      
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
              <div className='WN-Content-btn'>
                  <MDBBtn className="mb-4 px-5" color='muted' onClick={toggleOpen} style={{ color:'black', width:'240px', margin:'auto', alignContent:'center'}}>           
                  Make an Appointment
                  </MDBBtn>
                  <MDBModal open={basicModal} onClose={() => setBasicModal(false)} tabIndex='-1'>
                  <MDBModalDialog>
                    <MDBModalContent>
                        <MDBModalHeader >
                            <MDBModalTitle>Appointment Information</MDBModalTitle>
                            <MDBBtn className='btn-close' color='none' onClick={toggleOpen}></MDBBtn>
                        </MDBModalHeader>
                        <MDBModalBody>
                            <AppointmentForm></AppointmentForm>
                        </MDBModalBody>
                    </MDBModalContent>
                </MDBModalDialog>
            </MDBModal>
                
              </div>
              

            </MDBCardBody>
          </MDBCol>

        </MDBRow>
      </MDBCard>


  );
}

export default HomeContent;