import { useState } from 'react';
import { Pets } from '../../Component/Data/Pet';
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

export default function PetList() {
  const [centredModal, setCentredModal] = useState(false);
  const [selectedOrchid, setSelectedOrchid] = useState(null);

  const toggleOpen = (orchid = null) => {
    setSelectedOrchid(orchid);
    setCentredModal(!centredModal);
  };

  return (
    <div className='orchid-display'>
      <div className='orchid-display-list'>
        {Pets.map((pet) => (
          <div className="orchid-item" key={pet.id}>
            <div className="orchid-item-img-container">
              <img className='orchid-item-image' src={pet.img} alt='' />
            </div>
            <div className='orchid-info'>
              <div className='orchid-name-rating'>
                <p>{pet.name}</p>
                <p>{pet.species}</p>
              </div>
              <>
                <MDBBtn color='muted' onClick={() => toggleOpen(pet)}>Detail</MDBBtn>
              </>
            </div>
          </div>
        ))}
      </div>

      {selectedOrchid && (
        <MDBModal tabIndex='-1' open={centredModal} onClose={() => setCentredModal(false)}>
          <MDBModalDialog centered>
            <MDBModalContent>
              <MDBModalHeader>
                <MDBModalTitle>Detail of {selectedOrchid.name}</MDBModalTitle>
                <MDBBtn className='btn-close' color='none' onClick={toggleOpen}></MDBBtn>
              </MDBModalHeader>
              <MDBModalBody>
                <p className='orchid-detail'>Color: {selectedOrchid.color}</p>
                <p className='orchid-detail'>Origin: {selectedOrchid.origin}</p>
                <p className='orchid-detail'>Category: {selectedOrchid.category}</p>
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
