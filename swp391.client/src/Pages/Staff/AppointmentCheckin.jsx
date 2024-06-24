import { useState, useEffect, useCallback } from 'react';
import {
    MDBBadge, MDBBtn, MDBTable, MDBTableBody, MDBTableHead
}
    from 'mdb-react-ui-kit';
import SideNavForStaff from '../../Component/SideNavForStaff/SideNavForStaff';
import YearWeekSelector from '../../Component/YearWeekSelector/YearWeekSelector';
import { useUser } from '../../Context/UserContext';
import FormatDateForDisplay from '../../Helpers/FormatDateForDisplay';
import FormatStartDateForDisplay from '../../Helpers/FormatDateForDisplay';
import getStartDateOfWeek from '../../Helpers/getStartDateOfWeek';
import formatDateForApi from '../../Helpers/formatDateForAPI';
import FormatDateForAPI from '../../Helpers/formatDateForAPI';

export default function AppointmentCheckin() {
    const [appointmentList, setAppointmentList] = useState([]);
    const [searchInput, setSearchInput] = useState();
    const [selectedDisplayDates, setSelectedDisplayDates] = useState([]);
    const [selectedAPIDates, setSelectedAPIDates] = useState([]);

    const handleSearchInputChange = () => {
    };

    useEffect(() => {

    }, []);

    const handleYearWeekChange = useCallback((year, week) => {
        const startDate = getStartDateOfWeek(week, year);
        const displayDates = [];
        const apiDates = [];
        for (let i = 0; i < 7; i++) {
            const date = new Date(startDate);
            date.setDate(date.getDate() + i);
            displayDates.push(FormatDateForDisplay(date));
            apiDates.push(FormatDateForAPI(date));
        }
        setSelectedDisplayDates(displayDates);
        setSelectedAPIDates(apiDates);
    }, []);
    return (
        <>
            <SideNavForStaff searchInput={searchInput} handleSearchInputChange={handleSearchInputChange} />
            <YearWeekSelector onYearWeekChange={handleYearWeekChange}></YearWeekSelector>
            <MDBTable align='middle'>
                <MDBTableHead>
                    <tr style={{ textAlign: 'center' }}>
                        <th scope='col' width='20%'>Appointment ID</th>
                        <th scope='col'>Owner name</th>
                        <th scope='col'>Pet name</th>
                        <th scope='col'>Appointment Date</th>
                        <th scope='col'>Note</th>
                        <th scope='col'></th>
                    </tr>
                </MDBTableHead>
                <MDBTableBody style={{ textAlign: 'center' }}>
                    {appointmentList.map((app) => (
                        <tr key={app.id}>
                            <td>
                                <div className='d-flex align-items-center'>
                                    <img
                                        src='https://mdbootstrap.com/img/new/avatars/8.jpg'
                                        alt=''
                                        style={{ width: '45px', height: '45px' }}
                                        className='rounded-circle'
                                    />
                                    <div className='ms-3'>
                                        <p className='fw-bold mb-1'>{app.ownerName}</p>
                                        <p className='text-muted mb-0'>{app.ownerNumber}</p>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <p className='fw-normal mb-1'>{app.petName}</p>
                            </td>
                            <td>
                                <p className='fw-bold mb-1'>{app.appointmentDate}</p>
                                <p className='text-muted mb-0'>{app.timeSlot}</p>
                            </td>
                            <td>
                                <p className='fw-normal mb-1'>{app.appointmentNotes}</p>
                            </td>
                            <td>
                                <MDBBtn>Checkin</MDBBtn>
                            </td>
                        </tr>
                    ))}
                </MDBTableBody>
            </MDBTable>
        </>
    );
}