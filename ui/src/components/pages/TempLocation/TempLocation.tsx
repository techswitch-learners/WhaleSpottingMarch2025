import { useState } from "react";
import LocationPicker, {
  GeoLocation,
} from "../../LocationPicker/LocationPicker";
import "../../LocationPicker/LocationPicker.scss";

export const TempLocation = () => {
  const [location, setLocation] = useState<GeoLocation>();
  return (
    <div>
      Location Picker{" "}
      <LocationPicker location={location} onLocationSelection={setLocation} />
    </div>
  );
};
