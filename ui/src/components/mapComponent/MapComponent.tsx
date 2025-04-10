import { useEffect, useRef, useState } from "react";
import { Map, Overlay, View } from "ol";
import TileLayer from "ol/layer/Tile";
import { Vector as VectorSource } from "ol/source.js";
import { Vector as VectorLayer } from "ol/layer.js";
import OSM from "ol/source/OSM";
import Feature from "ol/Feature.js";
import Point from "ol/geom/Point.js";
import { Icon, Style } from "ol/style.js";
import { fromLonLat } from "ol/proj";
import { SightingsResponse } from "../../models/apiModels";
import "./MapComponent.scss";
import "ol/ol.css";
import whale_icon from "/src/whale-icon32.png";
import { getSightings } from "../../utils/apiClient";

export const MapComponent = () => {
  const mapRef = useRef(null!);
  const popupRef = useRef<HTMLDivElement>(null!);

  // const [coordinates, setCoordinates] = useState<number[] | null>(null);

  const [sightingsData, setSightingsData] = useState<SightingsResponse[]>();
  const [popupWhaleSightingInfo, setPopupWhaleSightingInfo] = useState({
    id: 0,
    speciesName: "",
    sightingDate: "",
    quantity: "",
    longitude: 0,
    latitude: 0,
    imageSource: "",
  });
  const [mapRendered, setMapRendered] = useState(false);
  // const [popupWhaleSightingInfo, setPopupWhaleSightingInfo] = useState<{
  //   id: number;
  //   speciesName: string;
  //   sightingDate: string;
  //   quantity: number;
  //   longitude: number;
  //   latitude: number;
  //   imageSource: string;
  // } | null>(null);
  const [loadingError, setLoadingError] = useState(false);

  const fetchSightings = async () => {
    try {
      const data = await getSightings();
      if (!data) {
        throw new Error("No sightings data loaded");
      }
      setSightingsData(data);
      console.log("Fetched data");
    } catch (error) {
      console.error(error);
      setLoadingError(true);
    }
  };

  useEffect(() => {
    fetchSightings();
  }, []);

  useEffect(() => {
    const renderMap = () => {
      const iconFeatures: Feature[] = [];

      const setIconFeatures = () => {
        sightingsData?.forEach((item) => {
          const {
            id,
            speciesName,
            sightingDate,
            quantity,
            latitude,
            longitude,
            imageSource,
          } = item;

          const iconFeature = new Feature(
            new Point(fromLonLat([longitude, latitude])),
          );

          // sets unique ID for each icon https://openlayers.org/en/latest/apidoc/module-ol_Feature-Feature.html#setId
          iconFeature.setId(id);
          iconFeature.setProperties({
            species: speciesName,
            sightingDate: sightingDate,
            quantity: quantity,
            longitude: longitude,
            latitude: latitude,
            imageSource: imageSource,
          });

          // set Style for each icon
          iconFeature.setStyle(
            new Style({
              image: new Icon({
                anchor: [0.5, 0.96],
                src: whale_icon,
              }),
            }),
          );

          iconFeatures.push(iconFeature);
        });
      };

      setIconFeatures();

      // new Vector Layer and Source
      const featuresVectorLayer = new VectorLayer({
        source: new VectorSource({ features: iconFeatures }),
      });

      // 2. Setup Map, View and other layers
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
        console.log("In Map Onclick....before forEachFeatureAtPixel()");
        map.forEachFeatureAtPixel(event.pixel, (feature) => {
          const key = feature.getId();
          console.log(`In Map OnClick block. Feature id = ${key}`);
          if (key) {
            const properties = feature.getProperties();
            setPopupWhaleSightingInfo({
              id: properties.id,
              speciesName: properties.species,
              sightingDate: properties.sightingDate,
              quantity: properties.quantity,
              longitude: properties.longitude,
              latitude: properties.latitude,
              imageSource: properties.imageSource,
            });
          }
          console.log(`Id of sighting = ${popupWhaleSightingInfo?.id}`);
        });
        // if (!map.hasFeatureAtPixel(event.pixel)) {
        //   setPopupWhaleSightingInfo(null);
        // }
      });
      setMapRendered(true);
      return () => map.setTarget();
    };
    if (!mapRendered && sightingsData && sightingsData.length > 0) {
      renderMap();
    }
  }, [sightingsData]);

  // this ensures the popup info only reloads when popup box is reset with new values
  useEffect(() => {
    if (popupRef.current && popupWhaleSightingInfo) {
      popupRef.current.innerHTML =
        `<p>Sighting:</p><code>` +
        popupWhaleSightingInfo.speciesName +
        `<p>Latitude:</p><code>` +
        popupWhaleSightingInfo.latitude +
        `</code><p>Longitude:</p><code>` +
        popupWhaleSightingInfo.longitude +
        `</code>`;
    }
  }, [popupWhaleSightingInfo]);

  if (loadingError) {
    return <div>Error loading species</div>;
  }

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
