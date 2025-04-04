import { useContext } from "react";
import { LoginContext } from "../../LoginManager/LoginManager";

export const Admin = () => {
  const loginContext = useContext(LoginContext);
  if (!loginContext.isLoggedIn && !loginContext.isAdmin) {
    return (
      <div>
        <h3> Only admin can view this page. Please log in as admin </h3>
        <h3>
          <a href="/LogIn"> Login </a>
        </h3>
      </div>
    );
  }
};
