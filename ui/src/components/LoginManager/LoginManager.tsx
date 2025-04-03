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
  const [admin, setAdmin] = useState(false);

  function logIn() {
    setLoggedIn(true);
    const role = getCookie("UserRole");
    if (role == "Admin") {
      console.log(role);
      setAdmin(true);
    }
    console.log(role);
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
