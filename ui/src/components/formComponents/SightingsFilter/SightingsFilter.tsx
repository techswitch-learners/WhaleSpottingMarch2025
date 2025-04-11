import React, { useContext, useEffect, useState } from "react";
import "./SightingFilter.scss";
import { Species } from "../../../models/apiModels.ts";
import { getAllSpecies } from "../../../utils/apiClient.tsx";
import { TABLET_MIN_WIDTH } from "../../../utils/constants.tsx";
import LocationPicker, {
  GeoLocation,
} from "../../formComponents/LocationPicker/LocationPicker.tsx";
import { FilterContext } from "../FilterContext/FilterContext.tsx";

export const SightingsFilter = () => {
  const [speciesOptions, setSpeciesOptions] = useState<Species[]>([]);
  const [speciesLoadingError, setSpeciesLoadingError] = useState(false);
  const filterContext = useContext(FilterContext);
  const [isLocationPickerVisible, setIsLocationPickerVisible] = useState(false);

  const [isMobile, setIsMobile] = useState(
    window.innerWidth <= TABLET_MIN_WIDTH,
  );
  const [location, setLocation] = useState<GeoLocation>({
    latitude: 0,
    longitude: 0,
  });

  //Location picker
  useEffect(() => {
    filterContext.updateFilter({
      ...filterContext.filterData,
      Latitude: location.latitude,
      Longitude: location.longitude,
    });
  }, [location.latitude, location.longitude]);

  // Mobile resizing
  useEffect(() => {
    const handleResize = () => {
      setIsMobile(window.innerWidth <= TABLET_MIN_WIDTH);
    };
    window.addEventListener("resize", handleResize);
    return () => window.removeEventListener("resize", handleResize);
  }, []);

  //Initial get Species
  useEffect(() => {
    async function fetchSpecies() {
      const species = await getAllSpecies().catch((error) => {
        setSpeciesLoadingError(error);
      });

      setSpeciesOptions(species);
    }
    fetchSpecies();
  }, []);

  const handleChange = (
    event:
      | React.ChangeEvent<HTMLInputElement>
      | React.ChangeEvent<HTMLSelectElement>,
  ) => {
    const { name, value } = event.target;
    filterContext.updateFilter({ ...filterContext.filterData, [name]: value });
  };

  const handleCheckBoxChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    filterContext.updateFilter({
      ...filterContext.filterData,
      HasImage: event.target.checked || null,
    });
  };

  const ClearFilterData = (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
    filterContext.resetFilter();
    const form = document.getElementById(
      "filterSightingsForm",
    ) as HTMLFormElement;
    form.reset();
  };

  const toggleLocationPicker = () => {
    setIsLocationPickerVisible((prevState) => !prevState);
  };

  const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
    e.preventDefault();
  };

  const renderFilterMobileView = () => {
    return (
      <form id="filterSightingsForm" name="filterSightings">
        <div className="filterContainer">
          <div>
            <label htmlFor="Start Date">
              <strong> Start Date: </strong>
            </label>
            <input
              type="date"
              name="SightingStartDate"
              onChange={handleChange}
              onKeyDown={handleKeyDown}
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
              onKeyDown={handleKeyDown}
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
            <label htmlFor="Radius">
              <strong> Radius (km): </strong>
            </label>
            <input id="radius" name="Radius" onChange={handleChange} />
          </div>
        </div>
        <div className="locationContainer">
          <button type="button" onClick={toggleLocationPicker}>
            {isLocationPickerVisible
              ? "Close Location Picker"
              : "Select Sighting Location"}
          </button>

          <div>
            <button id="clearButton" type="button" onClick={ClearFilterData}>
              Clear
            </button>
          </div>
        </div>

        {isLocationPickerVisible && (
          <div className="location-selection">
            <LocationPicker
              location={location}
              onLocationSelection={setLocation}
            />
          </div>
        )}
      </form>
    );
  };

  const renderFilterDesktopView = () => {
    return (
      <form id="filterSightingsForm" name="filterSightings">
        <div className="filterContainer">
          <label htmlFor="Start Date">
            <strong> Start Date: </strong>
          </label>
          <input
            type="date"
            name="SightingStartDate"
            onChange={handleChange}
            onKeyDown={handleKeyDown}
            max={new Date().toISOString().split("T")[0]}
          ></input>

          <label htmlFor="End Date">
            <strong> End Date: </strong>
          </label>
          <input
            type="date"
            name="SightingEndDate"
            onChange={handleChange}
            onKeyDown={handleKeyDown}
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
        </div>
        <div className="locationContainer">
          <label htmlFor="Radius">
            <strong> Radius (km): </strong>
          </label>
          <input id="radius" name="Radius" onChange={handleChange} />

          <button type="button" onClick={toggleLocationPicker}>
            {isLocationPickerVisible
              ? "Close Location Picker"
              : "Select Sighting Location"}
          </button>

          <button id="clearButton" type="button" onClick={ClearFilterData}>
            Clear
          </button>
        </div>

        {isLocationPickerVisible && (
          <div className="location-selection">
            <LocationPicker
              location={location}
              onLocationSelection={setLocation}
            />
          </div>
        )}
      </form>
    );
  };
  return (
    <>
      <h3>Filter Sightings</h3>
      <div className="SightingsFilterContainer">
        {isMobile && renderFilterMobileView()}
        {!isMobile && renderFilterDesktopView()}
      </div>
    </>
  );
};
