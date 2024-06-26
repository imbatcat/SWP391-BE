import { useState, useEffect } from 'react';
import {
    MDBBadge, MDBBtn, MDBTable, MDBTableBody, MDBTableHead, MDBModal,
    MDBModalBody, MDBModalHeader, MDBModalFooter, MDBInput, MDBModalDialog,
    MDBModalContent, MDBModalTitle, MDBCol, MDBRow, MDBCheckbox
}
    from 'mdb-react-ui-kit';
import SideNav from '../../Component/SideNav/SideNav';
import { toast } from 'react-toastify';
import AdminLayout from '../../Layouts/AdminLayout';


function UsersAccount() {
    const [accounts, setAccounts] = useState([]);
    const [selectedAccount, setSelectedAccount] = useState(null);
    const [basicModal, setBasicModal] = useState(false);
    const [basicModalNew, setBasicModalNew] = useState(false);
    const [searchInput, setSearchInput] = useState('');
    const [filteredAccounts, setFilteredAccounts] = useState([]);

    useEffect(() => {
        async function fetchData() {
            try {
                const response = await fetch('https://localhost:7206/api/Accounts', {
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
                setAccounts(data);
                setFilteredAccounts(data);
                console.log(data);
            } catch (error) {
                console.error(error.message);
            }
        }

        fetchData();
    }, []);

    const toggleOpen = (account = null) => {
        setSelectedAccount(account);
        setBasicModal(!basicModal);
    };
    const toggleOpenNew = () => setBasicModalNew(!basicModalNew);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setSelectedAccount(prevState => ({
            ...prevState,
            [name]: value
        }));
    };
    const handleSearchInputChange = (e) => {
        const value = e.target.value.toLowerCase();
        setSearchInput(value);
        if (value === '') {
            setFilteredAccounts(accounts);
        } else {
            setFilteredAccounts(filteredAccounts.filter(acc =>
                acc.fullName.toLowerCase().includes(value) ||
                acc.phoneNumber.toLowerCase().includes(value) ||
                acc.email.toLowerCase().includes(value) ||
                acc.username.toLowerCase().includes(value)
            ));
        }
    };

    const handleSaveChanges = async () => {
        //const requestBody = {
        //    "fullName": "string",
        //    "username": "string",
        //    "email": "user@example.com",
        //    "phoneNumber": "string",
        //    "isMale": true
        //};
        //console.log(requestBody);
        //try {
        //    const response = await fetch(`https://localhost:7206/api/Accounts/${selectedAccount.id}`, {
        //        method: 'PUT',
        //        credentials: 'include',
        //        headers: {
        //            'Content-Type': 'application/json',
        //        },
        //        body: JSON.stringify(requestBody),
        //    });

        //    if (!response.ok) {
        //        throw new Error('Error updating data');
        //    }

        //    const updatedAccount = await response.json();
        //    setFilteredAccounts(prevAccounts => prevAccounts.map(acc => (acc.id === updatedAccount.id ? updatedAccount : acc)));
        //    toast.info("Account updated");
        //    toggleOpen();
        //} catch (error) {
        //    console.error(error.message);
        //}
    };

    return (
        <AdminLayout>
            <div>
                <SideNav searchInput={searchInput} handleSearchInputChange={handleSearchInputChange} />
                <MDBTable align='middle'>
                    <MDBTableHead>
                        <tr>
                            <th scope='col'>Name</th>
                            <th scope='col'>Email</th>
                            <th scope='col'>Phone Number</th>
                            <th scope='col'>Date of Birth</th>
                            <th scope='col'>Gender</th>
                            <th scope='col'>Status</th>
                            <th scope='col'>Actions</th>
                        </tr>
                    </MDBTableHead>
                    <MDBTableBody>
                        {filteredAccounts.filter(acc => acc.roleId === 1).map((acc) => (
                            <tr key={acc.id}>
                                <td>
                                    <div className='d-flex align-items-center'>
                                        <img
                                            src='https://mdbootstrap.com/img/new/avatars/8.jpg'
                                            alt=''
                                            style={{ width: '45px', height: '45px' }}
                                            className='rounded-circle'
                                        />
                                        <div className='ms-3'>
                                            <p className='fw-bold mb-1'>{acc.fullName}</p>
                                            <p className='text-muted mb-0'>{acc.username}</p>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <p className='fw-normal mb-1'>{acc.email}</p>
                                </td>
                                <td>
                                    <p className='fw-normal mb-1'>{acc.phoneNumber}</p>
                                </td>                     <td>
                                    <p className='fw-normal mb-1'>{acc.dateOfBirth}</p>
                                </td>
                                <td>
                                    <p className='fw-normal mb-1'>{acc.isMale ? "Male" : "Female"}</p>
                                </td>
                                <td>
                                    <MDBBadge color={acc.isDisabled ? 'danger' : 'success'} pill>
                                        {acc.isDisabled ? "Disabled" : "Active"}
                                    </MDBBadge>
                                </td>
                                <td>
                                    <MDBBtn color='danger' style={{ color: 'black' }} rounded size='sm' onClick={() => toggleOpen(acc)}>
                                        X
                                    </MDBBtn>
                                </td>
                            </tr>
                        ))}
                    </MDBTableBody>
                </MDBTable>


                {/*{selectedAccount && (*/}
                {/*    <MDBModal open={basicModal} onClose={() => setBasicModal(false)} tabIndex='-1'>*/}
                {/*        <MDBModalDialog centered>*/}
                {/*            <MDBModalContent>*/}
                {/*                <MDBModalHeader>*/}
                {/*                    <MDBModalTitle>Edit Account</MDBModalTitle>*/}
                {/*                    <MDBBtn className='btn-close' color='none' onClick={toggleOpen}></MDBBtn>*/}
                {/*                </MDBModalHeader>*/}
                {/*                <MDBModalBody>*/}
                {/*                    <form>*/}
                {/*                        <MDBRow className='mb-4'>*/}
                {/*                            <MDBCol>*/}
                {/*                                <MDBInput*/}
                {/*                                    label="Full Name"*/}
                {/*                                    name="fullName"*/}
                {/*                                    value={selectedAccount.fullName}*/}
                {/*                                    onChange={handleInputChange}*/}
                {/*                                />*/}
                {/*                            </MDBCol>*/}
                {/*                            <MDBCol>*/}
                {/*                                <MDBInput*/}
                {/*                                    label="Username"*/}
                {/*                                    name="username"*/}
                {/*                                    value={selectedAccount.username}*/}
                {/*                                    onChange={handleInputChange}*/}
                {/*                                />*/}
                {/*                            </MDBCol>*/}
                {/*                        </MDBRow>*/}
                {/*                        <MDBRow className='mb-4'>*/}
                {/*                            <MDBCol>*/}
                {/*                                <MDBInput*/}
                {/*                                    label="Email"*/}
                {/*                                    name="email"*/}
                {/*                                    value={selectedAccount.email}*/}
                {/*                                    onChange={handleInputChange}*/}
                {/*                                />*/}
                {/*                            </MDBCol>*/}

                {/*                        </MDBRow>*/}

                {/*                        <MDBRow className='mb-4'>*/}
                {/*                            <MDBCol>*/}
                {/*                                <MDBInput*/}
                {/*                                    label="Phone Number"*/}
                {/*                                    name="phoneNumber"*/}
                {/*                                    value={selectedAccount.phoneNumber}*/}
                {/*                                    onChange={handleInputChange}*/}
                {/*                                />*/}
                {/*                            </MDBCol>*/}
                {/*                            <MDBCol>*/}
                {/*                                <MDBCheckbox label="Is male"></MDBCheckbox>*/}
                {/*                            </MDBCol>*/}

                {/*                        </MDBRow>*/}
                {/*                    </form>*/}
                {/*                </MDBModalBody>*/}
                {/*                <MDBModalFooter>*/}
                {/*                    <MDBBtn color='secondary' onClick={toggleOpen}>Close</MDBBtn>*/}
                {/*                    <MDBBtn color='success' style={{ color: 'black' }} onClick={handleSaveChanges}>Save changes</MDBBtn>*/}
                {/*                </MDBModalFooter>*/}
                {/*            </MDBModalContent>*/}
                {/*        </MDBModalDialog>*/}
                {/*    </MDBModal>*/}
                {/*)}*/}

            </div>

        </AdminLayout>
    );
}

export default UsersAccount;
