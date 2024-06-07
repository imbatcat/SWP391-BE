import { useState, useEffect } from 'react';
import { MDBBadge, MDBBtn, MDBTable, MDBTableBody, MDBTableHead } from 'mdb-react-ui-kit';
import SideNav from '../../Component/SideNav/SideNav';
import { Accounts } from './AccountData';

function AdminAccount() {
  const [accounts, setAccounts] = useState([]);

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
        setAccounts(data); // Ensure setAccounts is correctly referenced
        console.log(data);
      } catch (error) {
        console.error(error.message);
      }
    }

    fetchData();
  }, []);

  return (
    <div>
      <SideNav />
      <MDBTable align='middle'>
        <MDBTableHead>
          <tr>
            <th scope='col'>Name</th>
            <th scope='col'>Email</th>
            <th scope='col'>Phone Number</th>
            <th scope='col'>Status</th>
            <th scope='col'>Position</th>
            <th scope='col'>Actions</th>
          </tr>
        </MDBTableHead>
        <MDBTableBody>
          {Accounts.map((Acc) => (
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
                    <p className='fw-bold mb-1'>{Acc.fullname}</p>
                    <p className='text-muted mb-0'>{Acc.username}</p>
                  </div>
                </div>
              </td>
              <td>
                <p className='fw-normal mb-1'>{Acc.email}</p>
              </td>
              <td>
                <p className='fw-normal mb-1'>{Acc.phonennumber}</p>
              </td>
              <td>
                <MDBBadge color='success' pill>
                  {Acc.isMale ? "Male" : "Female"}
                </MDBBadge>
              </td>
              <td>{Acc.roleId === 1 ? "Admin" : "User"}</td>
              <td>
                <MDBBtn color='link' rounded size='sm'>
                  Edit
                </MDBBtn>
              </td>
            </tr>
          ))}
        </MDBTableBody>
      </MDBTable>
    </div>
  );
}

export default AdminAccount;
