import * as FaIcons from 'react-icons/fa';
import * as AiIcons from 'react-icons/ai';
import * as IoIcons from 'react-icons/io';


export const SideNavData = [
    {
        title: 'Work Schedule',
        path: '/vet/WorkSchedule',
        icon: <AiIcons.AiFillHome />,
        cName: 'nav-text'
    },
    {
        title: 'Appointment List',
        path: '/vet/appointmentList',
        icon: <IoIcons.IoMdHelpCircle />,
        cName: 'nav-text'
    },
    {
        title: 'Pet Medical Record',
        path: '/vet/petMedicalRecord',
        icon: <IoIcons.IoIosPaper />,
        cName: 'nav-text'
    },
    {
        title: 'Hospitalization Management',
        path: '/vet/hospitalizationManagement',
        icon: <FaIcons.FaCartPlus />,
        cName: 'nav-text'
    }
];