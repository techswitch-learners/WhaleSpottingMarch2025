import { useState } from "react";
import LocationPicker, {
  GeoLocation,
} from "../../LocationPicker/LocationPicker";
import "../../LocationPicker/LocationPicker.scss";
import "./TempLocation.scss";

export const TempLocation = () => {
  const [location, setLocation] = useState<GeoLocation>();
  return (
    <div>
    <div className="temp-location-container">
      Location Picker <LocationPicker onLocationSelection={setLocation} />
    </div>
    <div> LAT: {location?.latitude} LONG: {location?.longitude} </div>
    </div>
  );
};
