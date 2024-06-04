import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import 'mdb-react-ui-kit/dist/css/mdb.min.css';
import "@fortawesome/fontawesome-free/css/all.min.css";
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import Login from './Pages/Login/Login';
import App from './App';
import Home from './Pages/Home/Home'
import SignUp from './Pages/SignUp/SignUp';
import AboutUs from './Pages/About Us/AboutUs';
import Appointment from './Pages/Appointment/Appointment';
import OTPInput from './Pages/OTP Input/OTPInput';
import PasswordResetForm from './Pages/SetNewPass/PasswordResetForm';
import PetList from './Pages/My Pet List/PetList';
import ConfirmEmail from './Pages/ConfirmEmail';
import GoogleLogin from './Pages/GoogleLogin';
const router = createBrowserRouter([
  {
    path: '/',
    element: <Home/>,
  errorElement: <div>404 Not Found</div>,
  },
  {
    path: '/login',
    element: <Login/> ,
    errorElement: <div>404 Not Found</div>,
  },
  {
    path: '/signUp',
    element: <SignUp/>,
    errorElement: <div>404 Not Found</div>,
  },
  {
    path: '/aboutUs',
    element: <AboutUs/>,
    errorElement: <div>404 Not Found</div>,
  },
  {
    path: '/appointment',
    element: <Appointment/>,
    errorElement: <div>404 Not Found</div>,
  },
  {
    path: '/otp',
    element: <OTPInput/>,
    errorElement: <div>404 Not Found</div>,
  },
  {
    path: '/setnewpw',
    element: <PasswordResetForm/>,
    errorElement: <div>404 Not Found</div>,
  },
  {
    path: '/petList',
    element: <PetList/>,
    errorElement: <div>404 Not Found</div>,
    },
    {
        path: '/account-confirm',
        element: <ConfirmEmail />,
        errorElement: <div>404 Not Found</div>,
    },
    {
        path: '/google',
        element: <GoogleLogin />,
        errorElement: <div>404 Not Found</div>,
    },
])
const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <RouterProvider router={router}/>
    {/*<App></App>*/}
  </React.StrictMode>,
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
