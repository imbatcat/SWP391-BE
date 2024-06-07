import { useState, useEffect } from 'react';
import './PetList.css'
import {
    MDBBtn,
    MDBModal,
    MDBModalDialog,
    MDBModalContent,
    MDBModalHeader,
    MDBModalTitle,
    MDBModalBody,
    MDBModalFooter,
} from 'mdb-react-ui-kit';
import { useUser } from '../../Context/UserContext';
import { toast } from 'react-toastify';

export default function PetList() {
    const [user, setUser] = useUser();
    const [petList, setPetList] = useState(null);
    const [centredModal, setCentredModal] = useState(false);
    const [selectedPet, setSelectedPet] = useState(null);

    useEffect(() => {
        const getPetList = async () => {
            console.log(user.id);
            try {
                const response = await fetch(`https://localhost:7206/api/accounts/pets/${user.id}`, {
                    method: 'GET',
                    credentials: 'include',
                });
                if (!response.ok) {
                    throw new Error(response.message);
                }
                const pets = await response.json();
                console.log(pets);
                setPetList(pets);
            } catch (error) {
                toast.error('Error fetching data!');
                console.error(error.message);
            }
        };

        getPetList();
    }, []);

    const toggleOpen = (Pet = null) => {
        setSelectedPet(Pet);
        setCentredModal(!centredModal);
    };

    return (
        <div className='Pet-display'>
            <div className='Pet-display-list'>
                {petList.map((pet) => (
                    <div className="Pet-item" key={pet.id}>
                        <div className="Pet-item-img-container">
                            <img className='Pet-item-image' src={pet.img} alt='' />
                        </div>
                        <div className='Pet-info'>
                            <div className='Pet-name-rating'>
                                <p>{pet.petName}</p>
                                <p>{pet.isCat ? 'Cat' : 'Dog'}</p>
                            </div>
                            <>
                                <MDBBtn color='muted' onClick={() => toggleOpen(pet)}>Detail</MDBBtn>
                            </>
                        </div>
                    </div>
                ))}
            </div>

            {selectedPet && (
                <MDBModal tabIndex='-1' open={centredModal} onClose={() => setCentredModal(false)}>
                    <MDBModalDialog centered>
                        <MDBModalContent>
                            <MDBModalHeader>
                                <MDBModalTitle>Detail of {selectedPet.name}</MDBModalTitle>
                                <MDBBtn className='btn-close' color='none' onClick={toggleOpen}></MDBBtn>
                            </MDBModalHeader>
                            <MDBModalBody>
                                <p className='Pet-detail'>Age: {selectedPet.petAge}</p>
                                <p className='Pet-detail'>Vaccination: {selectedPet.vaccinationHistory}</p>
                                <p className='Pet-detail'>Gender: {selectedPet.isMale ? 'Male' : 'Female'}</p>
                                <p className='Pet-detail'>Description: {selectedPet.description}</p>
                            </MDBModalBody>
                            <MDBModalFooter>
                            </MDBModalFooter>
                        </MDBModalContent>
                    </MDBModalDialog>
                </MDBModal>
            )}
        </div>
    );
}
