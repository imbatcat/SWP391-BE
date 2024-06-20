import {
    MDBBtn,
    MDBCheckbox,
    MDBCol,
    MDBInput,
    MDBRow
} from 'mdb-react-ui-kit';
import { useUser } from '../../Context/UserContext';
import { useState } from 'react';
import { toast } from 'react-toastify';
import { useEffect } from 'react';

function AppointmentForm() {
    const [user, setUser] = useUser();
    const [vetList, setVetList] = useState([]);
    const [petList, setPetList] = useState([]);
    const [timeslotList, setTimeSlotList] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [formData, setFormData] = useState({
        accountId: user.id,
        petId: '',
        timeslotId: '',
        veterinarianAccountId: '',
        appointmentDate: '',
        appointmentNotes: '',
        appointmentType: ''
    });

    const apis = [
        `https://localhost:7206/api/accounts/pets/${user.id}`,
        `https://localhost:7206/api/TimeSlots`,
        `https://localhost:7206/api/byRole/3`
    ];
    const getData = async () => {
        try {
            const promise = apis.map(api => fetch(api, {
                method: 'GET', // *GET, POST, PUT, DELETE, etc.
                headers: {
                    'Content-Type': 'application/json'
                },
                credentials: 'include'
            }));
            const [response1, response2, response3] = await Promise.all(promise);
            if (!response1.ok || !response2.ok || !response3.ok) {
                throw new Error("Error fetching data");
            }
            var petData = await response1.json();
            var timeslotData = await response2.json();
            var vetData = await response3.json();
            setPetList(petData);
            setTimeSlotList(timeslotData);
            setVetList(vetData);

            //console.log(petData);
            console.log(timeslotData);
        } catch (error) {
            toast.error('Error getting user details!');
            console.error(error.message);
        } finally {
            setIsLoading(false);
        }
    };
    useEffect(() => {
        getData();
    }, []);

    const addAppointment = async () => {
        try {
            console.log(formData);
            const response = await fetch('https://localhost:7206/api/Appointment', {
                method: 'POST',
                headers: {
                    'Content-type': 'application/json'
                },
                credentials: 'include',
                body: JSON.stringify({
                    formData
                })
            });

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            toast.success('Your appointment has been successfully registered!');
        } catch (error) {
            toast.error('Error registering appointment');
            console.error('There has been a problem with your fetch operation:', error);
        }

    };
    if (isLoading) {
        return (<div>Loading...</div>);
    }
    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({
            ...formData,
            [name]: value
        });
    };
    const handleSubmit = (e) => {
        e.preventDefault();
        addAppointment(formData);
    };

    return (
        <form onSubmit={handleSubmit}>
            <MDBRow className='mb-4'>
                <MDBCol>
                    <select name="petId" value={formData.petId} onChange={handleChange} data-mdb-select-init >
                        <option value="" disabled>Choose your pet</option>
                        {petList.map((pet, index) => (
                            <option key={index} value={pet.petId}>
                                {pet.petName}
                            </option>
                        ))}
                    </select>
                </MDBCol>
            </MDBRow>

            <MDBRow className='mb-4'>
                <MDBCol>
                    <select name="timeSlotId" value={formData.timeslotId} onChange={handleChange} data-mdb-select-init >
                        <option value="" disabled>Choose your time</option>
                        {timeslotList.map((timeslot, index) => (
                            <option key={index} value={timeslot.timeSlotId}>
                                {timeslot.startTime} - {timeslot.endTime}
                            </option>
                        ))}
                    </select>
                </MDBCol>
            </MDBRow>

            <MDBRow className='mb-4'>
                <MDBCol>
                    <select name="veterinarianAccountId" value={formData.veterinarianAccountId} onChange={handleChange} data-mdb-select-init >
                        <option value="" disabled>Choose your vet (optional)</option>
                        {vetList.map((vet, index) => (
                            <option key={index} value={vet.accoundId}>
                                {vet.fullName}
                            </option>
                        ))}
                    </select>
                </MDBCol>
            </MDBRow>

            <MDBRow className='mb-4'>
                <MDBCol>
                    <MDBInput id='appDate' name='appointmentDate' label='Appointment Date' type='date' value={formData.appointmentDate} onChange={handleChange} />
                </MDBCol>
            </MDBRow>

            <MDBRow className='mb-4'>
                <MDBCol>
                    <MDBInput id='appNotes' name='appointmentNotes' label='Additional Notes' type='text' size='lg' value={formData.appointmentNotes} onChange={handleChange} />
                </MDBCol>
            </MDBRow>

            <MDBRow className='mb-4'>
                <MDBCol>
                    <select name="appointmentType" value={formData.appointmentType} onChange={handleChange} data-mdb-select-init >
                        <option value="" disabled>Choose your payment method</option>
                        <option value="Deposit">Deposit</option>
                        <option value="Fully paid">Fully paid</option>
                    </select>
                </MDBCol>
            </MDBRow>

            <MDBBtn type='submit' outline color='dark' className='mb-4' block>
                Submit
            </MDBBtn>
        </form>
    );
}

export default AppointmentForm;