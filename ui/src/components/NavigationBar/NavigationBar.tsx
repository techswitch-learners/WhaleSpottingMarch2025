import { NavLink } from "react-router";
import './NavigationBar.scss'
import { useEffect, useState } from "react";

export const NavigationBar = () => {
  const [isMobile, setIsMobile] = useState(window.innerWidth <= 760);
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  useEffect(() => {
    const handleResize = () => {setIsMobile(window.innerWidth <= 760)};
    window.addEventListener('resize', handleResize);

    return () => window.removeEventListener('resize', handleResize);
  }, []);

  const mobileMenuDisplay = () => {
    setIsMenuOpen(!isMenuOpen)
  }

  const links = [
    { name: "Home", link: "/" },
    { name: "View Sightings", link: "/ViewSightings" },
    { name: "Report Sighting", link: "/ReportSighting" },
    { name: "View Locations", link: "/ViewLocations" },
    { name: "Admin", link: "/Admin" },
    { name: "Log In", link: "/LogIn" },
  ];

  const mainMenuLinks = isMobile ? [links[2], links[5]] : links;
  const mobileMenuLinks = links.filter((_, index) => ![2, 5].includes(index));

  const renderMobileMenu = () => (
    <div className="navigation-bar__mobile-menu">
      {mobileMenuLinks.map(link => (
        <NavLink key={link.name} to={link.link}>
          {link.name}
        </NavLink>
      ))}
    </div>
  );

  const renderBurger = () => (
    <div className="navigation-bar__menu-button">
      <button onClick={mobileMenuDisplay}>
        ☰
      </button>
    </div>
  );

  return (
    <div className="navigation-bar">
      {isMenuOpen && renderMobileMenu()}
      <div className="navigation-bar__content">
        {isMobile && renderBurger()}
        {mainMenuLinks.map((link) => (
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
