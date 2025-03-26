import { NavLink } from "react-router";
import './NavigationBar.scss'

export const NavigationBar = () => {
  const Links = [
    { name: "Home", link: "/" },
    { name: "View Sightings", link: "/ViewSightings" },
    { name: "Report Sighting", link: "/ReportSighting" },
    { name: "View Locations", link: "/ViewLocations" },
    { name: "Admin", link: "/Admin" },
    { name: "Log In", link: "/LogIn" },
  ];

  return (
    <div className="navigation-bar">
      <div className="navigation-bar__content">
        {Links.map((link) => (
          <NavLink
            key={link.name}
            to={link.link}
            className={({ isActive }) => `navigation-bar__item ${isActive ? "navigation-bar__item--active" : ""}`
          }>
            {link.name}
          </NavLink>
        ))}
      </div>
    </div>
  );
};
