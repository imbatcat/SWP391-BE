import { useState, useEffect } from 'react';
import { MDBBadge, MDBBtn, MDBTable, MDBTableBody, MDBTableHead, MDBModal, MDBModalBody, MDBModalHeader, MDBModalFooter, MDBInput, MDBModalDialog, MDBModalContent, MDBModalTitle } from 'mdb-react-ui-kit';
import SideNav from '../../Component/SideNav/SideNav';

function VetAccount() {
  const [accounts, setAccounts] = useState([]);
  const [selectedAccount, setSelectedAccount] = useState(null);
  const [centredModal, setCentredModal] = useState(false);

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
        console.log(data);
      } catch (error) {
        console.error(error.message);
      }
    }

    fetchData();
  }, []);

  const toggleOpen = (account = null) => {
    setSelectedAccount(account);
    setCentredModal(!centredModal);
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setSelectedAccount(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

  const handleSaveChanges = async () => {
    try {
      const response = await fetch(`https://localhost:7206/api/Accounts/${selectedAccount.id}`, {
        method: 'PUT',
        credentials: 'include',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(selectedAccount),
      });

      if (!response.ok) {
        throw new Error('Error updating data');
      }

      const updatedAccount = await response.json();
      setAccounts(prevAccounts => prevAccounts.map(acc => (acc.id === updatedAccount.id ? updatedAccount : acc)));
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
            <th scope='col'>Role</th>
            <th scope='col'>Actions</th>
          </tr>
        </MDBTableHead>
        <MDBTableBody>
          {accounts.filter(acc => acc.roleId === 3).map((acc) => (
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
              </td>
              <td>
                <p className='fw-normal mb-1'>{acc.isMale ? "Male" : "Female"}</p>
              </td>
              <td>
                <MDBBadge color={acc.isDisabled ? 'danger' : 'success'} pill>
                  {acc.isDisabled ? "Disabled" : "Active"}
                </MDBBadge>
              </td>
              <td>Veterinary</td>
              <td>
                <MDBBtn color='link' rounded size='sm' onClick={() => toggleOpen(acc)}>
                  Edit
                </MDBBtn>
              </td>
            </tr>
          ))}
        </MDBTableBody>
      </MDBTable>

      {selectedAccount && (
        <MDBModal isOpen={centredModal} toggle={toggleOpen}>
          <MDBModalDialog centered>
            <MDBModalContent>
              <MDBModalHeader>
                <MDBModalTitle>Edit Account</MDBModalTitle>
                <MDBBtn className='btn-close' color='none' onClick={toggleOpen}></MDBBtn>
              </MDBModalHeader>
              <MDBModalBody>
                <MDBInput
                  label="Full Name"
                  name="fullName"
                  value={selectedAccount.fullName}
                  onChange={handleInputChange}
                />
                <MDBInput
                  label="Username"
                  name="username"
                  value={selectedAccount.username}
                  onChange={handleInputChange}
                />
                <MDBInput
                  label="Email"
                  name="email"
                  value={selectedAccount.email}
                  onChange={handleInputChange}
                />
                <MDBInput
                  label="Phone Number"
                  name="phoneNumber"
                  value={selectedAccount.phoneNumber}
                  onChange={handleInputChange}
                />
                <MDBInput
                  type="select"
                  label="Gender"
                  name="isMale"
                  value={selectedAccount.isMale ? "Male" : "Female"}
                  onChange={(e) => handleInputChange({ target: { name: "isMale", value: e.target.value === "Male" } })}
                >
                  <option value="Male">Male</option>
                  <option value="Female">Female</option>
                </MDBInput>
                <MDBInput
                  type="select"
                  label="Status"
                  name="isDisabled"
                  value={selectedAccount.isDisabled ? "Disabled" : "Active"}
                  onChange={(e) => handleInputChange({ target: { name: "isDisabled", value: e.target.value === "Disabled" } })}
                >
                  <option value="Active">Active</option>
                  <option value="Disabled">Disabled</option>
                </MDBInput>
              </MDBModalBody>
              <MDBModalFooter>
                <MDBBtn color='secondary' onClick={toggleOpen}>Close</MDBBtn>
                <MDBBtn color='primary' onClick={handleSaveChanges}>Save changes</MDBBtn>
              </MDBModalFooter>
            </MDBModalContent>
          </MDBModalDialog>
        </MDBModal>
      )}
    </div>
  );
}

export default VetAccount;
