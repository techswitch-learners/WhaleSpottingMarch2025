import { useEffect, useState } from "react";
import "./ViewSightings.scss";

const MEDIUM_DEVICE_SIZE = 480;

type SightingsResponse = {
  id: number;
  species: Species;
  description: string;
  sightingDate: string;
  reportDate: string;
  quantity: number;
  location: Location;
  imageSource: string;
  //username: string;
};

type Species = {
  id: number;
  speciesName: string;
};

type Location = {
  id: number;
  latitude: number;
  longitude: number;
};

const fetchSightings = async () => {
  const headers = {
    "Content-Type": "application/json",
    "Access-Control-Allow-Origin": "*",
    "Access-Control-Allow-Methods": "GET, POST, PATCH, OPTIONS",
  };

  try {
    const response = await fetch(
      import.meta.env.VITE_APP_API_HOST + "/Sighting",
      {
        method: "GET",
        headers: headers,
      },
    );
    if (!response.ok) {
      throw new Error("Network response was not ok");
    }
    const data = await response.json();
    return data;
  } catch (error) {
    console.error("There was a problem with the fetch operation:", error);
  }
};

export const ViewSightings = () => {
  const [sightingsData, setSightingsData] = useState<SightingsResponse[]>();

  const [isMobile, setIsMobile] = useState(
    window.innerWidth <= MEDIUM_DEVICE_SIZE,
  );

  useEffect(() => {
    const handleResize = () => {
      setIsMobile(window.innerWidth <= MEDIUM_DEVICE_SIZE);
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

  const renderMobileView = () => {
    return (
      <ul id="Sightings" className="ul-container">
        {sightingsData?.map((sighting) => (
          <li className="li-item">
            <img src={sighting.imageSource} />
            <p id="species-name">Species: {sighting.species.speciesName}</p>
            <p id="date-reported">
              Date Reported: {sighting.reportDate.slice(0, 10)}
            </p>
            <p id="description">Description: {sighting.description}</p>
            <p className="coordinates">
              Lat: {sighting.location.latitude}
              ;Long: {sighting.location.longitude}
            </p>
          </li>
        ))}
      </ul>
    );
  };
  const renderDesktopView = () => {
    return (
      <table id="SightingsTable" className="table-container">
        <thead className="table-header">
          <th className="table-cell">Id</th>
          <th className="table-cell">Species</th>
          <th className="table-cell">Date</th>
          <th className="table-cell">Description</th>
          <th className="table-cell">Location</th>
          <th className="table-cell">Image</th>
        </thead>

        <tbody>
          {sightingsData?.map((sighting) => (
            <tr className="table-row">
              <td className="table-cell hide-on-mobile">{sighting.id}</td>
              <td className="table-cell">{sighting.species.speciesName}</td>
              <td className="table-cell">{sighting.reportDate.slice(0, 10)}</td>
              <td className="table-cell">{sighting.description}</td>
              <td className="table-cell hide-on-mobile">
                Lat: {sighting.location.latitude}
                Long: {sighting.location.longitude}
              </td>
              <td className="table-cell">
                <img src={sighting.imageSource} />
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    );
  };

  return (
    <div id="SightingsListContainer">
      <h1> Sightings </h1>
      {isMobile && renderMobileView()}
      {!isMobile && renderDesktopView()}
    </div>
  );
};
