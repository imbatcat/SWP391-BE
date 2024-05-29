
import './Footer.css'
import { Link } from 'react-router-dom';
import { MDBFooter, MDBContainer, MDBRow, MDBCol, MDBIcon } from 'mdb-react-ui-kit';

export default function Footer() {
  return (
    <MDBFooter bgColor='light' className='text-center text-lg-start text-muted'>
      <section className='d-flex justify-content-center justify-content-lg-between p-4 border-bottom'>
        <div className='me-5 d-none d-lg-block'>
          <span>Get connected with us on social networks:</span>
        </div>

        <div>
          <a href='' className='me-4 text-reset'>
            <MDBIcon color='secondary' fab icon='facebook-f' />
          </a>
          <a href='' className='me-4 text-reset'>
            <MDBIcon color='secondary' fab icon='twitter' />
          </a>
          <a href='' className='me-4 text-reset'>
            <MDBIcon color='secondary' fab icon='instagram' />
          </a>
          <a href='' className='me-4 text-reset'>
            <MDBIcon color='secondary' fab icon='linkedin' />
          </a>
        </div>
      </section>

      <section className=''>
        <MDBContainer className='text-center text-md-start mt-5'>
          <MDBRow className='mt-3'>
            <MDBCol md='3' lg='4' xl='3' className='mx-auto mb-4'>
              <h6 className='text-uppercase fw-bold mb-4'>
                <MDBIcon color='secondary' icon='gem' className='me-3' />
                Pet-ternary
              </h6>
              <p>
              We are a pet care clinic located in Leavenworth and 
              have been helping (primarily) dogs and cats live happier and healthier since 2023.
              </p>
            </MDBCol>

            <MDBCol md='2' lg='2' xl='2' className='mx-auto mb-4'>
            <p>
                <Link to='/'><a className='text-reset'> Home </a></Link>
              </p>
              <p>
                <Link to='/aboutUs'><a className='text-reset'> About Us </a></Link>
              </p>
              <p>
              <Link to='/MakeAnAppointment'><a className='text-reset'> Make an Appointment </a></Link>
              </p>
              <p>
                <a href='#!' className='text-reset'>
                  Vue
                </a>
              </p>
              <p>
                <a href='#!' className='text-reset'>
                  Laravel
                </a>
              </p>
            </MDBCol>

            <MDBCol md='3' lg='2' xl='2' className='mx-auto mb-4'>
              <h6 className='text-uppercase fw-bold mb-4'>HOURS OF OPERATION</h6>
              <p>
                <a href='#!' className='text-reset'>
                Regular hours:
                </a>
              </p>
              <p>
                <a href='#!' className='text-reset'>
                Mon-Fri: 8:00-17h30
                </a>
              </p>
              <p>
                <a href='#!' className='text-reset'>
                Sat: 8:00-14:30
                </a>
              </p>
              <p>
                <a href='#!' className='text-reset'>
                Emergencies 24/7
                </a>
              </p>
            </MDBCol>

            <MDBCol md='4' lg='3' xl='3' className='mx-auto mb-md-0 mb-4'>
              <h6 className='text-uppercase fw-bold mb-4'>Contact</h6>
              <p>
                <MDBIcon color='secondary' icon='home' className='me-2' />
                1 Lê Duẩn, P.Bến Nghé, Q.1, HCMC
              </p>
              <p>
                <MDBIcon color='secondary' icon='envelope' className='me-3' />
                pet-ternary.vet2023@fpt.com.vn
              </p>
              <p>
                <MDBIcon color='secondary' icon='phone' className='me-3' /> + 84 9783327233
              </p>
              <p>
                <MDBIcon color='secondary' icon='print' className='me-3' /> + 84 9783327233
              </p>
            </MDBCol>
          </MDBRow>
        </MDBContainer>
      </section>

      <div className='text-center p-4' style={{ backgroundColor: 'rgba(0, 0, 0, 0.05)' }}>
        © 2021 Copyright- 
        <a className='text-reset fw-bold'>
          Pet-ternary- All Dogs & Cats Veterinary Hospital
        </a>
      </div>
    </MDBFooter>
  );
}