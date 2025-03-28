import { NavLink } from "react-router";
import "./NavigationBar.scss";
import { useEffect, useState } from "react";

const MEDIUM_DEVICE_SIZE = 760;

export const NavigationBar = () => {
  const [isMobile, setIsMobile] = useState(window.innerWidth <= MEDIUM_DEVICE_SIZE);
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  useEffect(() => {
    const handleResize = () => {
      setIsMobile(window.innerWidth <= MEDIUM_DEVICE_SIZE);
    };
    window.addEventListener("resize", handleResize);

    return () => window.removeEventListener("resize", handleResize);
  }, []);

  const handleMobileMenu = () => {
    setIsMenuOpen(!isMenuOpen);
  };

  const links = [
    { name: "Home", link: "/" },
    { name: "View Sightings", link: "/ViewSightings" },
    { name: "Report Sighting", link: "/ReportSighting" },
    { name: "View Locations", link: "/ViewLocations" },
    { name: "Admin", link: "/Admin" },
    { name: "Log In", link: "/LogIn" },
  ];

  const getMainMenuLinks = () =>
    isMobile ? (isMenuOpen ? [] : [links[2], links[5]]) : links;

  const renderCloseButton = () => (
    <button
      type="button"
      className="navigation-bar__close-btn"
      onClick={handleMobileMenu}
    >
      &#x2715;
    </button>
  );

  const renderMobileMenu = () => (
    <div className="navigation-bar__mobile-menu">
      {renderCloseButton()}
      {links.map((link) => (
        <NavLink
          key={link.name}
          to={link.link}
          onClick={handleMobileMenu}
          className="navigation-bar__mobile-menu-item"
        >
          {link.name}
        </NavLink>
      ))}
    </div>
  );

  const renderBurger = () => (
    <div className="navigation-bar__menu-button">
      <button type="button" onClick={handleMobileMenu}>
        ☰
      </button>
    </div>
  );

  const renderMainMenu = () => (
    getMainMenuLinks().map((link) => (
      <NavLink
        key={link.name}
        to={link.link}
        className={({ isActive }) =>
          `navigation-bar__item ${
            isActive ? "navigation-bar__item--active" : ""
          }`
        }
      >
        {link.name}
      </NavLink>
    ))
  );

  return (
    <div className="navigation-bar">
      {isMobile && isMenuOpen && renderMobileMenu()}
      <div className="navigation-bar__content">
        {isMobile && !isMenuOpen && renderBurger()}
        {renderMainMenu()}
      </div>
    </div>
  );
};
