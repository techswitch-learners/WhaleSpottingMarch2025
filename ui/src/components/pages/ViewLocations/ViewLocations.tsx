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
import "./ViewLocations.scss";

export const ViewLocations = () => {
  const [locationData, setLocationData] = useState<LocationSearchResponse>(
    null!,
  );
  const [loading, setLoading] = useState(true);

  const [location, setLocation] = useState<GeoLocation>({
    latitude: null!,
    longitude: null!,
  });

  useEffect(() => {
    const handleLocation = async () => {
      const data = await fetchLocationData(
        location.latitude,
        location.longitude,
        50,
      );
      setLocationData(data);
      setLoading(false);
    };

    if (location.latitude && location.longitude) {
      handleLocation();
    }
  }, [location.latitude, location.longitude]);

  if (loading)
    return (
      <div>
        <p className="page-title">
          Click on map to view weather forecast and sighting details:
        </p>
        <div className="location-data">
          <div className="location-selection">
            <label className="map-label"></label>
            <LocationPicker
              location={location}
              onLocationSelection={setLocation}
            />
          </div>
        </div>
      </div>
    );

  if (!locationData)
    return (
      <p>Weather and sightings data is only available marine locations. </p>
    );

  return (
    <div>
      <p className="page-title">
        Click on map to view weather forecast and sighting details:
      </p>
      <div className="location-data">
        <div className="location-selection">
          <label className="map-label"></label>
          <LocationPicker
            location={location}
            onLocationSelection={setLocation}
          />
        </div>
        <p className="data-title">Weather Forecast</p>
        <ul className="weather-data">
          {locationData.weatherForecast?.map(
            (day: DailyWeatherForecast, index: number) => (
              <li key={index} className="weather-list">
                <p className="weather-font">
                  <strong>{format(day.date, "EEEE,dd MMM")}</strong>
                </p>
                <p>{day.description}</p>
                <p className="weather-font">
                  <strong>{day.maxTemperatureInCelcius}</strong> /{" "}
                  {day.minTemperatureInCelcius} &deg;C
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
        {locationData.recentSightings.length === 0 && (
          <p>No nearby sightings</p>
        )}
        {locationData.recentSightings.length !== 0 && (
          <p className="data-title">Top Species</p>
        )}
        <ul className="topspecies-data">
          {locationData.topSpecies?.map(
            (species: TopSpecies, index: number) => (
              <li key={index} className="species-list">
                <p>
                  <strong>{species.species}</strong>
                </p>
                <p> Number of Sightings: {species.numSightings}</p>
                <p> Last seen on {format(species.lastSeen, "dd/MM/yyyy")} </p>
              </li>
            ),
          )}
        </ul>
        {locationData.recentSightings.length !== 0 && (
          <p className="data-title">Recent Sightings</p>
        )}
        <ul className="sightings-data">
          {locationData.recentSightings?.map(
            (sighting: SightingsResponse, index: number) => (
              <li key={index} className="sightings-list">
                <p>
                  <strong>Sighting ID: {sighting.id}</strong>
                </p>
                <p>Species seen: {sighting.speciesName}</p>
                <p>Number of whales: {sighting.quantity}</p>
                <p>
                  Sighting Date: {format(sighting.sightingDate, "dd/MM/yyyy")}
                </p>
                <p>
                  Coordinates of Sighting: {sighting.latitude},{" "}
                  {sighting.longitude}
                </p>
                <img src={sighting.imageSource}></img>
                <p>{sighting.description}</p>
              </li>
            ),
          )}
        </ul>
      </div>
    </div>
  );
};
