import React from 'react';
import { MDBCarousel, MDBCarouselItem, MDBCarouselCaption } from 'mdb-react-ui-kit';
import img4 from '../../assets/images/hero4.jpeg'
import img1 from '../../assets/images/dc.png'
import img2 from '../../assets/images/hero2.jpg'
import img3 from '../../assets/images/hero3.jpg'
import './Hero.css';
export default function Hero() {
  return (
    <MDBCarousel showIndicators showControls fade>
      <MDBCarouselItem itemId={1}>
        <img src={img4} className='d-block w-100 img-fit ' alt='...' />
        <div className='carousel-caption-center' >
            <div className='hero-text'>
                <h5>Experienced Veterinarian</h5>
                <p>Friendly, experienced, and professional veterinarian</p>
            </div>
                
        </div>
        <MDBCarouselCaption>Pet-ternary</MDBCarouselCaption>
      </MDBCarouselItem>

      <MDBCarouselItem itemId={2}>
        <img src={img1} className='d-block w-100 img-fit4 ' alt='...' />
        <div className='carousel-caption-center' >
            <div className='hero-text'>
                <h5>Convenient Online Services</h5>
                <p>Book appointments, access medical records, and consult with veterinarians—all online!</p>
            </div>
                
        </div>
        <MDBCarouselCaption>Purr-fectly Healthy</MDBCarouselCaption>
      </MDBCarouselItem>


      <MDBCarouselItem itemId={3}>
        <img src={img2} className='d-block w-100 img-fit4 ' alt='...' />
        <div className='carousel-caption-center' >
            <div className='hero-text'>
                <h5>Convenient Online Services</h5>
                <p>Book appointments, access medical records, and consult with veterinarians—all online!</p>
            </div>
                
        </div>
        <MDBCarouselCaption>Woof-tastically Happy</MDBCarouselCaption>
      </MDBCarouselItem>


      <MDBCarouselItem itemId={4}>
        <img src={img3} className='d-block w-100 img-fit4 ' alt='...' />
        <div className='carousel-caption-center'>
            <div className='hero-text'>
                <h5>Convenient Online Services</h5>
                <p>Book appointments, access medical records, and consult with veterinarians—all online!</p>
            </div>
                
        </div>
        <MDBCarouselCaption>Woof-tastically Happy</MDBCarouselCaption>
      </MDBCarouselItem>
    </MDBCarousel>
  );
}