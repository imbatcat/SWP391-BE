import { Link } from 'react-router-dom';

function UserSidebar() {
    return (
        <div className="sidebar d-flex flex-column p-3 bg-light">
            <h2>Dashboard</h2>
            <ul className="list-group">
                <li className='list-group-item'>
                    <Link to="/user/petList" className="nav-link">My pets</Link>
                </li>
            </ul>
        </div>
    );
}

export default UserSidebar;