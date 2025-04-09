import { useEffect, useState } from "react";
import "./Landing.scss";
import { SightingsResponse } from "../../../models/apiModels.ts";
import { getSightings } from "../../../utils/apiClient.tsx";
import { TABLET_MIN_WIDTH } from "../../../utils/constants.tsx";
import { MapComponentWorking } from "../../mapComponent/MapComponentWorking.tsx";
// import { MapComponentNotWorking } from "../../mapComponent/NotWorking.tsx";

const fetchSightings = async () => {
  try {
    return getSightings();
  } catch (error) {
    console.error("There was a problem with the fetch operation:", error);
  }
};

export const Landing = () => {
  const [sightingsData, setSightingsData] = useState<SightingsResponse[]>();
  const [isMobile, setIsMobile] = useState(
    window.innerWidth <= TABLET_MIN_WIDTH,
  );

  useEffect(() => {
    const handleResize = () => {
      setIsMobile(window.innerWidth <= TABLET_MIN_WIDTH);
    };
    window.addEventListener("resize", handleResize);
    return () => window.removeEventListener("resize", handleResize);
  }, []);

  useEffect(() => {
    const getSightings = async () => {
      const data = await fetchSightings();
      setSightingsData(data);
    };
    getSightings();
  }, []);

  const renderLandingMobileView = () => {
    return (
      <>
        <MapComponentWorking />
        <h2>Most recent sightings</h2>
        <ul id="Sightings" className="ul-container">
          {sightingsData?.map((sighting) => (
            <li className="li-item">
              <img src={sighting.imageSource} />
              <div id="sightings-info">
                <div>
                  <strong> Species: </strong> {sighting.species.speciesName}
                </div>
                <div>
                  <strong> Date Reported: </strong>
                  {sighting.reportDate.slice(0, 10)}
                </div>
                <div>
                  <strong>Description: </strong> {sighting.description}
                </div>
                <div>
                  <strong>Latitude: </strong>
                  {sighting.location.latitude}
                </div>
                <div>
                  <strong>Longitude: </strong>
                  {sighting.location.longitude}
                </div>
              </div>
            </li>
          ))}
        </ul>
      </>
    );
  };
  const renderLandingDesktopView = () => {
    return (
      <>
        <MapComponentWorking />
        <table id="SightingsTable" className="table-container">
          <thead className="table-header">
            {/* <th className="table-cell">Id</th> */}
            <th className="table-cell">Species</th>
            <th className="table-cell">Date</th>
            <th className="table-cell">Description</th>
            {/* <th className="table-cell">Location</th> */}
            <th className="table-cell">Image</th> 
          </thead>

          <tbody>
            {sightingsData?.map((sighting) => (
              <tr className="table-row">
                {/* <td className="table-cell">{sighting.id}</td> */}
                <td className="table-cell">{sighting.species.speciesName}</td>
                <td className="table-cell">
                  {sighting.reportDate.slice(0, 10)}
                </td>
                <td className="table-cell">{sighting.description}</td>
                {/* <td className="table-cell">
                Latitude: {sighting.location.latitude} <br />
                Longitude: {sighting.location.longitude}
              </td> */}
                <td className="table-cell">
                <img src={sighting.imageSource} />
              </td>
              </tr>
            ))}
          </tbody>
        </table>
      </>
    );
  };

  return (
    <div id="SightingsListContainer">
      <h1>Welcome to Whale Spotting</h1>
      {isMobile && renderLandingMobileView()}
      {!isMobile && renderLandingDesktopView()}
    </div>
  );
};
