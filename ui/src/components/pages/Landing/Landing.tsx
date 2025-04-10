import "./Landing.scss";
import { MapComponent } from "../../mapComponent/MapComponent.tsx";
import { Link } from "react-router";

export const Landing = () => {
  return (
    <div id="SightingsListContainer">
      <h1>Welcome to Whale Spotting</h1>
      <img src="https://live.staticflickr.com/7418/8919679424_775e8c3d1d_b.jpg" />
      <div className="landing-page-text">
        <p className="intro-text">
          Recently we have finally seen an increase in whale numbers across the
          globe due to conservation efforts, and we would love to keep the
          public public and scientific community engaged with these efforts. We
          have created a website to help encourage and track whale spotting.
          Inspired by the Washington Whale Museum who have been tracking whale
          sightings in the Salish Sea, we have create a new website that covers
          the whole world.
        </p>
      </div>
      <div>
        <ul className="list-of-links">
          <li>
            <Link to="/ViewSightings">View existing Whale Sightings</Link>
          </li>
          <li>
            <Link to="/ReportSighting">Report a Whale Sighting</Link>
          </li>
          <li>
            <Link to="/Register">
              Please register on our website to report a whale sighting
            </Link>
          </li>
        </ul>
        <p>
          <b>Explore our map:</b> click on the whale icons on the map to see
          what our whale watchers have spotted!
        </p>
      </div>
      <MapComponent />
    </div>
  );
};
