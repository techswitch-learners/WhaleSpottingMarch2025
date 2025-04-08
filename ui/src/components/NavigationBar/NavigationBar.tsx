import { NavLink } from "react-router";
import "./NavigationBar.scss";
import { useContext, useEffect, useRef, useState } from "react";
import { LoginContext } from "../LoginManager/LoginManager";

const MEDIUM_DEVICE_SIZE = 760;

export const NavigationBar = () => {
  const mobileMenuRef = useRef<HTMLDivElement>(null);
  const loginContext = useContext(LoginContext);
  const [isMobile, setIsMobile] = useState(
    window.innerWidth <= MEDIUM_DEVICE_SIZE,
  );

  const [isMenuOpen, setIsMenuOpen] = useState(false);

  useEffect(() => {
    const handleResize = () => {
      setIsMobile(window.innerWidth <= MEDIUM_DEVICE_SIZE);
    };
    window.addEventListener("resize", handleResize);

    return () => window.removeEventListener("resize", handleResize);
  }, []);

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (
        mobileMenuRef.current &&
        !mobileMenuRef.current.contains(event.target as Node)
      ) {
        setIsMenuOpen(false);
      }
    };

    if (isMenuOpen) {
      document.addEventListener("mousedown", handleClickOutside);
    }

    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, [isMenuOpen]);

  const handleMobileMenu = () => {
    setIsMenuOpen(!isMenuOpen);
  };

  const links = {
    home: { name: "Home", link: "/" },
    viewSightings: { name: "View Sightings", link: "/ViewSightings" },
    reportSighting: { name: "Report Sighting", link: "/ReportSighting" },
    viewLocation: { name: "View Locations", link: "/ViewLocations" },
    admin: { name: "Admin", link: "/Admin" },
    logIn: { name: "Log In", link: "/LogIn" },
    register: { name: "Register", link: "/Register" },
    logOut: { name: "Log Out", link: "/logout" },
  };

  const getMainMenuLinks = (): { name: string; link: string }[] => {
    let updatedLinks: { name: string; link: string }[] = [];
    if (!loginContext.isLoggedIn) {
      updatedLinks = [
        links.home,
        links.viewSightings,
        links.reportSighting,
        links.viewLocation,
        links.logIn,
        links.register,
      ];
    }
    if (loginContext.isLoggedIn && loginContext.isAdmin) {
      updatedLinks = [
        links.home,
        links.viewSightings,
        links.reportSighting,
        links.viewLocation,
        links.admin,
        links.register,
        links.logOut,
      ];
    }

    if (loginContext.isLoggedIn && !loginContext.isAdmin) {
      updatedLinks = [
        links.home,
        links.viewSightings,
        links.reportSighting,
        links.viewLocation,
        links.logOut,
      ];
    }

    if (isMobile) {
      if (!isMenuOpen) {
        if (loginContext.isLoggedIn) {
          updatedLinks = [links.reportSighting, links.logOut];
        } else {
          updatedLinks = [links.reportSighting, links.logIn];
        }
      }
    }
    return updatedLinks;
  };

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
    <div className="navigation-bar__mobile-menu" ref={mobileMenuRef}>
      {renderCloseButton()}
      {getMainMenuLinks().map((link) => (
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

  const renderMainMenu = () =>
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
    ));

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
