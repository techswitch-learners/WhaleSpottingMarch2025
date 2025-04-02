import { Routes } from "react-router";
import "./App.scss";
import { Route } from "react-router";
import { NavigationBar } from "./components/NavigationBar/NavigationBar";
import { Admin } from "./components/pages/Admin/Admin";
import { Landing } from "./components/pages/Landing/Landing";
import { ReportSighting } from "./components/pages/ReportSighting/ReportSighting";
import { ViewSightings } from "./components/pages/ViewSightings/ViewSightings";
import { ViewLocations } from "./components/pages/ViewLocations/ViewLocations";
import { LogIn } from "./components/pages/LogIn/LogIn";
import { Register } from "./components/pages/Registration/Registration";
import { LoginManager } from "./components/LoginManager/LoginManager";

function App() {
  return (
    <>
      <LoginManager>
        <NavigationBar />
        <div className="content">
          <Routes>
            <Route path="/" element={<Landing />} />
            <Route path="/ReportSighting" element={<ReportSighting />} />
            <Route path="/ViewSightings" element={<ViewSightings />} />
            <Route path="/Admin" element={<Admin />} />
            <Route path="/ViewLocations" element={<ViewLocations />} />
            <Route path="/LogIn" element={<LogIn />} />
            <Route path="/Register" element={<Register />} />
          </Routes>
        </div>
      </LoginManager>
    </>
  );
}

export default App;
