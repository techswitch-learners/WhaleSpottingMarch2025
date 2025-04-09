import { useEffect, useRef, useState } from "react";
import { Map, Overlay, View } from "ol";
import TileLayer from "ol/layer/Tile";
import { Vector } from "ol/source.js";
// import { VectorLayer } from "ol/layer.js";
import { Vector as VectorSource } from "ol/source.js";
import { Vector as VectorLayer } from "ol/layer.js";
import OSM from "ol/source/OSM";
import Feature from "ol/Feature.js";
import Point from "ol/geom/Point.js";
import { Icon, Style } from "ol/style.js";
import { fromLonLat } from "ol/proj";
import { SightingsResponse, Species, Location } from "../../models/apiModels";
import { WhaleSighting } from "../formComponents/whaleSightingForm/WhaleSightingForm";
import "./MapComponent.scss";
import "ol/ol.css";

// export interface WhaleSightingLandingDisplay {
//   id: number | string;
//   species: string;
//   sightingDate: string;
//   quantity: number;
//   longitude: number;
//   latitude: number;
//   imageSource: string;
// }

export const MapComponentWorking = () => {
  const mapRef = useRef(null!);
  const popupRef = useRef<HTMLDivElement>(null!);

  // const [coordinates, setCoordinates] = useState<number[] | null>(null);
  const [popupWhaleSightingInfo, setPopupWhaleSightingInfo] = useState({
    id: 0,
    species: "",
    sightingDate: "",
    quantity: 0,
    location: { longitude: 0, latitude: 0 },
    imageSource: "",
  });

  const jsonWhaleData: Array<SightingsResponse> = [
    {
      id: 1,
      species: {
        id: 1,
        speciesName: "Blue Whale",
      },
      // speciesId: 1,
      description: "Details of Sighting 1",
      sightingDate: "2024-03-01T13:21:33Z",
      reportDate: "2024-03-02T13:21:33Z",
      quantity: 1,
      location: {
        id: 1,
        latitude: 41.9028,
        longitude: -60,
      },
      // locationId: 1,
      imageSource: "http://localhost:5067/images/blue-whale.jpg",
    },
    {
      id: 2,
      species: {
        id: 2,
        speciesName: "Humpback Whale",
      },
      // speciesId: 2,
      description: "Details of Sighting 2",
      sightingDate: "2024-03-02T13:21:33Z",
      reportDate: "2024-03-03T13:21:33Z",
      quantity: 1,
      location: {
        id: 2,
        latitude: 57.808243,
        longitude: -146.412739,
      },
      // locationId: 2,
      imageSource: "http://localhost:5067/images/orca-whale.jpg",
    },
  ];

  // 1. setup icon features
  const iconFeatures: Feature[] = [];

  const setIconFeatures = () => {
    // create features from jsonWhaleData
    jsonWhaleData.forEach((item) => {
      const { id, species, sightingDate, quantity, location, imageSource } =
        item;

      const iconFeature = new Feature(
        new Point(fromLonLat([location.longitude, location.latitude])),
      );

      console.log(
        `location lat ${location.latitude}, lon ${location.longitude}`,
      );

      // sets unique ID for each icon https://openlayers.org/en/latest/apidoc/module-ol_Feature-Feature.html#setId
      iconFeature.setId(id);
      iconFeature.setProperties({
        species: species.speciesName,
        sightingDate: sightingDate,
        quantity: quantity,
        location: location,
        imgSrc: imageSource,
      });

      // set Style for each icon
      iconFeature.setStyle(
        new Style({
          image: new Icon({
            anchor: [0.5, 0.96],
            src: "https://openlayers.org/en/latest/examples/data/icon.png", // https://openlayers.org/en/latest/apidoc/module-ol_style_Style.html#~StyleLike
          }),
        }),
      );

      iconFeatures.push(iconFeature);
    });
  };

  setIconFeatures();

  console.log(
    `item ${iconFeatures[0].getId()?.toString()} of iconFeatures =${iconFeatures[0]?.getProperties().toString()}`,
  );

  // new Vector Layer and Source
  const featuresVectorLayer = new VectorLayer({
    source: new VectorSource({ features: iconFeatures }),
  });

  // this ensures map and array
  useEffect(() => {
    // 2. Setup Map, View and other layers: https://openlayers.org/doc/tutorials/concepts.html
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
      layers: [osmLayer, featuresVectorLayer],
      view: new View({
        center: [0, 0],
        zoom: 1,
      }),
      overlays: [overlay],
    });

    map.on("click", (event) => {
      const clickedCoordinate = event.coordinate;
      overlay.setPosition(clickedCoordinate);

      // https://openlayers.org/en/latest/apidoc/module-ol_Map-Map.html#forEachFeatureAtPixel
      map.forEachFeatureAtPixel(event.pixel, (feature) => {
        const key = feature.getId();
        console.log(`In Map OnClick block. Feature id = ${key}`);
        if (key) {
          const properties = feature.getProperties();
          setPopupWhaleSightingInfo({
            id: properties.id,
            species: properties.species,
            sightingDate: properties.sightingDate,
            quantity: properties.quantity,
            location: properties.location,
            imageSource: properties.imageSource,
          });
        }
        console.log(`Id of sighting = ${popupWhaleSightingInfo?.id}`);
      });
    });
    return () => map.setTarget();
  }, []);

  // this ensures the popup info only reloads when popup box is reset with new values
  useEffect(() => {
    if (popupRef.current && popupWhaleSightingInfo) {
      popupRef.current.innerHTML =
        `<p>Sighting:</p><code>` +
        popupWhaleSightingInfo.species +
        `<p>Latitude:</p><code>` +
        popupWhaleSightingInfo.location.latitude +
        `</code><p>Longitude:</p><code>` +
        popupWhaleSightingInfo.location.longitude +
        `</code>`;
    }
  }, [popupWhaleSightingInfo]);

  return (
    <div>
      <div
        style={{ height: "70vh", width: "100%" }}
        ref={mapRef}
        className="map-container"
      ></div>
      {popupWhaleSightingInfo && (
        <div ref={popupRef} className="ol-popup"></div>
      )}
    </div>
  );
};
