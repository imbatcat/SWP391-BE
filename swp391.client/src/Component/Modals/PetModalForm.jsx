import {
    MDBBtn,
    MDBCheckbox,
    MDBCol,
    MDBInput,
    MDBRow
} from 'mdb-react-ui-kit';
import { useState } from 'react';
import { toast } from 'react-toastify';
import { useUser } from '../../Context/UserContext';


function PetModalForm() {
    const [user, setUser] = useUser();
    const [petName, setPetName] = useState('');
    const [petAge, setPetAge] = useState(0);
    const [petBreed, setPetBreed] = useState('');
    const [isMale, setIsMale] = useState(false);
    const [isCat, setIsCat] = useState(false);
    const [petImg, setPetImg] = useState(null);
    const [petImgUrl, setPetImgUrl] = useState(null);
    const [petNotes, setPetNotes] = useState('');
    const [vaccinationHistory, setVaccinationHistory] = useState('');

    const createPetApi = async (e) => {
        e.preventDefault();
        console.log(user.id);
        const petInfo = {
            petName,
            petAge,
            petBreed,
            isMale,
            isCat,
            "imgUrl": petImgUrl,
            "description": petNotes,
            vaccinationHistory,
            "isDisable": false,
            "accountId": user.id
        };
        //console.log(JSON.stringify(petInfo));
        try {
            const response = await fetch('https://localhost:7206/api/Pets', {
                method: 'POST', // *GET, POST, PUT, DELETE, etc.
                headers: {
                    'Content-Type': 'application/json'
                },
                credentials: 'include',
                body: JSON.stringify(petInfo)
            });
            if (!response.ok) {

                throw new Error(response);
            }
            console.log('ok');
            toast.success(`${petName} has been added`);
        } catch (error) {
            console.error(error.message);
        }
    };

    // Handle changes for each input/checkbox
    const handleInputChange = (e) => {
        const { id, value } = e.target;

        switch (id) {
            case 'petName':
                setPetName(value);
                break;
            case 'petAge':
                setPetAge(value);
                break;
            case 'petBreed':
                setPetBreed(value);
                break;
            case 'isMale':
                setIsMale(value === 'true');
                break;
            case 'isCat':
                setIsCat(value === 'true');
                break;
            case 'petImgUrl':
                var reader = new FileReader();
                setPetImg(e.target.files[0]);

                reader.readAsDataURL(e.target.files[0]);
                reader.onload = () => {
                    setPetImgUrl(reader.result);
                };
                reader.onerror = (error) => {
                    console.error('Error', error);
                };
                break;
            case 'petNotes':
                setPetNotes(value);
                break;
            case 'vaccinationHistory':
                setVaccinationHistory(value);
                break;
            default:
                break;
        }
    };

    return (
        <form>
            <MDBRow className='mb-4'>
                <MDBCol>
                    <MDBInput id='petName' label='Pet Name' value={petName} onChange={handleInputChange} />
                </MDBCol>
                <MDBCol>
                    <MDBInput id='petAge' label='Pet Age' type='date' min='0' value={petAge} onChange={handleInputChange} />
                </MDBCol>
                <MDBCol>
                    <MDBInput id='petBreed' label='Pet breed' type='text' value={petBreed} onChange={handleInputChange} />
                </MDBCol>
            </MDBRow>
            <MDBRow className='mb-4'>

                <MDBCol>
                    <MDBCheckbox id='isMale' label="Is male" checked={isMale} onChange={(e) => setIsMale(e.target.checked)}></MDBCheckbox>
                </MDBCol>
                <MDBCol>
                    <MDBCheckbox id='isCat' label="Is cat" checked={isCat} onChange={(e) => setIsCat(e.target.checked)}></MDBCheckbox>
                </MDBCol>
            </MDBRow>
            <MDBRow className='mb-4'>
                <MDBCol>
                    <MDBInput id='petImgUrl' type='file' name='photo' accept='image/*' onChange={handleInputChange} />
                </MDBCol>
                {petImgUrl && (
                    <div>
                        <img
                            src={URL.createObjectURL(petImg)}
                            alt='pet photo'
                            height={'200px'}>

                        </img>
                    </div>

                )}
            </MDBRow>
            <MDBRow className='mb-4'>
                <MDBCol>
                    <MDBInput id='petNotes' label='Description' type='text' size='lg' value={petNotes} onChange={handleInputChange}></MDBInput>
                </MDBCol>
            </MDBRow>
            <MDBRow className='mb-4'>
                <MDBCol>
                    <MDBInput id='vaccinationHistory' label='Vaccination history' type='text' size='lg' value={vaccinationHistory} onChange={handleInputChange}></MDBInput>
                </MDBCol>
            </MDBRow>

            <MDBBtn onClick={(e) => createPetApi(e)} type='submit' outline color='dark' className='mb-4' block >
                Submit
            </MDBBtn>
            <input type="reset" value="Reset"></input>
        </form >
    );
}

export default PetModalForm;