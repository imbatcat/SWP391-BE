import {
    MDBBtn,
    MDBCard,
    MDBCardBody,
    MDBCardImage,
    MDBCardText,
    MDBCol,
    MDBContainer,
    MDBModal,
    MDBModalDialog,
    MDBModalContent,
    MDBModalHeader,
    MDBModalTitle,
    MDBModalBody,
    MDBModalFooter,
    MDBIcon,
    MDBListGroup,
    MDBListGroupItem,
    MDBRow,
} from 'mdb-react-ui-kit';
import { useEffect, useState } from "react";
import { useUser } from "../../Context/UserContext";
import MainLayout from "../../Layouts/MainLayout";
import { toast } from 'react-toastify';
import { Link } from 'react-router-dom';
import UserSidebar from "../../Component/UserSidebar/UserSidebar";

function UserAppointments() {
    const [user, setUser] = useUser();
    const [appointmentList, setAppointmentList] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [centredModal, setCentredModal] = useState(false);
    const [selectedAppointment, setSelectedAppointment] = useState(null);

    const getAppointmentList = async (user) => {
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
            setAppointmentList(userData);
            console.log(userData);
        } catch (error) {
            toast.error('Error getting user details!');
            console.error(error.message);
        } finally {
            setIsLoading(false);
        }
    }

    useEffect(() => {
        if (user)
            getAppointmentList(user);
    }, [user])

    if (isLoading) {
        return <div>Loading...</div>; // Loading state
    }
    const toggleOpen = (Appointment = null) => {
        setSelectedAppointment(Appointment);
        setCentredModal(!centredModal);
    };
    return (
        <>
            <MainLayout>
                <section style={{ backgroundColor: '#eee' }}>
                    <MDBContainer className="py-5">
                        <MDBRow>
                            <MDBCol lg="4">
                                <UserSidebar></UserSidebar>
                            </MDBCol >

                            <MDBCol>
                                <MDBCard className="mb-4 mb-lg-0">
                                    <MDBCardBody className="p-0">
                                        <div className='Appointment-display-list'>
                                            {appointmentList.map((appointment, index) => (
                                                <div className="Appointment-item" key={index}>
                                                    <div className="Appointment-item-img-container">
                                                        <img className='Appointment-item-image' src={appointment.img} alt='' />
                                                    </div>
                                                    <div className='Appointment-info'>
                                                        <div className='Appointment-name-rating'>
                                                            <p>{appointment.appointmentName}</p>
                                                            <p>{appointment.isCat ? 'Cat' : 'Dog'}</p>
                                                        </div>
                                                        <>
                                                            <MDBBtn color='muted' onClick={() => toggleOpen(appointment)}>Detail</MDBBtn>
                                                        </>
                                                    </div>
                                                </div>
                                            ))}
                                        </div>

                                        {
                                            selectedAppointment && (
                                                <MDBModal tabIndex='-1' open={centredModal} onClose={() => setCentredModal(false)}>
                                                    <MDBModalDialog centered>
                                                        <MDBModalContent>
                                                            <MDBModalHeader>
                                                                <MDBModalTitle>Detail of {selectedAppointment.name}</MDBModalTitle>
                                                                <MDBBtn className='btn-close' color='none' onClick={toggleOpen}></MDBBtn>
                                                            </MDBModalHeader>
                                                            <MDBModalBody>
                                                                <p className='Appointment-detail'>Age: {selectedAppointment.appointmentAge}</p>
                                                                <p className='Appointment-detail'>Vaccination: {selectedAppointment.vaccinationHistory}</p>
                                                                <p className='Appointment-detail'>Gender: {selectedAppointment.isMale ? 'Male' : 'Female'}</p>
                                                                <p className='Appointment-detail'>Description: {selectedAppointment.description}</p>
                                                            </MDBModalBody>
                                                            <MDBModalFooter>
                                                            </MDBModalFooter>
                                                        </MDBModalContent>
                                                    </MDBModalDialog>
                                                </MDBModal>
                                            )
                                        }
                                    </MDBCardBody>
                                </MDBCard>
                            </MDBCol>
                        </MDBRow>
                    </MDBContainer>
                </section>
            </MainLayout >
        </>
    );
}

export default UserAppointments;