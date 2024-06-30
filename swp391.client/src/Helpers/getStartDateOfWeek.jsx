const GetStartDateOfWeek = (week, year) => {
    const janFirst = new Date(year, 0, 1);
    const days = (week - 1) * 7;
    const startDate = new Date(janFirst.setDate(janFirst.getDate() + days));
    const dayOfWeek = startDate.getDay();
    const diff = startDate.getDate() - dayOfWeek + (dayOfWeek === 0 ? -6 : 1);
    return new Date(startDate.setDate(diff));
};
export default GetStartDateOfWeek;