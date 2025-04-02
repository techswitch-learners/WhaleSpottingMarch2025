import { FormEvent, useContext, useState } from "react";
import "./LogIn.scss";
import { LoginContext } from "../../LoginManager/LoginManager";
import { Login } from "../../../models/apiModels";
import { login } from "../../../utils/apiClient.tsx";
import { useNavigate } from "react-router-dom";

export const LogIn = () => {
  const loginContext = useContext(LoginContext);
  const [errorMessage, setErrorMessage] = useState("");
  const [loginData, setLoginData] = useState<Login>({
    username: "",
    password: "",
  });

  const navigate = useNavigate();

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    setLoginData({ ...loginData, [name]: value });
  };

  const tryLogin = async (event: FormEvent): Promise<void> => {
    event.preventDefault();
    try {
      const response = await login(loginData);
      if (response.status == 401) {
        setErrorMessage("Incorrect username and password.");
      }
      if (response.status == 200) {
        navigate("/");
      }
    } catch (error) {
      setErrorMessage("An error occurred during login." + error);
    }
    loginContext.logIn();
  };
  return (
    <div className="login-container">
      <h1 className="title">Log In</h1>

      {errorMessage.length > 0 && (
        <p className="errorMessage">{errorMessage}</p>
      )}

      <form className="login-form" onSubmit={tryLogin}>
        <div className="username">
          <label htmlFor="username">
            Username
            <input type={"text"} name="username" onChange={handleChange} required />
          </label>
        </div>
        <div className="password">
          <label htmlFor="password">
            Password
            <input type={"password"} name="password" onChange={handleChange} required />
          </label>
        </div>
        <button className="submit-button" type="submit">
          Log In
        </button>
      </form>
    </div>
  );
};
