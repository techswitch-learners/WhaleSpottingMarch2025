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

  const links = [
    { name: "Home", link: "/" },
    { name: "View Sightings", link: "/ViewSightings" },
    { name: "Report Sighting", link: "/ReportSighting" },
    { name: "View Locations", link: "/ViewLocations" },
    { name: "Admin", link: "/Admin" },
    { name: "Log In", link: "/LogIn" },
    { name: "Register", link: "/Register" },
    { name: "Log Out", link: "/logout" },
  ];

  const getMainMenuLinks = (): { name: string; link: string }[] => {
    let updatedLinks: { name: string; link: string }[] = [];
    if (!loginContext.isLoggedIn) {
      updatedLinks = [links[0], links[1], links[3], links[5], links[6]];
    }
    if (loginContext.isLoggedIn && loginContext.isAdmin) {
      updatedLinks = [
        links[0],
        links[1],
        links[2],
        links[3],
        links[4],
        links[6],
        links[7],
      ];
    }
    if (loginContext.isLoggedIn && !loginContext.isAdmin) {
      updatedLinks = [links[0], links[1], links[2], links[3], links[7]];
    }

    if (isMobile) {
      if (!isMenuOpen) {
        if (loginContext.isLoggedIn) {
          updatedLinks = [links[2], links[7]];
        } else {
          updatedLinks = [links[2], links[5]];
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
