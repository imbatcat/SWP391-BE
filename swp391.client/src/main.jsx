import React from 'react';
import ReactDOM from 'react-dom/client';
import UserPets from './Pages/Profile/UserPets';
import UserAppointments from './Pages/Profile/UserAppointments';
import './index.css';
import 'mdb-react-ui-kit/dist/css/mdb.min.css';
import "@fortawesome/fontawesome-free/css/all.min.css";
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import { AuthProvider } from "./Context/AuthProvider";
import Login from './Pages/Login/Login';
/*import App from './App';*/
import Home from './Pages/Home/Home';
import SignUp from './Pages/SignUp/SignUp';
import Appointment from './Pages/Appointment/Appointment';
import OTPInput from './Pages/OTP Input/OTPInput';
import PasswordResetForm from './Pages/SetNewPass/PasswordResetForm';
import PetList from './Pages/MyPetList/PetList';
import ConfirmEmail from './Pages/ConfirmEmail';
import GglLogin from './Pages/GoogleLogin';
import { ToastContainer } from 'react-toastify';
import UserProfile from './Pages/Profile/UserProfile';
import { UserProvider } from './Context/UserContext';
import AppointmentManage from './Pages/AdminPages/AppointmentManage';
import UsersAccount from './Pages/AdminPages/UsersAccount';
import UserHistoricalAppointments from './Pages/Profile/UserHistoricalAppointments';
import CheckAuth from './Helpers/CheckAuth';
import AdminPet from './Pages/AdminPages/petManage';
import VetAccount from './Pages/AdminPages/VetAccount';
import AdminAccount from './Pages/AdminPages/AdminAccount';

const router = createBrowserRouter([
    {
        path: '/',
        element: <Home />,
        errorElement: <div>404 Not Found</div>,
    },
    {
        path: '/login',
        element: <Login />,
        errorElement: <div>404 Not Found</div>,
    },
    {
        path: '/signUp',
        element: <SignUp />,
        errorElement: <div>404 Not Found</div>,
    },
    {
        path: '/appointment',
        element: <Appointment />,
        errorElement: <div>404 Not Found</div>,
    },
    {
        path: '/otp',
        element: <OTPInput />,
        errorElement: <div>404 Not Found</div>,
    },
    {
        path: '/reset-password',
        element: <PasswordResetForm />,
        errorElement: <div>404 Not Found</div>,
    },
    {
        path: '/petList',
        element: <PetList />,
        errorElement: <div>404 Not Found</div>,
    },
    {
        path: '/account-confirm',
        element: <ConfirmEmail />,
        errorElement: <div>404 Not Found</div>,
    },
    {
        path: '/google',
        element: <GglLogin />,
        errorElement: <div>404 Not Found</div>,
    },
    {
        path: '/user/profile',
        element: (
            <CheckAuth>
                <UserProfile />
            </CheckAuth>
        ),
        errorElement: <div>404 Not Found</div>,
    },
    {
        path: '/user/pets',
        element: (
            <CheckAuth>
                <UserPets />
            </CheckAuth>
        ),
        errorElement: <div>404 Not Found</div>,
    },
    {
        path: '/user/appointments',
        element: (
            <CheckAuth>
                <UserAppointments />
            </CheckAuth>
        ),
        errorElement: <div>404 Not Found</div>,
    },
    {
        path: '/user/old-appointments',
        element: (
            <CheckAuth>
                <UserHistoricalAppointments />
            </CheckAuth>
        ),
        errorElement: <div>404 Not Found</div>,
    },
    {
        path: '/admin/vets',
        element: <VetAccount />,
        errorElement: <div>404 Not Found</div>,
    },
    {
        path: '/admin/appointments',
        element: <AppointmentManage />,
        errorElement: <div>404 Not Found</div>,
    },
    {
        path: '/admin/customers',
        element: <UsersAccount />,
        errorElement: <div>404 Not Found</div>,
    },
    {
        path: '/admin/pets',
        element: <AdminPet />,
        errorElement: <div>404 Not Found</div>,
    },
    {
        path: '/admin/admins',
        element: <AdminAccount />,
        errorElement: <div>404 Not Found</div>,
    },

]);
const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <AuthProvider>
        <UserProvider>
            <React.StrictMode>
                <RouterProvider router={router}>
                </RouterProvider>
                <ToastContainer></ToastContainer>
            </React.StrictMode>
        </UserProvider>
    </AuthProvider>

);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
