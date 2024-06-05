import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';

const PasswordResetForm = () => {
    const navigate = useNavigate();
    const query = useQuery();
    const [confirmationResult, setConfirmationResult] = useState(null);

    const [password1, setPassword1] = useState('');
    const [password2, setPassword2] = useState('');
    const [message, setMessage] = useState('');
    const [isButtonDisabled, setIsButtonDisabled] = useState(true);
    const [pwdLength1, setPwdLength1] = useState('');
    const [pwdLength2, setPwdLength2] = useState('');

    useEffect(() => {
        const userId = query.get('userId');
        const token = query.get('token');

        if (userId && token) {
            resetPassword(userId, token, password1);
        }
    }, [query]);

    const resetPassword = async (userId, token, password1) => {
        try {
            const response = await fetch(`https://localhost:7206/api/ApplicationAuth/reset-password`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                credentials: 'include',
                body: JSON.stringify(
                    {
                        "UserId": userId,
                        "Token": token,
                        "NewPassword": password1
                    }
                ),
            });
            if (response.ok) {
                navigate('/');
            }
        } catch (error) {
            console.error('Error resetting password:', error);
        }
 
    };


  useEffect(() => {
    const validate = () => {
      let pwdLength1 = '';
      let pwdLength2 = '';

      if (password1.length < 6) {
        pwdLength1 = 'Minimum 6 characters';
      }

      if (password2.length < 6) {
        pwdLength2 = 'Minimum 6 characters';
      }

      setPwdLength1(pwdLength1);
      setPwdLength2(pwdLength2);

      if (password1.length >= 6 && password2.length >= 6) {
        if (password1 === password2) {
          setIsButtonDisabled(false);
          setMessage('Password Matched');
        } else {
          setIsButtonDisabled(true);
          setMessage('Password not matching');
        }
      } else {
        setIsButtonDisabled(true);
        setMessage('');
      }
    };

    validate();
  }, [password1, password2]);

  return (
    <div className="container-fluid bg-body-tertiary d-block">
      <div className="row justify-content-center">
        <div className="col-12 col-md-6 col-lg-4" style={{ minWidth: '500px' }}>
          <div className="card bg-white mb-5 mt-5 border-0" style={{ boxShadow: '0 12px 15px rgba(0, 0, 0, 0.02)' }}>
            <div className="card-body p-5 text-center">
              <form action="/users/updatePassword" method="PUT">
                <h4>Reset your Password</h4>
                <p>Your code was sent to you via email</p>

                <div className="input-container">
                  <input
                    className="input-field"
                    id="password-1"
                    type="password"
                    placeholder="Type your new password"
                    name="password"
                    value={password1}
                    onChange={(e) => setPassword1(e.target.value)}
                  />
                </div>
                <span id="pwd-length-1">{pwdLength1}</span>

                <div className="input-container">
                  <input
                    className="input-field"
                    id="password-2"
                    type="password"
                    placeholder="Re-type your new password"
                    name="confirmPassword"
                    value={password2}
                    onChange={(e) => setPassword2(e.target.value)}
                  />
                </div>
                <span id="pwd-length-2">{pwdLength2}</span>
                <span id="message" style={{ color: password1 === password2 ? 'green' : 'red' }}>{message}</span>
                <div>
                  <button className="btn" id="formSubmit" type="submit" disabled={isButtonDisabled} 
                    style={{ backgroundColor: isButtonDisabled ? 'grey' : 'pink' }}>Submit</button>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

//     <div style={{ maxWidth: '500px', margin: 'auto' }}>
//       <form action="/users/updatePassword" method="PUT">
//         <center>
//           <h2>
//             <span className="fa-passwd-reset fa-stack">
//               <i className="fa fa-undo fa-stack-2x"></i>
//               <i className="fa fa-lock fa-stack-1x"></i>
//             </span>
//             Reset your Password
//             <span className="fa-passwd-reset fa-stack">
//               <i className="fa fa-undo fa-stack-2x"></i>
//               <i className="fa fa-lock fa-stack-1x"></i>
//             </span>
//           </h2>
//         </center>
//         <div className="input-container">
//           <i className="fa fa-key icon"></i>
//           <input
//             className="input-field"
//             id="password-1"
//             type="password"
//             placeholder="Type your new password"
//             name="password"
//             value={password1}
//             onChange={(e) => {
//               setPassword1(e.target.value);
//               validate();
//             }}
//           />
//         </div>
//         <span id="pwd-length-1">{pwdLength1}</span>
//         <div className="input-container">
//           <i className="fa fa-key icon"></i>
//           <input
//             className="input-field"
//             id="password-2"
//             type="password"
//             placeholder="Re-type your new password"
//             name="confirmPassword"
//             value={password2}
//             onChange={(e) => {
//               setPassword2(e.target.value);
//               validate();
//             }}
//           />
//         </div>
//         <span id="pwd-length-2">{pwdLength2}</span>
//         <span id="message" style={{ color: password1 === password2 ? 'green' : 'red' }}>{message}</span>
//         <button className="btn" id="formSubmit" type="submit" disabled={isButtonDisabled} style={{ background: isButtonDisabled ? 'grey' : 'blue' }}>Register</button>
//       </form>
//     </div>
//   );
// };

export default PasswordResetForm;
