import { useContext, useEffect } from "react";
import "./LogOut.scss";
import { LoginContext } from "../../LoginManager/LoginManager";
import { logout } from "../../../utils/apiClient.tsx";
import { useNavigate } from "react-router-dom";

export const LogOut = () => {
  const loginContext = useContext(LoginContext);
  const navigate = useNavigate();

  useEffect(() => {
    const tryLogout = async () => {
      try {
        const response = await logout();
        if (response.status == 200) {
          loginContext.logOut();
          navigate("/Login");
        }
      } catch (error) {
        console.log("An error occurred during logout." + error);
      }
    };
    tryLogout();
  });
  return <div></div>;
};
