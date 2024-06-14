import {
    MDBBtn,
    MDBCheckbox,
    MDBCol,
    MDBInput,
    MDBRow
} from 'mdb-react-ui-kit';

function StaffAppointmentModalForm() {
    const [user, setUser] = useUser();
    const [petList, setPetList] = useState([]);
    const [timeslotList, setTimeSlotList] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [centredModal, setCentredModal] = useState(false);
    const [selectedPet, setSelectedPet] = useState(null);

    const getPetList = async () => {
        try {
            const response = await fetch(`https://localhost:7206/api/accounts/pets/${user.id}`, {
                method: 'GET', // *GET, POST, PUT, DELETE, etc.
                headers: {
                    'Content-Type': 'application/json'
                },
                credentials: 'include'
            });
            if (!response.ok) {
                throw new Error("Error fetching data");
            }
            var userData = await response.json();
            setPetList(userData);
            console.log(userData);
        } catch (error) {
            toast.error('Error getting user details!');
            console.error(error.message);
        } finally {
            setIsLoading(false);
        }
    };

    const getTimeslots = async () => {
        try {
            const response = await fetch(`https://localhost:7206/api/TimeSlots`, {
                method: 'GET', // *GET, POST, PUT, DELETE, etc.
                headers: {
                    'Content-Type': 'application/json'
                },
                credentials: 'include'
            });
            if (!response.ok) {
                throw new Error("Error fetching data");
            }
            var userData = await response.json();
            setTimeSlotList(userData);
            console.log(userData);
        } catch (error) {
            toast.error('Error getting user details!');
            console.error(error.message);
        } finally {
            setIsLoading(false);
        }
    };
    if (!isLoading) {
        return (<div>Loading...</div>);
    }
    return (
        <form>
            <MDBRow className='mb-4'>
                <MDBCol>
                    <MDBInput id='ownerName' label='Owner Name' />
                </MDBCol>
                <MDBCol>
                    <MDBInput id='phone' label='Phone Number' type='tel' />
                </MDBCol>
            </MDBRow>

            <MDBRow className='mb-4'>
                <MDBCol>
                    <MDBInput id='petName' label='Pet Name' />
                </MDBCol>
                <MDBCol>
                    <MDBInput id='petAge' label='Pet Age' type='number' min='0' />
                </MDBCol>
                <MDBCol>
                    <MDBInput id='petAge' label='Pet breed' type='text' />
                </MDBCol>
                <MDBCol>
                    <MDBCheckbox label="Is male"></MDBCheckbox>
                </MDBCol>

            </MDBRow>
            <MDBRow className='mb-4'>
                <MDBCol>
                    <MDBInput id='timeSlot' label='Meet time' type='time' defaultValue={'00:00'}></MDBInput>
                </MDBCol>
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
        </form>
    );
}

export default StaffAppointmentModalForm;