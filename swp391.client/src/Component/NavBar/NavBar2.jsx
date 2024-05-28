import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import {
  MDBContainer,
  MDBNavbar,
  MDBNavbarBrand,
  MDBNavbarToggler,
  MDBIcon,
  MDBNavbarNav,
  MDBNavbarItem,
  MDBNavbarLink,
  MDBBtn,
  MDBDropdown,
  MDBDropdownToggle,
  MDBDropdownMenu,
  MDBDropdownItem,
  MDBCollapse,
} from 'mdb-react-ui-kit';

export default function App() {
  const [openBasic, setOpenBasic] = useState(false);

  return (
    <MDBNavbar expand='lg' light bgColor='light'>
      <MDBContainer fluid>
      <Link to="/"><h1 style={{color:'black'}}>Pet-ternary</h1></Link> 

        <MDBNavbarToggler
          aria-controls='navbarSupportedContent'
          aria-expanded='false'
          aria-label='Toggle navigation'
          onClick={() => setOpenBasic(!openBasic)}
        >
          <MDBIcon icon='bars' fas />
        </MDBNavbarToggler>

        <MDBCollapse navbar open={openBasic} style={{justifyContent:'end'}}>
          <MDBNavbarNav className='mr-auto mb-2 mb-lg-0'>
            <MDBNavbarItem>
              <MDBNavbarLink active aria-current='page' href='/'>
                Home
              </MDBNavbarLink>
            </MDBNavbarItem>
            <MDBNavbarItem>
              <Link to="/about"> <MDBNavbarLink>About Us</MDBNavbarLink></Link> 
             
            </MDBNavbarItem>

            <MDBNavbarItem>
              <MDBDropdown>
                <MDBDropdownToggle tag='a' className='nav-link' role='button'>
                  HealthCare System
                </MDBDropdownToggle>
                <MDBDropdownMenu>
                  <MDBDropdownItem link='/action'>Action</MDBDropdownItem>
                  <MDBDropdownItem link>Another action</MDBDropdownItem>
                  <MDBDropdownItem link>Something else here</MDBDropdownItem>
                </MDBDropdownMenu>
              </MDBDropdown>
            </MDBNavbarItem>

            <MDBNavbarItem>
              <MDBDropdown>
                <MDBDropdownToggle tag='a' className='nav-link' role='button'>
                  Contact Us
                </MDBDropdownToggle>
                <MDBDropdownMenu>
                  <MDBDropdownItem link>Zalo</MDBDropdownItem>
                  <MDBDropdownItem link>FaceBook</MDBDropdownItem>
                  <MDBDropdownItem link>Phone Number</MDBDropdownItem>
                </MDBDropdownMenu>
              </MDBDropdown>
            </MDBNavbarItem>

            <MDBNavbarItem>
              <MDBNavbarLink disabled href='#' tabIndex={-1} aria-disabled='true'>
                
              </MDBNavbarLink>
            </MDBNavbarItem>
            <MDBNavbarItem>
            <Link to="/login"><button  className='btn'>Sign In</button></Link> 
            </MDBNavbarItem>
          </MDBNavbarNav>
            
          
        </MDBCollapse>
      </MDBContainer>
    </MDBNavbar>
  );
}