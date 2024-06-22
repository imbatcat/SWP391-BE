import { useState, useEffect } from 'react';
import {
    MDBBadge, MDBBtn, MDBTable, MDBTableBody, MDBTableHead
}
    from 'mdb-react-ui-kit';
import SideNavForStaff from '../../Component/SideNavForStaff/SideNavForStaff';


function CageList() {
    const [cageList, setCageList] = useState([]);
    const [searchInput, setSearchInput] = useState('');
    const [isLoading, setIsLoading] = useState('');

    const handleSearchInputChange = () => {

    };
    useEffect(() => {
        async function fetchData() {
            try {
                const appResponse = await fetch('https://localhost:7206/api/Cages', {
                    method: 'GET',
                    credentials: 'include',
                });
                if (!appResponse.ok) {
                    throw new Error("Error fetching pet data");
                }
                const data = await appResponse.json();
                setCageList(data);

            } catch (error) {
                console.error(error.message);
            } finally {
                setIsLoading(false);
            }
        }

        fetchData();
    }, []);

    const getBadgeColor = (app) => {
        if (app.isCancel) {
            return 'danger'; // red
        }
        if (app.isCheckUp) {
            return 'success'; // green
        }
        if (app.isCheckIn) {
            return 'warning'; // yellow
        }
        return 'secondary'; // default color
    };

    if (isLoading) {
        return (<>Loading...</>);
    }

    return (
        <div>
            <SideNavForStaff searchInput={searchInput} handleSearchInputChange={handleSearchInputChange} />
            <MDBTable align='middle'>
                <MDBTableHead>
                    <tr style={{ textAlign: 'center' }}>
                        <th scope='col'>Id</th>
                        <th scope='col'>Pet Name</th>
                        <th scope='col'>Status</th>
                        <th scope='col'></th>
                        <th scope='col'>Actions</th>
                    </tr>
                </MDBTableHead>
                <MDBTableBody style={{ textAlign: 'center' }}>
                    {cageList.map((cage) => (
                        <tr key={cage.id}>
                            <td>
                                <div className='d-flex align-items-center'>
                                    <img
                                        src='https://mdbootstrap.com/img/new/avatars/8.jpg'
                                        alt=''
                                        style={{ width: '45px', height: '45px' }}
                                        className='rounded-circle'
                                    />
                                    <div className='ms-3'>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    ))
                    }
                </MDBTableBody>
            </MDBTable>

        </div>
    );
}

export default CageList;
