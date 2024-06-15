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
import Footer from "../Component/Footer/Footer";
import NavBar2 from "../Component/NavBar/NavBar2";
import '../Layouts/MainLayout.css';
import { Tooltip } from 'react-tooltip';
import 'react-tooltip/dist/react-tooltip.css';
import { useState } from 'react';
import SelectModal from '../Component/Modals/SelectModal';
import CheckAuth from '../Helpers/CheckAuth';
import { GoogleLogin } from '@react-oauth/google';
function MainLayout({ children }) {
    const [basicModal, setBasicModal] = useState(false);

    const toggleOpen = () => setBasicModal(!basicModal);
    const getProfile = (credential) => {
        fetch(`https://localhost:7206/api/ApplicationAuth/signinGoogle
`, {
            method: 'POST',

            headers: {
                'Content-type': 'application/json'
            },      
        body: JSON.stringify({
            "token":credential
        })
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json(); // Parses JSON response into native JavaScript objects
            })
            .then(data => {
                console.log(data); // Assuming setProfile is a function to update your component's state with the user profile
            })
            .catch(error => {
                console.error('There has been a problem with your fetch operation:', error);
            });
    };
    return (
        <div>
            <GoogleLogin onSuccess={credentialResponse => {
                console.log(credentialResponse);
                getProfile(credentialResponse.credential);
            }}
                onError={() => {
                    console.log('Login Failed');
                }}>
            </GoogleLogin>

            <NavBar2 />
           
            <main>
                {children}
            </main>
            <div className="fixed-bottom-right">
                <button className="static-button" data-tooltip-id="add-button" onClick={toggleOpen}>+</button>
                <Tooltip id='add-button' content={'Make An Appointment'}></Tooltip>

                <MDBModal open={basicModal} onClose={() => setBasicModal(false)} tabIndex='-1'>
                    <SelectModal toggleOpen={toggleOpen}>
                    </SelectModal>
                </MDBModal>
            </div>
            <Footer></Footer>
        </div>
    );
};

export default MainLayout;