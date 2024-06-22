import React, { useState, useEffect } from 'react';
import './YearWeekSelector.css';
const YearWeekSelector = ({ onYearWeekChange }) => {
  const [years, setYears] = useState([]);
  const [weeks, setWeeks] = useState([]);
  const [selectedYear, setSelectedYear] = useState(new Date().getFullYear());
  const [selectedWeek, setSelectedWeek] = useState(1);

  useEffect(() => {
    populateYears();
    populateWeeks(selectedYear);
  }, [selectedYear]);

  useEffect(() => {
    onYearWeekChange(selectedYear, selectedWeek);
  }, [selectedYear, selectedWeek, onYearWeekChange]);

  const populateYears = () => {
    const currentYear = new Date().getFullYear();
    const startYear = 2015; // Adjust the start year if needed
    const yearArray = [];
    for (let year = startYear; year <= currentYear; year++) {
      yearArray.push(year);
    }
    setYears(yearArray);
  };

  const populateWeeks = (year) => {
    const weekArray = [];
    for (let week = 1; week <= 52; week++) {
      const startDate = getStartDateOfWeek(week, year);
      const endDate = new Date(startDate);
      endDate.setDate(endDate.getDate() + 6);
      weekArray.push({
        week,
        label: `${formatDate(startDate)} To ${formatDate(endDate)}`
      });
    }
    setWeeks(weekArray);
    setSelectedWeek(getCurrentWeek());
  };

  const getStartDateOfWeek = (week, year) => {
    const janFirst = new Date(year, 0, 1);
    const days = (week - 1) * 7;
    const startDate = new Date(janFirst.setDate(janFirst.getDate() + days));
    const dayOfWeek = startDate.getDay();
    const diff = startDate.getDate() - dayOfWeek + (dayOfWeek === 0 ? -6 : 1);
    return new Date(startDate.setDate(diff));
  };

  const formatDate = (date) => {
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0');
    return `${day}/${month}`;
  };
  const getCurrentWeek = () => {
    const currentDate = new Date();
    const startOfYear = new Date(currentDate.getFullYear(), 0, 1);
    const dayOfYear = ((currentDate - startOfYear + 86400000) / 86400000);
    
    // Adjust for start of the week (Monday as the first day of the week)
    const startOfWeekAdjustment = (startOfYear.getDay() || 7) - 1; // startOfYear.getDay() returns 0 for Sunday
    
    // Calculate the current week number
    return Math.ceil((dayOfYear + startOfWeekAdjustment) / 7);
  };
  
  return (
<div>
    <div>
      <select className='yearweek-btn' id="year" value={selectedYear} onChange={(e) => setSelectedYear(parseInt(e.target.value))}>
        {years.map((year) => (
          <option key={year} value={year}>
            {year}
          </option>
        ))}
      </select>
    </div>
    <div>
      <select className='yearweek-btn' id="week" value={selectedWeek} onChange={(e) => setSelectedWeek(parseInt(e.target.value))}>
        {weeks.map((week) => (
          <option key={week.week} value={week.week}>
            {week.label}
          </option>
        ))}
      </select>
    </div>
</div>
  );
};

export default YearWeekSelector;
