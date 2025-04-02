import React, { useState } from "react";
import "./Registration.scss";
import { register } from "../../../utils/apiClient";
import { useNavigate } from "react-router-dom";
import { Registration } from "../../../models/apiModels";

export const Register = () => {
  const [formData, setFormData] = useState<Registration>({
    name: "",
    username: "",
    email: "",
    password: "",
  });

  const [errorMessage, setErrorMessage] = useState("");
  const navigate = useNavigate();

  const validatePassword = (password: string) => {
    let errMessage = "";
    if (password.length < 6) {
      errMessage = "Password must be 6 characters long.";
    }

    if (!password.match(/[a-z]/)) {
      errMessage += "Password must contain atleast 1 lowercase letter.";
    }

    if (!password.match(/[A-Z]/)) {
      errMessage += "Password must contain atleast 1 uppercase letter.";
    }

    if (!password.match(/[0-9]/)) {
      errMessage += "Password must have 1 number.";
    }

    if (!password.match(/[\W_]/)) {
      errMessage += "Password must have 1 non alphanumeric character.";
    }
    if (errMessage != "") {
      setErrorMessage(errMessage);
      return false;
    }
    return true;
  };
  const handleSubmit = async (
    event: React.FormEvent<HTMLFormElement>,
  ): Promise<void> => {
    event.preventDefault();

    try {
      if (validatePassword(formData.password)) {
        const response = await register(formData);
        if (response.status >= 300) {
          setErrorMessage("");
        } else {
          navigate("/LogIn");
        }
      }
    } catch {
      setErrorMessage("An error has occurred.");
    }
  };

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    setFormData({ ...formData, [name]: value });
  };

  return (
    <div className="registrationForm">
      <h2>Register your account to report whale sightings</h2>
      {errorMessage.length > 0 && (
        <p className="errorMessage">{errorMessage}</p>
      )}
      <form onSubmit={handleSubmit}>
        <div className="name">
          <label htmlFor="name"> Name: </label>
          <input name="name" onChange={handleChange} maxLength={50} required />
        </div>

        <div className="username">
          <label htmlFor="username"> Username: </label>
          <input
            name="username"
            onChange={handleChange}
            maxLength={50}
            required
          />
        </div>

        <div className="email">
          <label htmlFor="email"> Email: </label>
          <input name="email" type="email" onChange={handleChange} required />
        </div>

        <div className="password">
          <label htmlFor="password"> Password: </label>
          <input name="password" onChange={handleChange} required />
        </div>
        <button type="submit">Submit</button>
      </form>
    </div>
  );
};
