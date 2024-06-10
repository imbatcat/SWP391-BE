import { useState, useEffect } from 'react';
import { MDBBadge, MDBBtn, MDBTable, MDBTableBody, MDBTableHead, MDBModal, MDBModalBody, MDBModalHeader, MDBModalFooter, MDBInput, MDBModalDialog, MDBModalContent, MDBModalTitle, MDBCol, MDBRow, MDBCheckbox } from 'mdb-react-ui-kit';
import SideNav from '../../Component/SideNav/SideNav';

function AdminPet() {
    const [pets, setPets] = useState([]);
    const [selectedPet, setSelectedPet] = useState(null);
    const [basicModal, setBasicModal] = useState(false);

    useEffect(() => {
        async function fetchData() {
            try {
                const response = await fetch('https://localhost:7206/api/Pets', {
                    method: 'GET',
                    credentials: 'include',
                    headers: {
                        // 'Content-Type': 'application/x-www-form-urlencoded',
                    }
                });
                if (!response.ok) {
                    throw new Error("Error fetching data");
                }
                const data = await response.json();
                setPets(data);
                console.log(data);
            } catch (error) {
                console.error(error.message);
            }
        }

        fetchData();
    }, []);

    const toggleOpen = (pet = null) => {
        setSelectedPet(pet);
        setBasicModal(!basicModal);
    };

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setSelectedPet(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    const handleSaveChanges = async () => {
        try {
            const response = await fetch(`https://localhost:7206/api/Pets`, {
                method: 'GET',
                credentials: 'include',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(selectedPet),
            });

            if (!response.ok) {
                throw new Error('Error updating data');
            }

            const updatedPet = await response.json();
            setPets(prevPets => prevPets.map(pet => (pet.id === updatedPet.id ? updatedPet : pet)));
            toggleOpen();
        } catch (error) {
            console.error(error.message);
        }
    };

    return (
        <div>
            <SideNav />
            <MDBTable align='middle'>
                <MDBTableHead>
                    <tr>
                        <th scope='col'>Name</th>
                        <th scope='col'>Email</th>
                        <th scope='col'>Phone Number</th>
                        <th scope='col'>Gender</th>
                        <th scope='col'>Status</th>
                        <th scope='col'>Actions</th>
                    </tr>
                </MDBTableHead>
                <MDBTableBody>
                    {pets.map((pet) => (
                        <tr key={pet.id}>
                            <td>
                                <div className='d-flex align-items-center'>
                                    <img
                                        src={pet.imgUrl}
                                        alt='pet img'
                                        style={{ width: '45px', height: '45px' }}
                                        className='rounded-circle'
                                    />
                                    <div className='ms-3'>
                                        <p className='fw-bold mb-1'>{pet.petName}</p>
                                        <p className='text-muted mb-0'>{pet.petBreed}</p>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <p className='fw-normal mb-1'>{pet.petAge}</p>
                            </td>
                            <td>
                                <p className='fw-normal mb-1'>{pet.description}</p>
                            </td>
                            <td>
                                <p className='fw-normal mb-1'>{pet.isMale ? "Male" : "Female"}</p>
                            </td>
                            <td>
                                <MDBBadge color={pet.isDisabled ? 'danger' : 'success'} pill>
                                    {pet.isDisabled ? "Disabled" : "Active"}
                                </MDBBadge>
                            </td>
                            <td>
                                <MDBBtn color='link' rounded size='sm' onClick={() => toggleOpen(pet)}>
                                    Edit
                                </MDBBtn>
                            </td>
                        </tr>
                    ))}
                </MDBTableBody>
            </MDBTable>

            {selectedPet && (
                <MDBModal open={basicModal} onClose={() => setBasicModal(false)} tabIndex='-1'>
                    <MDBModalDialog centered>
                        <MDBModalContent>
                            <MDBModalHeader>
                                <MDBModalTitle>Edit Pet</MDBModalTitle>
                                <MDBBtn className='btn-close' color='none' onClick={toggleOpen}></MDBBtn>
                            </MDBModalHeader>
                            <MDBModalBody>
                                <form>
                                    <MDBRow className='mb-4'>
                                        <MDBCol>
                                            <MDBInput
                                                label="Full Name"
                                                name="fullName"
                                                value={selectedPet.petName}
                                                onChange={handleInputChange}
                                            />
                                        </MDBCol>
                                        <MDBCol>
                                            <MDBInput
                                                label="Pet ID"
                                                name="petId"
                                                value={selectedPet.petId}
                                                onChange={handleInputChange}
                                            />
                                        </MDBCol>
                                    </MDBRow>
                                    <MDBRow className='mb-4'>
                                        <MDBCol>
                                            <MDBInput
                                                label="Email"
                                                name="email"
                                                value={selectedPet.imgUrl}
                                                onChange={handleInputChange}
                                            />
                                        </MDBCol>

                                    </MDBRow>

                                    <MDBRow className='mb-4'>
                                        <MDBCol>
                                            <MDBInput
                                                label="Phone Number"
                                                name="phoneNumber"
                                                value={selectedPet.petName}
                                                onChange={handleInputChange}
                                            />
                                        </MDBCol>
                                        <MDBCol>
                                            <MDBCheckbox label="Is male"></MDBCheckbox>
                                        </MDBCol>

                                    </MDBRow>
                                </form>

                                <MDBInput
                                    type="select"
                                    label="Gender"
                                    name="isMale"
                                    value={selectedPet.isMale ? "Male" : "Female"}
                                    onChange={(e) => handleInputChange({ target: { name: "isMale", value: e.target.value === "Male" } })}
                                >
                                    <option value="Male">Male</option>
                                    <option value="Female">Female</option>
                                </MDBInput>

                            </MDBModalBody>
                            <MDBModalFooter>
                                <MDBBtn color='secondary' onClick={toggleOpen}>Close</MDBBtn>
                                <MDBBtn color='success' style={{ color: 'black' }} onClick={handleSaveChanges}>Save changes</MDBBtn>
                            </MDBModalFooter>
                        </MDBModalContent>
                    </MDBModalDialog>
                </MDBModal>
            )}
        </div>
    );
}

export default AdminPet;
