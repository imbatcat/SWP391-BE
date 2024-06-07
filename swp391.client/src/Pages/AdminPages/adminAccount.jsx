import { useState, useEffect } from 'react';
import { MDBBadge, MDBBtn, MDBTable, MDBTableBody, MDBTableHead, MDBModal, MDBModalBody, MDBModalHeader, MDBModalFooter, MDBInput } from 'mdb-react-ui-kit';
import SideNav from '../../Component/SideNav/SideNav';
import { Accounts } from './AccountData';

function AdminAccount() {
  const [accounts, setAccounts] = useState([]);
  const [selectedAccount, setSelectedAccount] = useState(null);
  const [isModalOpen, setIsModalOpen] = useState(false);

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

  const handleEditClick = (account) => {
    setSelectedAccount(account);
    setIsModalOpen(true);
  };

  const toggleModal = () => {
    setIsModalOpen(!isModalOpen);
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
      const response = await fetch(`https://localhost:7206/api/Accounts/`, {
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
      toggleModal();
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
          {Accounts.filter(Acc => Acc.roleId === 1).map((Acc) => (
            <tr key={Acc.id}>
              <td>
                <div className='d-flex align-items-center'>
                  <img
                    src='https://mdbootstrap.com/img/new/avatars/8.jpg'
                    alt=''
                    style={{ width: '45px', height: '45px' }}
                    className='rounded-circle'
                  />
                  <div className='ms-3'>
                    <p className='fw-bold mb-1'>{Acc.fullName}</p>
                    <p className='text-muted mb-0'>{Acc.username}</p>
                  </div>
                </div>
              </td>
              <td>
                <p className='fw-normal mb-1'>{Acc.email}</p>
              </td>
              <td>
                <p className='fw-normal mb-1'>{Acc.phoneNumber}</p>
              </td>
              <td>
                <p className='fw-normal mb-1'>{Acc.isMale ? "Male" : "Female"}</p>
              </td>
              <td>
                <MDBBadge color={Acc.isDisabled ? 'danger' : 'success'} pill>
                  {Acc.isDisabled ? "Disabled" : "Active"}
                </MDBBadge>
              </td>
              <td>User</td>
              <td>
                <MDBBtn color='link' rounded size='sm' onClick={() => handleEditClick(Acc)}>
                  Edit
                </MDBBtn>
              </td>
            </tr>
          ))}
        </MDBTableBody>
      </MDBTable>

      {selectedAccount && (
        <MDBModal isOpen={isModalOpen} toggle={toggleModal}>
          <MDBModalHeader toggle={toggleModal}>Edit Account</MDBModalHeader>
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
            <MDBBtn color='secondary' onClick={toggleModal}>Close</MDBBtn>
            <MDBBtn color='primary' onClick={handleSaveChanges}>Save changes</MDBBtn>
          </MDBModalFooter>
        </MDBModal>
      )}
    </div>
  );
}

export default AdminAccount;
