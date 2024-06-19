import React, { useEffect, useState } from 'react';
import { MDBBadge, MDBTable, MDBTableBody, MDBTableHead } from 'mdb-react-ui-kit';
import SideNavForVet from '../../Component/SideNavForVet/SideNavForVet';
import YearWeekSelector from '../../Component/YearWeekSelector/YearWeekSelector';

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

function WorkSchedule() {
  const [selectedDisplayDates, setSelectedDisplayDates] = useState([]);
  const [selectedAPIDates, setSelectedAPIDates] = useState([]);
  const [appointments, setAppointments] = useState([]);

  useEffect(() => {
    async function fetchData() {
      try {
        const response = await fetch('https://localhost:7206/api/Appointment', {
          method: 'GET',
          credentials: 'include',
        });
        if (!response.ok) {
          throw new Error("Error fetching data");
        }
        const data = await response.json();
        setAppointments(data);
        console.log(data);
      } catch (error) {
        console.error(error.message);
      }
    }

    fetchData();
  }, []);

  const handleYearWeekChange = (year, week) => {
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
  };

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
        <tr style={{ textAlign: 'center' }}>
            <th scope='row'>7:00 - 8:30</th>
            {selectedAPIDates.map((date, index) => {
              const count = countAppointments(date, '7:00 - 8:30');
              return (
                <td key={index}>
                  <MDBBadge 
                    color={getBadgeColor(count)} 
                    pill
                    style={{ textAlign: 'center', display: 'block', margin: 'auto' }}
                  >
                    {count} Appointments
                  </MDBBadge>
                </td>
              );
            })}
          </tr>
          <tr style={{ textAlign: 'center' }}>
            <th scope='row'>8:30 - 10:00</th>
            {selectedAPIDates.map((date, index) => {
              const count = countAppointments(date, '8:30 - 10:00');
              return (
                <td key={index}>
                  <MDBBadge 
                    color={getBadgeColor(count)} 
                    pill
                    style={{ textAlign: 'center', display: 'block', margin: 'auto' }}
                  >
                    {count} Appointments
                  </MDBBadge>
                </td>
              );
            })}
          </tr>
        </MDBTableBody>
      </MDBTable>
    </div>
  );
}

export default WorkSchedule;
