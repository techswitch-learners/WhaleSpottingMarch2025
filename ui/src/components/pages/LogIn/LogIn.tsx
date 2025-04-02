import React, { FormEvent, useState } from "react";
import "./LogIn.scss";

export const LogIn = () => {
  //const loginContext = useContext(LoginContext)
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  function tryLogin(event: FormEvent) {
    event.preventDefault();
    //loginContext.logIn(username, ,password);
  }
  return (
    <div className="login-container">
      <h1 className="title">Log In</h1>
      <form className="login-form" onSubmit={tryLogin}>
        <div className="username">
          <label htmlFor="username">
            Username
            <input
              type={"text"}
              value={username}
              onChange={(event) => setUsername(event.target.value)}
            />
          </label>
        </div>
        <div className="password">
          <label htmlFor="password">
            Password
            <input
              type={"password"}
              value={password}
              onChange={(event) => setPassword(event.target.value)}
            />
          </label>
        </div>
        <button className="submit-button" type="submit">
          Log In
        </button>
      </form>
    </div>
  );
};
