import { createContext, JSX, ReactNode, useState } from "react";

export const LoginContext = createContext({
  isLoggedIn: false,
  isAdmin: false,
  logIn: () => {},
  logOut: () => {},
});

interface LoginManagerProps {
  children: ReactNode;
}

export function LoginManager(props: LoginManagerProps): JSX.Element {
  const [loggedIn, setLoggedIn] = useState(false);

  function logIn() {
    setLoggedIn(true);
    getCookieValue("UserRole");
  }

  function getCookieValue(name: string) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    // if (parts.length === 2) return parts.pop().split(";").shift();
    // return null;
  }

  function logOut() {
    setLoggedIn(false);
  }

  const context = {
    isLoggedIn: loggedIn,
    isAdmin: loggedIn,
    logIn: logIn,
    logOut: logOut,
  };

  return (
    <LoginContext.Provider value={context}>
      {props.children}
    </LoginContext.Provider>
  );
}
