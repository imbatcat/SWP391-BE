import React, { useEffect, useState, useCallback } from 'react';
import {
    MDBBadge,
    MDBTable,
    MDBTableBody,
    MDBTableHead,
    MDBBtn,
    MDBModal,
    MDBModalDialog,
    MDBModalContent,
    MDBModalHeader,
    MDBModalTitle,
    MDBModalBody,
    MDBModalFooter
} from 'mdb-react-ui-kit';
import SideNavForVet from '../../Component/SideNavForVet/SideNavForVet';
import YearWeekSelector from '../../Component/YearWeekSelector/YearWeekSelector';
import { useUser } from '../../Context/UserContext';

// Helper functions omitted for brevity

const getStartDateOfWeek = (week, year) => {
    const janFirst = new Date(year, 0, 1);
    const days = (week - 1) * 7;
    const startDate = new Date(janFirst.setDate(janFirst.getDate() + days));
    const dayOfWeek = startDate.getDay();
    const diff = startDate.getDate() - dayOfWeek + (dayOfWeek === 0 ? -6 : 1);
    return new Date(startDate.setDate(diff));
};

const formatDateForDisplay = (date) => {
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0');
    return `${day}/${month}`;
};

const formatDateForAPI = (date) => {
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const year = date.getFullYear();
    return `${year}-${month}-${day}`;
};

const timeSlotMapping = {
    1: '7:00 - 8:30',
    2: '8:30 - 10:00',
    3: '10:00 - 11:30',
    4: '13:00 - 14:30',
    5: '14:30 - 16:00',
    6: '16:00 - 17:30'
};

const getTimeSlotKey = (timeSlotValue) => {
    return parseInt(Object.keys(timeSlotMapping).find(key => timeSlotMapping[key] === timeSlotValue), 10);
};
function WorkSchedule() {
    const [user] = useUser();
    const [selectedDisplayDates, setSelectedDisplayDates] = useState([]);
    const [selectedAPIDates, setSelectedAPIDates] = useState([]);
    const [appointments, setAppointments] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [modal, setModal] = useState({ isOpen: false, date: '', timeSlot: '', appointments: [] });

    const handleYearWeekChange = useCallback((year, week) => {
        const startDate = getStartDateOfWeek(week, year);
        const displayDates = [];
        const apiDates = [];
        for (let i = 0; i < 7; i++) {
            const date = new Date(startDate);
            date.setDate(date.getDate() + i);
            displayDates.push(formatDateForDisplay(date));
            apiDates.push(formatDateForAPI(date));
        }
        setSelectedDisplayDates(displayDates);
        setSelectedAPIDates(apiDates);
    }, []);

    async function fetchData(vetId, timeSlot, date, isGetAll = true) {
        try {
            const url = new URL(`https://localhost:7206/api/Appointment/AppointmetList/VetAppointment/${vetId}`);
            if (timeSlot) url.searchParams.append('timeSlot', timeSlot);
            if (date) url.searchParams.append('date', date);
            url.searchParams.append('isGetAll', isGetAll);
    
            // Log the constructed URL for debugging
            console.log('Fetching data from URL:', url.toString());
    
            const response = await fetch(url, {
                method: 'GET',
                credentials: 'include',
                headers: {
                    'Content-Type': 'application/json',
                }
            });
    
            console.log(`Response status: ${response.status}`); // Log response status
    
            if (!response.ok) {
                if (response.status === 400) {
                    throw new Error(`Bad Request: ${response.statusText}`);
                } else if (response.status === 401) {
                    throw new Error(`Unauthorized: ${response.statusText}`);
                } else if (response.status === 404) {
                    throw new Error(`Not Found: ${response.statusText}`);
                } else {
                    throw new Error(`Unexpected Error: ${response.statusText}`);
                }
            }
    
            const data = await response.json();
            setAppointments(data);
            setIsLoading(false); // Update the loading state after data is fetched
        } catch (error) {
            console.error('Fetch error:', error.message);
            setIsLoading(false); // Update the loading state in case of an error
        }
    }

    useEffect(() => {
        if (user) {
            fetchData(user.vetId, '', '', true); // Fetch initial data without timeSlot and date
        }
    }, [user]);

    const countAppointments = (date, timeSlot) => {
        return appointments.filter(appointment =>
            appointment.timeSlot === timeSlot && appointment.appointmentDate === date
        ).length;
    };

    const getBadgeColor = (count) => {
        if (count > 0 && count < 4) return 'success';
        if (count >= 4 && count < 7) return 'warning';
        if (count >= 7) return 'danger';
        return 'secondary';
    };

    const handleBadgeClick = (date, timeSlotDisplay) => {
        const timeSlot = getTimeSlotKey(timeSlotDisplay);
        fetchData(user.vetId, timeSlot, date, false); // Fetch data for the specific date and timeSlot
        const filteredAppointments = appointments.filter(appointment =>
            appointment.timeSlot === timeSlot && appointment.appointmentDate === date
        );
        setModal({ isOpen: true, date, timeSlot: timeSlotDisplay, appointments: filteredAppointments });
    };

    if (isLoading) {
        return <div>Loading...</div>; // Loading state
    }

    return (
        <div>
            <SideNavForVet />
            <MDBTable bordered align='middle' style={{ margin: 'auto' }}>
                <MDBTableHead>
                    <tr style={{ textAlign: 'center' }}>
                        <th scope='row' style={{ width: '10%' }}>
                            <YearWeekSelector onYearWeekChange={handleYearWeekChange} />
                        </th>
                        <th scope='col'>Monday</th>
                        <th scope='col'>Tuesday</th>
                        <th scope='col'>Wednesday</th>
                        <th scope='col'>Thursday</th>
                        <th scope='col'>Friday</th>
                        <th scope='col'>Saturday</th>
                        <th scope='col'>Sunday</th>
                    </tr>
                    <tr style={{ textAlign: 'center', height: '10px', fontSize: '0.8vw' }}>
                        <th scope='row'>Date</th>
                        {selectedDisplayDates.map((date, index) => (
                            <td key={index}>{date}</td>
                        ))}
                    </tr>
                </MDBTableHead>
                <MDBTableBody>
                    {Object.values(timeSlotMapping).map((timeSlotDisplay, timeIndex) => (
                        <tr key={timeIndex} style={{ textAlign: 'center' }}>
                            <th scope='row'>{timeSlotDisplay}</th>
                            {selectedAPIDates.map((date, dateIndex) => {
                                const count = countAppointments(date, getTimeSlotKey(timeSlotDisplay));
                                return (
                                    <td key={dateIndex}>
                                        <MDBBadge
                                            color={getBadgeColor(count)}
                                            pill
                                            style={{ textAlign: 'center', display: 'block', margin: 'auto' }}
                                            onClick={() => handleBadgeClick(date, timeSlotDisplay)}
                                        >
                                            {count} Appointments
                                        </MDBBadge>
                                    </td>
                                );
                            })}
                        </tr>
                    ))}
                </MDBTableBody>
            </MDBTable>

            <MDBModal open={modal.isOpen} onClose={() => setModal({ ...modal, isOpen: false })} tabIndex='-1'>
                <MDBModalDialog scrollable>
                    <MDBModalContent style={{ width: '60vw' }} >
                        <MDBModalHeader>
                            <MDBModalTitle>
                                <p>Appointments for {modal.date} </p>
                                <p>{modal.timeSlot}</p>
                            </MDBModalTitle>
                            <MDBBtn
                                className='btn-close'
                                color='none'
                                onClick={() => setModal({ ...modal, isOpen: false })}
                            ></MDBBtn>
                        </MDBModalHeader>
                        <MDBModalBody>
                            <MDBTable align='middle'>
                                <MDBTableHead>
                                    <tr style={{ textAlign: 'center' }}>
                                        <th scope='col' width='20%'>Customer Name & Phone Number</th>
                                        <th scope='col'>Pet Name</th>
                                        <th scope='col'>Date & Timeslot</th>
                                        <th scope='col'>Veterinarian</th>
                                        <th scope='col'>Status</th>
                                        <th scope='col'>Booking Price</th>
                                        <th scope='col'>Note</th>
                                    </tr>
                                </MDBTableHead>
                                <MDBTableBody style={{ textAlign: 'center' }}>
                                    {modal.appointments.map((app) => (
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
                                                <p className='fw-normal mb-1'>{app.veterinarianName}</p>
                                            </td>
                                            <td>
                                                <MDBBadge color={app.isCancel ? 'danger' : app.isCheckUp ? 'success' : app.isCheckIn ? 'warning' : 'secondary'} pill>
                                                    {app.isCancel ? "Cancelled" : app.isCheckUp ? "Checked Up" : app.isCheckIn ? "Checked In" : "Active"}
                                                </MDBBadge>
                                            </td>
                                            <td>
                                                <p className='fw-bold mb-1'>{app.bookingPrice}</p>
                                                <p className='text-muted mb-0'>{app.appointmentType}</p>
                                            </td>
                                            <td>
                                                <p className='fw-normal mb-1'>{app.appointmentNotes}</p>
                                            </td>
                                        </tr>
                                    ))}
                                </MDBTableBody>
                            </MDBTable>
                        </MDBModalBody>
                        <MDBModalFooter>
                            <MDBBtn color='secondary' onClick={() => setModal({ ...modal, isOpen: false })}>
                                Close
                            </MDBBtn>
                        </MDBModalFooter>
                    </MDBModalContent>
                </MDBModalDialog>
            </MDBModal>
        </div>
    );
}

export default WorkSchedule;
