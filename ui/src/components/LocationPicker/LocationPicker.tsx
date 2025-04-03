import { useEffect, useRef } from "react";
import { Map, View } from "ol";
import TileLayer from "ol/layer/Tile";
import OSM from "ol/source/OSM";
import { toLonLat } from "ol/proj";
import "./LocationPicker.scss";
import "ol/ol.css";

export interface LocationPickerProps {
  location: GeoLocation | undefined;
  onLocationSelection: (location: GeoLocation) => void;
}

export type GeoLocation = {
  longitude: number;
  latitude: number;
};

function LocationPicker({
  location,
  onLocationSelection,
}: LocationPickerProps) {
  const mapRef = useRef(null!);

  useEffect(() => {
    const osmLayer = new TileLayer({
      preload: Infinity,
      source: new OSM(),
    });

    const map = new Map({
      target: mapRef.current,
      layers: [osmLayer],
      view: new View({
        center: [0, 0],
        zoom: 0,
      }),
    });

    map.on("click", (event) => {
      const clickedCoordinate = event.coordinate;
      const longitudelatitude = toLonLat(clickedCoordinate);
      const clickedLocation: GeoLocation = {
        longitude: longitudelatitude[0],
        latitude: longitudelatitude[1],
      };
      onLocationSelection(clickedLocation);
    });
    return () => map.setTarget();
  }, [onLocationSelection]);

  return (
    <div className="location-picker-container">
      <div ref={mapRef} className="location-picker"></div>
      {location && (
        <div>
          <p>Coordinates</p>
          <p>Longitude: {location.longitude}</p>
          <p>Latitude: {location.latitude}</p>
        </div>
      )}
    </div>
  );
}

export default LocationPicker;
