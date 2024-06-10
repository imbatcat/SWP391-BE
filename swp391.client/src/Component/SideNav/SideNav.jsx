import React, { useState } from 'react';
import * as FaIcons from 'react-icons/fa';
import * as AiIcons from 'react-icons/ai';
import { Link, useNavigate } from 'react-router-dom';
import { SideNavData } from './SideNavData';
import './SideNav.css';
import { IconContext } from 'react-icons';
import { useAuth } from '../../Context/AuthProvider';
import { toast } from 'react-toastify';
import { MDBBtn, MDBCol, MDBContainer, MDBIcon, MDBInput, MDBInputGroup, MDBRow } from 'mdb-react-ui-kit';

function SideNav() {
    const [sidebar, setSidebar] = useState(false);
    const [isAuthenticated, setIsAuthenticated] = useAuth();
    const showSidebar = () => setSidebar(!sidebar);
    const navigate = useNavigate();
    const logout = async () => {
        try {
            const response = await fetch(`https://localhost:7206/api/ApplicationAuth/logout`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                credentials: 'include',
            });
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            setIsAuthenticated(false);
            localStorage.removeItem("user");
            navigate('/');
        } catch (error) {
            toast.error('Error logging out!');
            console.error(error.message);
        }
    };
    return (
        <>
            <IconContext.Provider value={{ color: '#fff' }}>
                <div className='navbar'>
                    <MDBCol md='10'>
                        <Link className='menu-bars'>
                            <FaIcons.FaBars onClick={showSidebar} />
                        </Link>
                    </MDBCol>
                    
                </div>
                <nav className={sidebar ? 'nav-menu active' : 'nav-menu'}>
                    <ul className='nav-menu-items' onClick={showSidebar}>
                        <li className='navbar-toggle'>
                            <Link to='#' className='menu-bars'>
                                <AiIcons.AiOutlineClose />
                            </Link>
                        </li>
                        {SideNavData.map((item, index) => {
                            return (
                                <li key={index} className={item.cName}>
                                    <Link to={item.path}>
                                        {item.icon}
                                        <span>{item.title}</span>
                                    </Link>
                                </li>
                            );
                        })}
                        <li className='nav-text'>
                            <Link onClick={() => logout()}>
                                <span>Log Out</span>
                            </Link>
                        </li>
                    </ul>
                </nav>
            </IconContext.Provider>
        </>
    );
}
export default SideNav;