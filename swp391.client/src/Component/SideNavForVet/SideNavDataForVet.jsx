import * as FaIcons from 'react-icons/fa';
import * as AiIcons from 'react-icons/ai';
import * as IoIcons from 'react-icons/io';


export const SideNavData = [
    {
        title: 'Work Schedule',
        path: '/vet/WorkSchedule',
        icon: <AiIcons.AiFillCalendar />,
        cName: 'nav-text'
    },
    {
        title: 'Appointment List',
        path: '/admin/appointmentList',
        icon: <IoIcons.IoIosPaper />,
        cName: 'nav-text'
    },
    {
        title: 'Pet Medical Record',
        path: '/admin/petMedicalRecord',
        icon: <IoIcons.IoIosMedkit />,
        cName: 'nav-text'
    },
    {
        title: 'Hospitalization Management',
        path: '/vet/hospitalizationManagement',
        icon: <FaIcons.FaHospital />,
        cName: 'nav-text'
    }
];