import { fetchLocationData } from "../../../utils/locationApiClient";
import { useState, useEffect } from "react";
import LocationPicker, {
  GeoLocation,
} from "../../formComponents/LocationPicker/LocationPicker";
import { format } from "date-fns";
import {
  SightingsResponse,
  DailyWeatherForecast,
  TopSpecies,
  LocationSearchResponse,
} from "../../../models/apiModels.ts";

export const ViewLocations = () => {
  const [locationData, setLocationData] = useState<LocationSearchResponse>(
    null!,
  );
  //const [loading, setLoading] = useState(true);
  const [location, setLocation] = useState<GeoLocation>({
    latitude: 40.9208,
    longitude: -60.0,
  });

  useEffect(() => {
    const handleLocation = async () => {
      const data = await fetchLocationData(
        location.latitude,
        location.longitude,
        10,
      );
      setLocationData(data);
      //setLoading(false);
    };

    if (location.latitude && location.longitude) {
      handleLocation();
    }
  }, [location.latitude, location.longitude]);

  //if (loading) return <p>Loading data</p>;
  if (!locationData) return <p>No data available for the given location</p>;

  return (
    <div>
      <p>View Locations</p>
      <div className="location-selection">
        <label className="map-label">Select Sighting Location:</label>
        <LocationPicker location={location} onLocationSelection={setLocation} />
      </div>
      <ul>
        {locationData.weatherForecast?.map(
          (day: DailyWeatherForecast, index: number) => (
            <li key={index}>
              <p>
                <strong>{format(day.date, "EEEE,dd MMM")}</strong>
              </p>
              <p>{day.description}</p>
              <p>
                <strong>{day.maxTemperatureInCelcius}</strong> / {day.minTemperatureInCelcius} &deg;C
              </p>
              <p>Visibility</p>
              <p>{day.visibilityInKm} km</p>
              <p>Max Wind Speed</p>
              <p>{day.maxWindSpeedInKmPerHour} km/h</p>
              <p>Precipitation</p>
              <p>{day.totalPrecipitationInMilimeters} mm</p>
            </li>
          ),
        )}
      </ul>
      <ul>
        {locationData.topSpecies?.map((species: TopSpecies, index: number) => (
          <li key={index}>
            <p>Top Species {index + 1}</p>
            <p> Species: {species.species}</p>
            <p> Number of Sightings: {species.numSightings}</p>
            <p> Last Seen Date: {format(species.lastSeen, "dd/MM/yyyy")} </p>
          </li>
        ))}
      </ul>
      <ul>
        {locationData.recentSightings?.map(
          (sighting: SightingsResponse, index: number) => (
            <li key={index}>
              <p>Sighting Id: {sighting.id} </p>
              <p>Species Id: {sighting.speciesId}</p>
              <p>Species Name: {sighting.speciesName}</p>
              <p>Description: {sighting.description}</p>
              <p>
                Sighting Date: {format(sighting.sightingDate, "dd/MM/yyyy")}
              </p>
              <p>Report Date: {format(sighting.reportDate, "dd/MM/yyyy")}</p>
              <p>Quantity: {sighting.quantity}</p>
              <p>
                Coordinates: {sighting.latitude}, {sighting.longitude}
              </p>
              <img src={sighting.imageSource}></img>
            </li>
          ),
        )}
      </ul>
    </div>
  );
};
