import * as FaIcons from 'react-icons/fa';
import * as AiIcons from 'react-icons/ai';
import * as IoIcons from 'react-icons/io';
import React from 'react'
import { Link } from 'react-router-dom'

export const SideNavData = [
    {
        title: 'Home',
        path: '/',
        icon: <AiIcons.AiFillHome />,
        cName: 'nav-text'
      },
      {
        title: 'Users',
        path: '/adminAccount',
        icon: <IoIcons.IoIosPaper />,
        cName: 'nav-text'
      },
      {
        title: 'Pets',
        path: '/petsManage',
        icon: <FaIcons.FaCartPlus />,
        cName: 'nav-text'
      },
      {
        title: 'Veternary',
        path: '/vetAccount',
        icon: <IoIcons.IoMdPeople />,
        cName: 'nav-text'
      },
      {
        title: 'Appointments',
        path: '/appointmentManage',
        icon: <FaIcons.FaEnvelopeOpenText />,
        cName: 'nav-text'
      },
      {
        title: 'Support',
        path: '/support',
        icon: <IoIcons.IoMdHelpCircle />,
        cName: 'nav-text'
      },
];