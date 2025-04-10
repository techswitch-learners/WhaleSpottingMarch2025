import "./ViewSightings.scss";
import { FilterManager } from "../../formComponents/FilterContext/FilterContext.tsx";
import { SightingsFilter } from "../../formComponents/SightingsFilter/SightingsFilter.tsx";
import { SightingsTable } from "../../formComponents/SightingsTable/SightingsTable.tsx";

export const ViewSightings = () => {
  return (
    <div id="SightingsListContainer">
      <FilterManager>
        <SightingsFilter></SightingsFilter>
        <SightingsTable></SightingsTable>
      </FilterManager>
    </div>
  );
};
