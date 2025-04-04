import { createContext, JSX, ReactNode, useEffect, useState } from "react";

export const LoginContext = createContext({
  isLoggedIn: false,
  isAdmin: false,
  logIn: () => {},
  logOut: () => {},
});

const getInitialisLoggedIn = () => {
  const isLoggedIn = sessionStorage.getItem("isLoggedIn");
  return isLoggedIn ? JSON.parse(isLoggedIn) : false;
};

const getInitialisAdmin = () => {
  const isAdmin = sessionStorage.getItem("isAdmin");
  return isAdmin ? JSON.parse(isAdmin) : false;
};

interface LoginManagerProps {
  children: ReactNode;
}

export function LoginManager(props: LoginManagerProps): JSX.Element {
  const [loggedIn, setLoggedIn] = useState(getInitialisLoggedIn());
  const [admin, setAdmin] = useState(getInitialisAdmin());

  useEffect(() => {
    sessionStorage.setItem("isLoggedIn", JSON.stringify(loggedIn));
  }, [loggedIn]);

  useEffect(() => {
    sessionStorage.setItem("isAdmin", JSON.stringify(admin));
  }, [admin]);

  function logIn() {
    setLoggedIn(true);
    const role = getCookie("UserRole");
    if (role == "Admin") {
      setAdmin(true);
    }
  }

  function getCookie(cname: string) {
    const name = cname + "=";
    const decodedCookie = decodeURIComponent(document.cookie);
    const cookieArray = decodedCookie.split(";");
    for (let i = 0; i < cookieArray.length; i++) {
      let c = cookieArray[i];
      while (c.charAt(0) == " ") {
        c = c.substring(1);
      }
      if (c.indexOf(name) == 0) {
        return c.substring(name.length, c.length);
      }
    }
    return "";
  }

  function logOut() {
    setLoggedIn(false);
    setAdmin(false);
  }

  const context = {
    isLoggedIn: loggedIn,
    isAdmin: admin,
    logIn: logIn,
    logOut: logOut,
  };

  return (
    <LoginContext.Provider value={context}>
      {props.children}
    </LoginContext.Provider>
  );
}
