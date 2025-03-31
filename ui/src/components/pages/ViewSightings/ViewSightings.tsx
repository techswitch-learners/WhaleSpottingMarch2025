import { useEffect, useState } from "react";
import Calendar from "react-calendar";
import "react-calendar/dist/Calendar.css";

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

  useEffect(() => {
    const getSightings = async () => {
      const data = await fetchSightings();
      setSightingsData(data);
    };

    getSightings();
  }, []);

  return (
    <div id="SightingsListContainer">
      <h1> Sightings </h1>
      <div id="SightingsTable">
        <div id="FilterContainer">
          <label htmlFor="Start Date"> Start Date </label>
          <Calendar maxDate={new Date()}></Calendar>
          <label htmlFor="End Date"> End Date </label>
          <Calendar maxDate={new Date()}></Calendar>
          <label htmlFor="Coordinates"> Coordinates </label>
          <input id="coordinates" />
          <label htmlFor="Radius"> Radius </label>
          <input id="radius" />
          <label htmlFor="HasImage"> Has Image </label>
          <input id="hasImage" type="checkbox" />
          <button id="btnFilterSubmit" type="submit">
            Filter
          </button>
        </div>
        <table>
          <thead>
            <tr>
              <th>Species</th>
              <th>Date</th>
              <th>Username</th>
              <th>Image</th>
              <th>View</th>
            </tr>
          </thead>
          <tbody>
            {sightingsData?.map((sighting) => (
              <tr>
                <td>{sighting.species.speciesName}</td>
                <td>{sighting.reportDate}</td>
                <td>{sighting.description}</td>
                <td>{sighting.imageSource}</td>
                <td>
                  <button>View</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};
