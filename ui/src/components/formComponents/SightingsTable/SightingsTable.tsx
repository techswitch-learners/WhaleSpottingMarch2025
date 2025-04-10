import { useContext, useEffect, useState } from "react";
import "./SightingsTable.scss";
import { SightingsResponse } from "../../../models/apiModels.ts";
import { getFilteredSightings } from "../../../utils/apiClient.tsx";
import { TABLET_MIN_WIDTH } from "../../../utils/constants.tsx";
import { FilterContext } from "../../formComponents/FilterContext/FilterContext.tsx";

export const SightingsTable = () => {
  const [sightingsData, setSightingsData] = useState<SightingsResponse>();
  const [errorMessage, setErrorMessage] = useState("");
  const [totalPages, setTotalPages] = useState(0);
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

  // To update totalPages
  useEffect(() => {
    if (sightingsData && sightingsData.totalCount !== undefined) {
      const totalPages = sightingsData.totalCount
        ? Math.ceil(
            sightingsData.totalCount / filterContext.filterData.PageSize,
          )
        : 0;

      setTotalPages(totalPages);
    }
  }, [sightingsData]);

  // API call to fetch when filter data changes
  useEffect(() => {
    const handleChangeInFilter = async () => {
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
      PageNumber: Math.max(filterContext.filterData.PageNumber - 1, 1),
    });
    window.scrollTo(0, 0);
  };

  const handleNext = () => {
    filterContext.updateFilter({
      ...filterContext.filterData,
      PageNumber: filterContext.filterData.PageNumber + 1,
    });
    window.scrollTo(0, 0);
  };

  const renderMobileView = () => {
    return (
      <div className="sightings-container">
        <ul id="Sightings" className="ul-container">
          {sightingsData?.sightings?.map((sighting) => (
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
            {sightingsData?.sightings.map((sighting) => (
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

      {sightingsData && sightingsData?.totalCount > 0 && (
        <div>
          {isMobile && renderMobileView()}
          {!isMobile && renderDesktopView()}
          <p>
            Page {filterContext.filterData.PageNumber} of {totalPages}
          </p>
          <br></br>
          <div className="pagination-links">
            {filterContext.filterData.PageNumber > 1 && (
              <button className="previous" onClick={handlePrevious}>
                &laquo; Previous
              </button>
            )}
            {filterContext.filterData.PageNumber < totalPages && (
              <button className="next" onClick={handleNext}>
                Next &raquo;
              </button>
            )}
          </div>
        </div>
      )}

      {sightingsData && sightingsData?.totalCount <= 0 && (
        <h3>
          There are no sightings for the search criteria provided. Please update
          the filter criteria.
        </h3>
      )}
    </div>
  );
};
