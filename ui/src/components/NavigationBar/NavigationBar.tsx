import { Link } from "react-router";

export const NavigationBar = () => {
  const Links = [
    { name: "Home", link: "/" },
    { name: "Report Sighting", link: "/ReportSighting" },
    { name: "View Sightings", link: "/ViewSightings" },
    { name: "Admin", link: "/Admin" },
  ];

  return (
    <div>
      {Links.map((link) => (
        <Link key={link.name} to={link.link}>
          {link.name}
        </Link>
      ))}
    </div>
  );
};