import React from 'react';
import { MDBCarousel, MDBCarouselItem, MDBCarouselCaption } from 'mdb-react-ui-kit';
import img1 from '../../assets/images/dc.png'
import './Hero.css';
export default function Hero() {
  return (
    <MDBCarousel showIndicators showControls fade>
      <MDBCarouselItem itemId={1}>
        <img src={img1} className='d-block w-100 img-fit ' alt='...' />
        <MDBCarouselCaption className='carousel-caption-center' >
            <div className='hero-text'>
                <h5>Convenient Online Services</h5>
                <p>Book appointments, access medical records, and consult with veterinarians—all online!
                </p>
            </div>
                
        </MDBCarouselCaption>
      </MDBCarouselItem>

      {/* <MDBCarouselItem itemId={2}>
        <img src='https://mdbootstrap.com/img/Photos/Slides/img%20(22).jpg' className='d-block w-100' alt='...' />
        <MDBCarouselCaption>
          <h5>Second slide label</h5>
          <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
        </MDBCarouselCaption>
      </MDBCarouselItem>

      <MDBCarouselItem itemId={3}>
        <img src='https://mdbootstrap.com/img/Photos/Slides/img%20(23).jpg' className='d-block w-100' alt='...' />
        <MDBCarouselCaption>
          <h5>Third slide label</h5>
          <p>Praesent commodo cursus magna, vel scelerisque nisl consectetur.</p>
        </MDBCarouselCaption>
      </MDBCarouselItem> */}
    </MDBCarousel>
  );
}