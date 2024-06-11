import React, { useEffect, useState } from 'react';
import './OTPInput.css'; // Ensure you have the appropriate styles imported
import { Link } from 'react-router-dom';

const OTPInput = () => {
  const [otpValues, setOtpValues] = useState(['', '', '', '', '', '']);
  const [isButtonDisabled, setIsButtonDisabled] = useState(true);

  useEffect(() => {
    const inputs = document.querySelectorAll(".otp-field > input");
    const button = document.querySelector(".btn");

    inputs[0].focus();
    button.setAttribute("disabled", "disabled");

    inputs[0].addEventListener("paste", function (event) {
      event.preventDefault();

      const pastedValue = (event.clipboardData || window.clipboardData).getData("text");
      const otpLength = inputs.length;

      for (let i = 0; i < otpLength; i++) {
        if (i < pastedValue.length) {
          inputs[i].value = pastedValue[i];
          inputs[i].removeAttribute("disabled");
          inputs[i].focus();
        } else {
          inputs[i].value = ""; // Clear any remaining inputs
          inputs[i].focus();
        }
      }
    });

    inputs.forEach((input, index1) => {
      input.addEventListener("keyup", (e) => {
        const currentInput = input;
        const nextInput = input.nextElementSibling;
        const prevInput = input.previousElementSibling;

        if (currentInput.value.length > 1) {
          currentInput.value = "";
          return;
        }

        if (nextInput && nextInput.hasAttribute("disabled") && currentInput.value !== "") {
          nextInput.removeAttribute("disabled");
          nextInput.focus();
        }

        if (e.key === "Backspace") {
          inputs.forEach((input, index2) => {
            if (index1 <= index2 && prevInput) {
              input.setAttribute("disabled", true);
              input.value = "";
              prevInput.focus();
            }
          });
        }

        button.classList.remove("active");
        button.setAttribute("disabled", "disabled");

        const inputsNo = inputs.length;
        if (!inputs[inputsNo - 1].disabled && inputs[inputsNo - 1].value !== "") {
          button.classList.add("active");
          button.removeAttribute("disabled");
          return;
        }
      });
    });
  }, []);

  return (
    <div className="container-fluid bg-body-tertiary d-block">
      <div className="row justify-content-center">
        <div className="col-12 col-md-6 col-lg-4" style={{ minWidth: '500px' }}>
          <div className="card bg-white mb-5 mt-5 border-0" style={{ boxShadow: '0 12px 15px rgba(0, 0, 0, 0.02)' }}>
            <div className="card-body p-5 text-center">
              <h4>Verify</h4>
              <p>Your code was sent to you via email</p>

              <div className="otp-field mb-4">
                <input type="number" />
                <input type="number" disabled />
                <input type="number" disabled />
                <input type="number" disabled />
                <input type="number" disabled />
                <input type="number" disabled />
              </div>

            <Link to='/setnewpw'>
              <button className="btn btn-primary mb-3" disabled={isButtonDisabled}>
                Verify
              </button>
            </Link>

              <p className="resend text-muted mb-0">
                Didn't receive code? <a href="">Request again</a>
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default OTPInput;
