import React, { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import {
    MDBCard,
    MDBCardBody,
    MDBCardTitle,
    MDBCardText,
    MDBRow,
    MDBCol,
    MDBBtn,
    MDBCardHeader,
    MDBBadge,
    MDBInputGroup
} from 'mdb-react-ui-kit';
import { toast } from 'react-toastify';

async function fetchOwnerAndPetData(accountId, petId, vetId) {
    try {
        const [accountResponse, petResponse, vetResponse] = await Promise.all([
            fetch(`https://localhost:7206/api/Accounts/${accountId}`, {
                method: 'GET',
                credentials: 'include',
            }),
            fetch(`https://localhost:7206/api/Pets/${petId}`, {
                method: 'GET',
                credentials: 'include',
            }),
            fetch(`https://localhost:7206/api/Accounts/${vetId}`, {
                method: 'GET',
                credentials: 'include',
            })
        ]);

        if (!accountResponse.ok) {
            throw new Error("Error fetching accountdata");
        }
        if (!petResponse.ok) {
            throw new Error("Error fetching petdata");
        }

        const accountData = await accountResponse.json();
        const petData = await petResponse.json();
        const vetData = await vetResponse.json();

        return { accountData, petData, vetData };
    } catch (error) {
        console.error(error.message);
        return { accountData: null, petData: null };
    }
}

function MedicalRecord() {
    const location = useLocation();
    const appointment = location.state;
    const [ownerData, setOwnerData] = useState(null);
    const [petData, setPetData] = useState(null);
    const [vetData, setVetData] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [formData, setFormData] = useState({
        petId: appointment.petId,
        appointmentId: appointment.appointmentId,
        petWeight: '',
        symptoms: '',
        allergies: '',
        diagnosis: '',
        additionalNotes: '',
        followUpAppointmentDate: '',
        followUpAppointmentNotes: '',
        drugPrescriptions: ''
    });

    useEffect(() => {
        console.log(appointment);
        if (appointment) {
            fetchOwnerAndPetData(appointment.accountId, appointment.petId, appointment.veterinarianId)
                .then(({ accountData, petData, vetData }) => {
                    setOwnerData(accountData);
                    setPetData(petData);
                    setVetData(vetData);
                    setLoading(false);
                })
                .catch((error) => {
                    setError(error.message);
                    setLoading(false);
                });
        } else {
            setError("No appointment data provided");
            setLoading(false);
        }
    }, [appointment]);

    if (loading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>Error: {error}</div>;
    }

    if (!ownerData || !petData) {
        return <div>No data available</div>;
    }

    const calculatePetAge = (petDOB) => {
        const birthDate = new Date(petDOB);
        const today = new Date();
        
        let years = today.getFullYear() - birthDate.getFullYear();
        let months = today.getMonth() - birthDate.getMonth();
        let days = today.getDate() - birthDate.getDate();
        
        if (days < 0) {
            months--;
            days += new Date(today.getFullYear(), today.getMonth(), 0).getDate();
        }

        if (months < 0) {
            years--; 
            months += 12;
        }
        
        return `${years} Year${years !== 1 ? 's' : ''} ${months} Month${months !== 1 ? 's' : ''} and ${days} Day${days !== 1 ? 's' : ''}`;
    };

    
    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({
            ...formData,
            [name]: value
        });
        console.log(formData);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const url = 'https://localhost:7206/api/MedicalRecords';
        try {
            const response = await fetch(url, {
                method: 'POST',
                credentials: 'include',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(formData)
            });

            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }

            const responseData = await response.json();
            console.log('Success:', responseData);
            toast.success('Submit Medical Record Success')
        } catch (error) {
            console.error('Error:', error);
        }
    };
    return (
        <MDBCard style={{ minHeight: '60vw', maxWidth: '50vw', margin: 'auto', marginTop: '50px' }}>
            <MDBCardHeader style={{ textAlign: 'center', fontSize: '3vw' }}>Medical Record</MDBCardHeader>
            <MDBCardBody style={{height:'5'}} scrollable >
                <MDBRow style={{ marginLeft: '15px', marginRight: '15px' }}>
                    <MDBCol sm='6'>
                        <MDBCard>
                            <MDBCardBody>
                                <MDBCardTitle style={{textAlign:'center'}}>Owner Information</MDBCardTitle>
                                <MDBCardText>
                                <div className='d-flex' style={{justifyContent:'center'}}>
                                        <img
                                            src='https://mdbootstrap.com/img/new/avatars/8.jpg'
                                            alt=''
                                            style={{ width: '45px', height: '45px' }}
                                            className='rounded-circle'
                                        />     
                                </div>                                               
                                        <div style={{textAlign:'center'}}>
                                            <p className='fw-bold mb-1'>{ownerData.fullName}</p>
                                            <p className='text-muted mb-0'>{ownerData.phoneNumber}</p>
                                            <p className='text-muted mb-0' style={{fontSize:'0.9vw'}}>{ownerData.email}</p>
                                        </div>                
                                </MDBCardText>
                            </MDBCardBody>
                        </MDBCard>
                        <br/>
                        <MDBCard>
                            <MDBCardBody>
                                <MDBCardTitle style={{textAlign:'center'}}>Veterinarian</MDBCardTitle>
                                <MDBCardText>
                                <div className='d-flex' style={{justifyContent:'center'}}>
                                        <img
                                            src='https://mdbootstrap.com/img/new/avatars/8.jpg'
                                            alt=''
                                            style={{ width: '45px', height: '45px' }}
                                            className='rounded-circle'
                                        />     
                                </div>                                               
                                        <div style={{textAlign:'center'}}>
                                            <p className='fw-bold mb-1'>{vetData.fullName}</p>
                                            <p className='text-muted mb-0' style={{fontSize:'1vw'}}>{vetData.position}</p>
                                            <br/>
                                
                                        </div>                
                                </MDBCardText>
                            </MDBCardBody>
                        </MDBCard>
                    </MDBCol>
                    <MDBCol sm='6'>
                    <MDBCard>
                            <MDBCardBody>
                                <MDBCardTitle style={{textAlign:'center'}}>Pet Information</MDBCardTitle>
                                <MDBCardText>
                                <div className='d-flex' style={{justifyContent:'center'}}>
                                        <img
                                            src={petData.imgUrl}
                                            alt='petimg'
                                            style={{ width: '45px', height: '45px' }}
                                            className='rounded-circle'
                                        />     
                                </div>                    

                                <div style={{textAlign:'center'}}>
                                    <p className='fw-bold mb-1'>{petData.petName}</p>
                                    <p className='text-muted mb-0'>{petData.petAge}</p>                                           
                                </div>

                                <div style={{textAlign:'center'}}>
                                <MDBBadge color={petData.isCat ? 'warning' : 'primary'} pill>
                                    {petData.isCat ? "Cat" : "Dog"}
                                </MDBBadge>
                                <MDBBadge color={petData.isMale ? 'primary' : 'danger'} pill>
                                    {petData.isMale ? "Male" : "Female"}
                                </MDBBadge>
                                </div>   
                                <p className='text-muted mb-0'>- Pet Age: {calculatePetAge(petData.petAge)} </p>
                                <p className='text-muted mb-0'>- Pet Breed: {petData.petBreed} </p>
                                <p className='text-muted mb-0'>- Vaccination: {petData.vaccinationHistory} </p>       
                                <p className='text-muted mb-0'>"{petData.description}" </p>             
                                </MDBCardText>
                            </MDBCardBody>
                        </MDBCard>
                    </MDBCol>
                </MDBRow>
                <br/>

                <MDBRow style={{ marginLeft: '15px', marginRight: '15px' }}>
                    <MDBCard>
                        <MDBCardHeader>Medical Record Information</MDBCardHeader>
                            <MDBCardBody>
                                <MDBCardTitle>Special title treatment</MDBCardTitle>
                                <MDBCardText>
                                     <form onSubmit={handleSubmit} style={{ maxWidth: '600px', margin: 'auto' }}>
                                        
                                        <div>
                                            <MDBInputGroup  className='mb-3' textBefore='Pet Weight' textAfter='kg'>
                                                <input className='form-control' style={{width:'5vw', textAlign:'center'}}  type="number" 
                                                        name="petWeight" value={formData.petWeight} onChange={handleChange} required />
                                            </MDBInputGroup>                                        
                                        </div>
                                        <div>
                                            <MDBInputGroup  className='mb-3' textBefore='Symptoms' >
                                                <input className='form-control' type="text" name="symptoms" value={formData.symptoms} onChange={handleChange} required />
                                            </MDBInputGroup>                                      
                                        </div>
                                        <div>
                                            <MDBInputGroup  className='mb-3' textBefore='Allergies' >
                                            <input className='form-control' type="text" name="allergies" value={formData.allergies} onChange={handleChange} required />
                                            </MDBInputGroup>     
                                        </div>
                                        <div>
                                            <MDBInputGroup  className='mb-3' textBefore='Diagnosis' >
                                            <input className='form-control' type="text" name="diagnosis" value={formData.diagnosis} onChange={handleChange} required />
                                            </MDBInputGroup>   
                                        </div>
                                        <div>
                                            <MDBInputGroup  className='mb-3' textBefore='Additional Notes' >
                                            <input className='form-control' type="text" name="additionalNotes" value={formData.additionalNotes} onChange={handleChange} required />
                                            </MDBInputGroup>                          
                                        </div>
                                        <div>
                                            <MDBInputGroup  className='mb-3' textBefore='Follow-Up Appointment Date' >
                                            <input className='form-control' type="date" name="followUpAppointmentDate" value={formData.followUpAppointmentDate} onChange={handleChange} required />
                                            </MDBInputGroup>  
                                        </div>
                                        <div>
                                            <MDBInputGroup  className='mb-3' textBefore='Follow-Up Appointment Notes' >
                                            <input className='form-control' type="text" name="followUpAppointmentNotes" value={formData.followUpAppointmentNotes} onChange={handleChange} required />
                                            </MDBInputGroup>  
                                        </div>
                                        <div>
                                            <MDBInputGroup  className='mb-3' textBefore='Drug Prescriptions' >
                                            <input type="text" name="drugPrescriptions" value={formData.drugPrescriptions} onChange={handleChange} required />
                                            </MDBInputGroup>  
                                        </div>
                                    </form>
                                </MDBCardText>
                                <MDBBtn type="submit" onClick={handleSubmit}>Submit</MDBBtn>
                            </MDBCardBody>
                    </MDBCard>
                </MDBRow>
            </MDBCardBody>
        </MDBCard>
    );
}

export default MedicalRecord;
