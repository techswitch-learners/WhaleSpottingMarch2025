import { useEffect, useState } from "react";
import "./ViewSightings.scss";
import {
  FilterSigtings,
  SightingsResponse,
} from "../../../models/apiModels.ts";
import {
  getFilteredSightings,
  getSightings,
} from "../../../utils/apiClient.tsx";
import { TABLET_MIN_WIDTH } from "../../../utils/constants.tsx";

const fetchSightings = async () => {
  try {
    return getSightings();
  } catch (error) {
    console.error("There was a problem with the fetch operation:", error);
  }
};

export const ViewSightings = () => {
  const [sightingsData, setSightingsData] = useState<SightingsResponse[]>();
  const [errorMessage, setErrorMessage] = useState("");
  const [filterData, setFilterData] = useState<FilterSigtings>({
    PageNumber: null,
    PageSize: null,
    SpeciesId: null,
    HasImage: null,
    SightingStartDate: null,
    SightingEndDate: null,
  });

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

  const handleChange = (
    event:
      | React.ChangeEvent<HTMLInputElement>
      | React.ChangeEvent<HTMLSelectElement>,
  ) => {
    const { name, value } = event.target;
    setFilterData({ ...filterData, [name]: value });
  };

  const handleSubmit = async (
    event: React.FormEvent<HTMLFormElement>,
  ): Promise<void> => {
    event.preventDefault();

    try {
      const response = await getFilteredSightings(filterData);
      setSightingsData(response);
    } catch (error) {
      console.log(error);
      setErrorMessage("An error has occurred." + error);
    }
  };

  const renderFilterMenu = () => {
    return (
      <div id="FilterContainer">
        <form name="filterSightings" onSubmit={handleSubmit}>
          <label htmlFor="Start Date"> Date </label>
          <input
            type="date"
            name="SightingStartDate"
            onChange={handleChange}
            max={new Date().toISOString().split("T")[0]}
          ></input>
          <label htmlFor="End Date"> End Date </label>
          <input
            type="date"
            name="SightingEndDate"
            onChange={handleChange}
            max={new Date().toISOString().split("T")[0]}
          ></input>
          {/* <label htmlFor="Coordinates"> Coordinates </label>
          <input id="coordinates" />
          <label htmlFor="Radius"> Radius </label>
          <input id="radius" />  */}
          <label htmlFor="Species"> Species </label>
          <select name="SpeciesId" onChange={handleChange}>
            <option value="">Please select a species</option>
            <option value="1">Blue Whale</option>
            <option value="2">Humpback Whale</option>
            <option value="3">Sperm Whale</option>
            <option value="4">Orca</option>
            <option value="5">Fin Whale</option>
            <option value="6">Minke Whale</option>
            <option value="7">Beluga Whale</option>
            <option value="8">Gray Whale</option>
            <option value="9">Right Whale</option>
            <option value="10">Bowhead Whale</option>
            <option value="11">Unknown</option>
          </select>
          <label htmlFor="HasImage"> Has Image </label>
          <input
            id="hasImage"
            type="checkbox"
            name="HasImage"
            onChange={handleChange}
          />
          <button id="btnFilterSubmit" type="submit">
            Filter
          </button>
        </form>
      </div>
    );
  };
  const renderMobileView = () => {
    return (
      <ul id="Sightings" className="ul-container">
        {sightingsData?.map((sighting) => (
          <li className="li-item">
            <img src={sighting.imageSource} />
            <div id="sightings-info">
              <div>
                <strong> Species: </strong> {sighting.speciesName}
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
                {sighting.latitude}
              </div>
              <div>
                <strong>Longitude: </strong>
                {sighting.longitude}
              </div>
            </div>
          </li>
        ))}
      </ul>
    );
  };
  const renderDesktopView = () => {
    return (
      <div className="sightings-container">
        {renderFilterMenu()};
        <table id="SightingsTable" className="table-container">
          <thead className="table-header">
            <tr>
              <th className="table-cell">Id</th>
              <th className="table-cell">Species</th>
              <th className="table-cell">Date</th>
              <th className="table-cell">Description</th>
              <th className="table-cell">Location</th>
              <th className="table-cell">Image</th>
            </tr>
          </thead>

        <tbody>
          {sightingsData?.map((sighting) => (
            <tr className="table-row">
              <td className="table-cell">{sighting.id}</td>
              <td className="table-cell">{sighting.speciesName}</td>
              <td className="table-cell">{sighting.reportDate.slice(0, 10)}</td>
              <td className="table-cell">{sighting.description}</td>
              <td className="table-cell">
                Latitude: {sighting.latitude} <br />
                Longitude: {sighting.longitude}
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
      {errorMessage.length > 0 && (
        <p className="errorMessage">{errorMessage}</p>
      )}
      {isMobile && renderMobileView()}
      {!isMobile && renderDesktopView()}
      <div id="pagination-links">
        <a href="#" className="previous">
          {" "}
          &laquo; Previous{" "}
        </a>
        <a href="#" className="next">
          {" "}
          Next &raquo;{" "}
        </a>
      </div>
    </div>
  );
};
