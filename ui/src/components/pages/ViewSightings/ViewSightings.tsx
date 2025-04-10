import React, { useEffect, useState } from "react";
import "./ViewSightings.scss";
import {
  FilterSigtings,
  SightingsResponse,
  Species,
} from "../../../models/apiModels.ts";
import {
  getFilteredSightings,
  getSightings,
  getAllSpecies,
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
  const [speciesOptions, setSpeciesOptions] = useState<Species[]>([]);
  const [speciesLoadingError, setSpeciesLoadingError] = useState(false);
  const resetFilterData: FilterSigtings = {
    PageNumber: 1,
    PageSize: 10,
    SpeciesId: null,
    HasImage: true,
    SightingStartDate: null,
    SightingEndDate: null,
  };
  const [filterData, setFilterData] = useState<FilterSigtings>(resetFilterData);
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

  useEffect(() => {
    async function fetchSpecies() {
      const species = await getAllSpecies().catch((error) => {
        setSpeciesLoadingError(error);
      });

      setSpeciesOptions(species);
    }
    fetchSpecies();
  }, []);

  const submitFilterData = async (
    event: React.FormEvent<HTMLFormElement>,
  ): Promise<void> => {
    event.preventDefault();
    console.log(filterData);
    try {
      filterData.PageNumber = 1;
      const response = await getFilteredSightings(filterData);
      setSightingsData(response);
    } catch (error) {
      setErrorMessage("An error has occurred." + error);
    }
  };

  useEffect(() => {
    const handleChangeInFilter = async () => {
      console.log("inside useEffect handleChangeInFilter");
      try {
        const response = await getFilteredSightings(filterData);
        setSightingsData(response);
      } catch (error) {
        setErrorMessage("An error has occurred. " + error);
      }
    };
    handleChangeInFilter();
  }, [filterData]);

  const handleChange = (
    event:
      | React.ChangeEvent<HTMLInputElement>
      | React.ChangeEvent<HTMLSelectElement>,
  ) => {
    const { name, value } = event.target;
    setFilterData({ ...filterData, [name]: value });
    console.log(filterData);
  };

  const handleCheckBoxChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setFilterData({ ...filterData, HasImage: event.target.checked });
    console.log(filterData);
  };

  const handlePrevious = () => {
    setFilterData({
      ...filterData,
      PageNumber: Math.min(filterData.PageNumber - 1, 1),
    });
  };

  const handleNext = () => {
    setFilterData({ ...filterData, PageNumber: filterData.PageNumber + 1 });
  };

  const ClearFilterData = (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
    setFilterData(resetFilterData);
    const form = document.getElementById(
      "filterSightingsForm",
    ) as HTMLFormElement;
    form.reset();
  };

  const renderFilterMobileView = () => {
    return (
      <div className="filterContainer">
        <form
          id="filterSightingsForm"
          name="filterSightings"
          onSubmit={submitFilterData}
        >
          <div>
            <label htmlFor="Start Date">
              <strong> Start Date: </strong>
            </label>
            <input
              type="date"
              name="SightingStartDate"
              onChange={handleChange}
              max={new Date().toISOString().split("T")[0]}
            ></input>
          </div>
          <div>
            <label htmlFor="End Date">
              <strong> End Date: </strong>
            </label>
            <input
              type="date"
              name="SightingEndDate"
              onChange={handleChange}
              max={new Date().toISOString().split("T")[0]}
            ></input>
          </div>
          <div>
            <label htmlFor="Species">
              <strong> Species: </strong>
            </label>
            <select name="SpeciesId" onChange={handleChange}>
              {speciesLoadingError ? (
                <div>Error loading species</div>
              ) : (
                <>
                  <option value="">Please select a species</option>
                  {speciesOptions &&
                    speciesOptions.map(({ id, speciesName }) => (
                      <option key={`speciesName-${id}`} value={id}>
                        {speciesName}
                      </option>
                    ))}
                </>
              )}
            </select>
          </div>
          <div>
            <label htmlFor="HasImage">
              <strong> Has Image: </strong>
            </label>
            <input
              id="hasImage"
              type="checkbox"
              name="HasImage"
              onChange={handleCheckBoxChange}
            />
          </div>
          <div>
            <button id="clearButton" type="button" onClick={ClearFilterData}>
              Clear
            </button>
          </div>
        </form>
      </div>
    );
  };

  const renderFilterDesktopView = () => {
    return (
      <div className="filterContainer">
        <form
          id="filterSightingsForm"
          name="filterSightings"
          onSubmit={submitFilterData}
        >
          <label htmlFor="Start Date">
            <strong> Start Date: </strong>
          </label>
          <input
            type="date"
            name="SightingStartDate"
            onChange={handleChange}
            max={new Date().toISOString().split("T")[0]}
          ></input>

          <label htmlFor="End Date">
            <strong> End Date: </strong>
          </label>
          <input
            type="date"
            name="SightingEndDate"
            onChange={handleChange}
            max={new Date().toISOString().split("T")[0]}
          ></input>

          <label htmlFor="Species">
            <strong> Species: </strong>
          </label>
          <select name="SpeciesId" onChange={handleChange}>
            {speciesLoadingError ? (
              <div>Error loading species</div>
            ) : (
              <>
                <option value="">Please select a species</option>
                {speciesOptions &&
                  speciesOptions.map(({ id, speciesName }) => (
                    <option key={`speciesName-${id}`} value={id}>
                      {speciesName}
                    </option>
                  ))}
              </>
            )}
          </select>

          <label htmlFor="HasImage">
            <strong> Has Image: </strong>
          </label>
          <input
            id="hasImage"
            type="checkbox"
            name="HasImage"
            onChange={handleCheckBoxChange}
          />
          <button id="clearButton" type="button" onClick={ClearFilterData}>
            Clear
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
      <div className="pagination-links">
        <button onClick={handlePrevious} disabled={filterData.PageNumber === 1}>
          &laquo; Previous
        </button>
        <button onClick={handleNext}>Next &raquo;</button>
      </div>
    </div>
  );
};
