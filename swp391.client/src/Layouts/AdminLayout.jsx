import {
    MDBModal,
} from 'mdb-react-ui-kit';
import Footer from "../Component/Footer/Footer";
import NavBar2 from "../Component/NavBar/NavBar2";
import '../Layouts/MainLayout.css';
import { Tooltip } from 'react-tooltip';
import 'react-tooltip/dist/react-tooltip.css';
import { useState } from 'react';
import SelectModal from '../Component/Modals/SelectModal';
import SelectAccountModal from '../Component/Modals/SelectAccountModal';
function AdminLayout({ children }) {
    const [basicModal, setBasicModal] = useState(false);

    const toggleOpen = () => setBasicModal(!basicModal);
    return (
        <div>
            <main>
                {children}
            </main>
            <div className="fixed-bottom-right">
                <button className="static-button" data-tooltip-id="add-button" onClick={toggleOpen}>+</button>
                <Tooltip id='add-button' content={'Create an account'}></Tooltip>

                <MDBModal open={basicModal} onClose={() => setBasicModal(false)} tabIndex='-1'>
                    <SelectAccountModal toggleOpen={toggleOpen}>
                    </SelectAccountModal>
                </MDBModal>
            </div>
        </div>
    );
}

export default AdminLayout;