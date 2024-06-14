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
    const [petList, setPetList] = useState([]);
    const [timeslotList, setTimeSlotList] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [centredModal, setCentredModal] = useState(false);
    const [selectedPet, setSelectedPet] = useState(null);

    const apis = [
        `https://localhost:7206/api/accounts/pets/${user.id}`,
        `https://localhost:7206/api/TimeSlots`,
        ``
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
            const [response1, response2] = await Promise.all(promise);
            if (!response1.ok || !response2.ok) {
                throw new Error("Error fetching data");
            }
            var petData = await response1.json();
            var timeslotData = await response2.json();
            setPetList(petData);
            setTimeSlotList(timeslotData);

            console.log(petData);
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

    if (isLoading) {
        return (<div>Loading...</div>);
    }
    return (
        <form>
            <MDBRow className='mb-4'>
                <select data-mdb-select-init >
                    <option value="" disabled selected>Choose your pet</option>
                    {petList.map((pet, index) => (
                        <option key={index} value={pet.petId}>
                            <div className='Pet-info'>
                                <div className='Pet-name-rating'>
                                    <p>{pet.petName}</p>
                                </div>
                            </div>
                        </option>
                    ))}
                </select>

            </MDBRow>
            <MDBRow className='mb-4'>
                <select data-mdb-select-init >
                    <option value="" disabled selected>Choose your time</option>
                    {timeslotList.map((timeslot, index) => (
                        <option key={index} value={timeslot.timeslotId}>
                            <div className='timeslot-info'>
                                <div className='timeslot-name-rating'>
                                    <p>{timeslot.startTime} - {timeslot.endTime} </p>
                                </div>
                            </div>
                        </option>
                    ))}
                </select>

            </MDBRow>
            <MDBRow className='mb-4'>
                <MDBCol>
                    <MDBInput id='appDate' label='Appointment Date' type='date' ></MDBInput>
                </MDBCol>
            </MDBRow>
            <MDBRow className='mb-4'>
                <MDBCol>
                    <MDBInput id='appNotes' label='Additional Notes' type='text' size='lg'></MDBInput>
                </MDBCol>
            </MDBRow>
            <MDBBtn type='submit' outline color='dark' className='mb-4' block>
                Submit
            </MDBBtn>
            <input type="reset" value="Reset"></input>
        </form>
    );
}

export default AppointmentForm;