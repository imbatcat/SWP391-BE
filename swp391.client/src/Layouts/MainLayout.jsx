import Footer from "../Component/Footer/Footer";
import NavBar2 from "../Component/NavBar/NavBar2";
import '../Layouts/MainLayout.css';
import { Tooltip } from 'react-tooltip';
import 'react-tooltip/dist/react-tooltip.css';
import { useState } from 'react';
import SelectModal from '../Component/Modals/SelectModal';
import CheckAuth from '../Helpers/CheckAuth';
function MainLayout({ children }) {
    const [basicModal, setBasicModal] = useState(false);

    const toggleOpen = () => setBasicModal(!basicModal);
    const getProfile = (token) => {
        console.log(`https://www.googleapis.com/oauth2/v1/userinfo?access_token=${token}`);
        fetch(`https://www.googleapis.com/oauth2/v1/userinfo?access_token=${token}`, {
            headers: {
                Authorization: `Bearer ${token}`,
                Accept: 'application/json'
            }
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

            <NavBar2 />

            <main>
                {children}
            </main>
            <Footer></Footer>
        </div>
    );
}

export default MainLayout;