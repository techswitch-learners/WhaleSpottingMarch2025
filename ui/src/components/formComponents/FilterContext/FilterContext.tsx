import React, { createContext, JSX, ReactNode, useState } from "react";
import { FilterSightings } from "../../../models/apiModels.ts";

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

interface FilterContextType {
  filterData: FilterSightings;
  updateFilter: (data: FilterSightings) => void;
}

export const FilterContext = createContext<FilterContextType>({
  filterData: resetFilterData,
  updateFilter: () => {},
});

interface FilterContextProps {
  children: ReactNode;
}

export function FilterManager(props: FilterContextProps): JSX.Element {
  const [filterData, setFilterData] =
    useState<FilterSightings>(resetFilterData);

  function updateFilter(data: FilterSightings) {
    setFilterData(data);
  }

  return (
    <FilterContext.Provider value={{ filterData, updateFilter }}>
      {props.children}
    </FilterContext.Provider>
  );
}
