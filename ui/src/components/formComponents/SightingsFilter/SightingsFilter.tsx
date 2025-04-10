import React, { useContext, useEffect, useState } from "react";
import "./SightingFilter.scss";
import { FilterSightings, Species } from "../../../models/apiModels.ts";
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
  const resetFilterData: FilterSightings = {
    PageNumber: 1,
    PageSize: 50,
    SpeciesId: null,
    HasImage: true,
    SightingStartDate: null,
    SightingEndDate: null,
    Latitude: 0,
    Longitude: 0,
    Radius: 0,
  };
  // const [filterData, setFilterData] =
  //   useState<FilterSightings>(resetFilterData);
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
  const handleChange = (
    event:
      | React.ChangeEvent<HTMLInputElement>
      | React.ChangeEvent<HTMLSelectElement>,
  ) => {
    const { name, value } = event.target;
    filterContext.updateFilter({ ...filterContext.filterData, [name]: value });
    console.log(filterContext.filterData);
  };

  const handleCheckBoxChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    filterContext.updateFilter({
      ...filterContext.filterData,
      HasImage: event.target.checked,
    });
    console.log(filterContext.filterData);
  };

  const ClearFilterData = (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
    filterContext.updateFilter(resetFilterData);
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
          //onSubmit={submitFilterData}
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
          //onSubmit={submitFilterData}
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
          <div className="location-selection">
            <label className="map-label">
              Select Sighting Location:
              <span className="requiredField">*</span>
            </label>
            <LocationPicker
              location={location}
              onLocationSelection={setLocation}
            />
          </div>
          <label htmlFor="Radius">
            <strong> Radius: </strong>
          </label>
          <input id="radius" name="Radius" onChange={handleChange} />

          <button id="clearButton" type="button" onClick={ClearFilterData}>
            Clear
          </button>
        </form>
      </div>
    );
  };
  return (
    <div id="SightingsFilterContainer">
      {isMobile && renderFilterMobileView()}
      {!isMobile && renderFilterDesktopView()}
    </div>
  );
};
