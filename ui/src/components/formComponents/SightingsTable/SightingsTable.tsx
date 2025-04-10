import { useContext, useEffect, useState } from "react";
import "./SightingsTable.scss";
import { SightingsResponse } from "../../../models/apiModels.ts";
import {
  getFilteredSightings,
  getSightings,
} from "../../../utils/apiClient.tsx";
import { TABLET_MIN_WIDTH } from "../../../utils/constants.tsx";
import { FilterContext } from "../../formComponents/FilterContext/FilterContext.tsx";

const fetchSightings = async () => {
  try {
    return getSightings();
  } catch (error) {
    console.error("There was a problem with the fetch operation:", error);
  }
};

export const SightingsTable = () => {
  const [sightingsData, setSightingsData] = useState<SightingsResponse[]>();
  const [errorMessage, setErrorMessage] = useState("");
  const filterContext = useContext(FilterContext);

  const [isMobile, setIsMobile] = useState(
    window.innerWidth <= TABLET_MIN_WIDTH,
  );

  // Mobile resizing
  useEffect(() => {
    const handleResize = () => {
      setIsMobile(window.innerWidth <= TABLET_MIN_WIDTH);
    };
    window.addEventListener("resize", handleResize);
    return () => window.removeEventListener("resize", handleResize);
  }, []);

  //Initial getSightings
  useEffect(() => {
    const getSightings = async () => {
      const data = await fetchSightings();
      setSightingsData(data);
    };
    getSightings();
  }, []);

  // const submitFilterData = async (
  //   event: React.FormEvent<HTMLFormElement>,
  // ): Promise<void> => {
  //   event.preventDefault();
  //   console.log(filterData);
  //   try {
  //     filterData.PageNumber = 1;
  //     const response = await getFilteredSightings(filterData);
  //     setSightingsData(response);
  //   } catch (error) {
  //     setErrorMessage("An error has occurred." + error);
  //   }
  // };

  // API call to fetch when filter data changes
  useEffect(() => {
    const handleChangeInFilter = async () => {
      console.log("inside useEffect handleChangeInFilter");
      try {
        const response = await getFilteredSightings(filterContext.filterData);
        setSightingsData(response);
      } catch (error) {
        setErrorMessage("An error has occurred. " + error);
      }
    };
    handleChangeInFilter();
  }, [filterContext.filterData]);

  const handlePrevious = () => {
    filterContext.updateFilter({
      ...filterContext.filterData,
      PageNumber: Math.min(filterContext.filterData.PageNumber - 1, 1),
    });
  };

  const handleNext = () => {
    filterContext.updateFilter({
      ...filterContext.filterData,
      PageNumber: filterContext.filterData.PageNumber + 1,
    });
  };

  const renderMobileView = () => {
    return (
      <div className="sightings-container">
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
      </div>
    );
  };
  const renderDesktopView = () => {
    return (
      <div className="sightings-container">
        <table id="SightingsTable" className="table-container">
          <thead>
            <tr className="table-header">
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
                <td className="table-cell">
                  {sighting.reportDate.slice(0, 10)}
                </td>
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
      </div>
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
      <div className="pagination-links">
        <button
          onClick={handlePrevious}
          disabled={filterContext.filterData.PageNumber === 1}
        >
          &laquo; Previous
        </button>
        <button onClick={handleNext}>Next &raquo;</button>
      </div>
    </div>
  );
};
