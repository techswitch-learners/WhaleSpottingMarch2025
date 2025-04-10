import { createContext, JSX, ReactNode, useState } from "react";
import { FilterSightings } from "../../../models/apiModels.ts";

const resetFilterData: FilterSightings = {
  PageNumber: 1,
  PageSize: 10,
  SpeciesId: 0,
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
  resetFilter: () => void;
}

export const FilterContext = createContext<FilterContextType>({
  filterData: resetFilterData,
  updateFilter: () => {},
  resetFilter: () => {},
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

  function resetFilter() {
    setFilterData(resetFilterData);
  }

  return (
    <FilterContext.Provider value={{ filterData, updateFilter, resetFilter }}>
      {props.children}
    </FilterContext.Provider>
  );
}
