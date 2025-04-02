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

  const handleSubmit = async (
    event: React.FormEvent<HTMLFormElement>,
  ): Promise<void> => {
    event.preventDefault();

    try {
      const response = await register(formData);
      if (response.status >= 300) {
        setErrorMessage(response.statusText);
      } else {
        navigate("/ViewSightings");
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
          <input name="email" onChange={handleChange} required />
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
