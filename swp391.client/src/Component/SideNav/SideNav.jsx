import React, { useState } from 'react';
import * as FaIcons from 'react-icons/fa';
import * as AiIcons from 'react-icons/ai';
import { Link, Navigate } from 'react-router-dom';
import { SideNavData } from './SideNavData';
import './SideNav.css';
import { IconContext } from 'react-icons';
import { useAuth } from '../../Context/AuthProvider';
import { toast } from 'react-toastify';
function SideNav() {
    const [sidebar, setSidebar] = useState(false);
    const [isAuthenticated, setIsAuthenticated] = useAuth();
  const showSidebar = () => setSidebar(!sidebar);

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
        Navigate('/');
    } catch (error) {
        toast.error('Error logging out!');
        console.error(error.message);
    }
};
  return (
    <>
      <IconContext.Provider value={{ color: '#fff' }}>
        <div className='navbar'>
          <Link to='#' className='menu-bars'>
            <FaIcons.FaBars onClick={showSidebar} />
          </Link>
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
              <Link to='#' onClick={logout}>
                <AiIcons.AiOutlineLogout />
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