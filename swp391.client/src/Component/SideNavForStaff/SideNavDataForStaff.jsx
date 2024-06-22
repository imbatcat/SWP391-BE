import * as FaIcons from 'react-icons/fa';
import * as AiIcons from 'react-icons/ai';
import * as IoIcons from 'react-icons/io';


export const SideNavData = [
    {
        title: 'Cage List',
        path: '/staff/cage-list',
        icon: <FaIcons.FaPaw />,
        cName: 'nav-text'
    },
    {
        title: 'Service Bills',
        path: '/staff/service-bill-list',
        icon: <IoIcons.IoIosPaper />,
        cName: 'nav-text'
    },
    {
        title: 'Appointment checkin',
        path: '/staff/appointment-checkin',
        icon: <IoIcons.IoIosJournal />,
        cName: 'nav-text'
    },

];