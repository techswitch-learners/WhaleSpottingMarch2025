import { useEffect, useRef } from "react";
import { Map, Overlay, View } from "ol";
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

function LocationPicker({ onLocationSelection }: LocationPickerProps) {
  const mapRef = useRef(null!);
  const popupRef = useRef<HTMLDivElement>(null!);

  useEffect(() => {
    const osmLayer = new TileLayer({
      preload: Infinity,
      source: new OSM(),
    });
    const overlay = new Overlay({
      element: popupRef.current,
      autoPan: {
        animation: {
          duration: 250,
        },
      },
    });

    const map = new Map({
      target: mapRef.current,
      layers: [osmLayer],
      view: new View({
        center: [0, 0],
        zoom: 0,
      }),
      overlays: [overlay],
    });

    map.on("click", (event) => {
      const clickedCoordinate = event.coordinate;
      const longitudelatitude = toLonLat(clickedCoordinate);
      const location = {
        longitude: longitudelatitude[0],
        latitude: longitudelatitude[1],
      };
      onLocationSelection(location);
      overlay.setPosition(clickedCoordinate);

      if (popupRef.current) {
        popupRef.current.innerHTML =
          `<p>Latitude:</p><code>` +
          location.latitude +
          `</code><p>Longitude:</p><code>` +
          location.longitude +
          `</code>`;
      }
      overlay.setPosition(clickedCoordinate);
    });
    return () => map.setTarget();
  }, [onLocationSelection]);

  return (
    <div className="location-picker-container">
      <div ref={mapRef} className="location-picker"></div>
      <div ref={popupRef} className="ol-popup"></div>
    </div>
  );
}

export default LocationPicker;
