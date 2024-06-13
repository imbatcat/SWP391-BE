import * as FaIcons from 'react-icons/fa';
import * as AiIcons from 'react-icons/ai';
import * as IoIcons from 'react-icons/io';


export const SideNavData = [
    {
        title: 'Home',
        path: '/',
        icon: <AiIcons.AiFillHome />,
        cName: 'nav-text'
    },
    {
        title: 'Admin',
        path: '/admin/admins',
        icon: <IoIcons.IoIosCode />,
        cName: 'nav-text'
    },
    {
        title: 'Users',
        path: '/admin/customers',
        icon: <IoIcons.IoIosPeople />,
        cName: 'nav-text'
    },
    {
        title: 'Pets',
        path: '/admin/pets',
        icon: <FaIcons.FaDog />,
        cName: 'nav-text'
    },
    {
        title: 'Veternary',
        path: '/admin/vets',
        icon: <IoIcons.IoIosMedical />,
        cName: 'nav-text'
    },
    {
        title: 'Appointments',
        path: '/admin/appointments',
        icon: <IoIcons.IoIosPaper />,
        cName: 'nav-text'
    },
    {
        title: 'Support',
        path: '/support',
        icon: <IoIcons.IoMdHelpCircle />,
        cName: 'nav-text'
    },
];