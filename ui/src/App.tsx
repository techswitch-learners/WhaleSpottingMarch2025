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

function App() {
  return (
    <>
      <div>
        <a href="https://vite.dev" target="_blank">
          <img src={viteLogo} className="logo" alt="Vite logo" />
        </a>
        <a href="https://react.dev" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Vite + React</h1>
      <div className="card">
        <CustomButton onClick={() => setCount((count) => count + 1)}>
          count is {count}
        </CustomButton>
        <p>
          Edit <code>src/App.tsx</code> and save to test HMR
        </p>
      </div>
      <p className="read-the-docs">
        Click on the Vite and React logos to learn more
      </p>
      <div>
        <WhaleSightingForm></WhaleSightingForm>
      </div>
      <NavigationBar />
      <div className="content">
        <Routes>
          <Route path="/" element={<Landing />} />
          <Route path="/ReportSighting" element={<ReportSighting />} />
          <Route path="/ViewSightings" element={<ViewSightings />} />
          <Route path="/Admin" element={<Admin />} />
          <Route path="/ViewLocations" element={<ViewLocations />} />
          <Route path="/LogIn" element={<LogIn />} />
        </Routes>
      </div>
    </>
  );
}

export default App;
