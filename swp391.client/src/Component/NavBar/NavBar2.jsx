import { useState } from 'react';
import { Link } from 'react-router-dom';
import './NavBar2.css';
import {
    MDBContainer,
    MDBNavbar,
    MDBNavbarToggler,
    MDBIcon,
    MDBNavbarNav,
    MDBNavbarItem,
    MDBNavbarLink,
    MDBDropdown,
    MDBDropdownToggle,
    MDBDropdownMenu,
    MDBDropdownItem,
    MDBCollapse,
} from 'mdb-react-ui-kit';
import { useAuth } from '../../Context/AuthProvider';

export default function NavBar2() {
    const [isAuthenticated, setIsAuthenticated] = useAuth();
    const [openBasic, setOpenBasic] = useState(false);

    return (
        <MDBNavbar expand='lg' light bgColor='light' sticky>
            <MDBContainer fluid>
                <Link to="/"><h1 style={{ color: 'black' }}>Pet-ternary</h1></Link>
                <h2 >Purr-fectly Healthy, Woof-tastically Happy</h2>
                <MDBNavbarToggler
                    aria-controls='navbarSupportedContent'
                    aria-expanded='false'
                    aria-label='Toggle navigation'
                    onClick={() => setOpenBasic(!openBasic)}
                >
                    <MDBIcon icon='bars' fas />
                </MDBNavbarToggler>

                <MDBCollapse navbar open={openBasic} style={{ justifyContent: 'end' }}>
                    <MDBNavbarNav className='mr-auto mb-2 mb-lg-0'>
                        <MDBNavbarItem>
                            <Link to="/"><MDBNavbarLink active aria-current='page'>
                                Home
                            </MDBNavbarLink>
                            </Link>

                        </MDBNavbarItem>
                        <MDBNavbarItem>
                            <Link to="/petList"> <MDBNavbarLink>My Pet List</MDBNavbarLink></Link>
                        </MDBNavbarItem>
                        <MDBNavbarItem>
                            <Link to="/aboutUs"> <MDBNavbarLink>About Us</MDBNavbarLink></Link>
                        </MDBNavbarItem>

                        <MDBNavbarItem>
                            <MDBDropdown>
                                <MDBDropdownToggle tag='a' className='nav-link' role='button'>
                                    Appointment
                                </MDBDropdownToggle>
                                <MDBDropdownMenu>
                                    <MDBDropdownItem link>My Appointment</MDBDropdownItem>
                                    <MDBDropdownItem link>Make an Appointment</MDBDropdownItem>

                                </MDBDropdownMenu>
                            </MDBDropdown>
                        </MDBNavbarItem>

                        <MDBNavbarItem>
                            <MDBDropdown>
                                <MDBDropdownToggle tag='a' className='nav-link' role='button'>
                                    Contact Us
                                </MDBDropdownToggle>
                                <MDBDropdownMenu>
                                    <MDBDropdownItem link href='https://zalo.me/g/alobzv478'>Zalo</MDBDropdownItem>
                                    <MDBDropdownItem link href='https://www.facebook.com/profile.php?id=100009406588322'>FaceBook</MDBDropdownItem>
                                    <MDBDropdownItem link>09321231232</MDBDropdownItem>
                                </MDBDropdownMenu>
                            </MDBDropdown>
                        </MDBNavbarItem>

                        <MDBNavbarItem>
                            <MDBNavbarLink disabled href='#' tabIndex={-1} aria-disabled='true'>
                            </MDBNavbarLink>
                        </MDBNavbarItem>

                        {isAuthenticated ? (
                            <>
                                <MDBNavbarItem>
                                    <Link to="/profile"><button className='btn'>Profile</button></Link>
                                </MDBNavbarItem>
                            </>
                        ) : (
                            <>
                                <MDBNavbarItem>
                                    <Link to="/login"><button className='btn'>Sign In</button></Link>
                                </MDBNavbarItem>
                                <MDBNavbarItem>
                                    <Link to="/signUp"><button className='btn'>Register</button></Link>
                                </MDBNavbarItem>
                            </>

                        )}
                    </MDBNavbarNav>


                </MDBCollapse>
            </MDBContainer>
        </MDBNavbar>
    );
}