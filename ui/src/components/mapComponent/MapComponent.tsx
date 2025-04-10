import { useEffect, useRef, useState } from "react";
import { Map, Overlay, View } from "ol";
import { Vector as VectorSource } from "ol/source.js";
import { Vector as VectorLayer } from "ol/layer.js";
import TileLayer from "ol/layer/Tile";
import OSM from "ol/source/OSM";
import Feature from "ol/Feature.js";
import Point from "ol/geom/Point.js";
import { Icon, Style } from "ol/style.js";
import { fromLonLat } from "ol/proj";
import { Popup } from "../PopUpComponent/PopUpComponent";
import { SightingsResponse } from "../../models/apiModels";
import { getSightings } from "../../utils/apiClient";
import whale_icon from "/src/whale-icon32.png";
import "ol/ol.css";
import "./MapComponent.scss";

export const MapComponent = () => {
  const mapRef = useRef(null!);
  const popupRef = useRef<HTMLDivElement>(null!);

  const [sightingsData, setSightingsData] = useState<SightingsResponse>();

  const [popupWhaleSightingInfo, setPopupWhaleSightingInfo] = useState<{
    id: number;
    speciesName: string;
    sightingDate: string;
    quantity: number;
    longitude: number;
    latitude: number;
    imageSource: string;
  } | null>(null);
  const [mapRendered, setMapRendered] = useState(false);

  const [loadingError, setLoadingError] = useState("");

  const fetchSightings = async () => {
    try {
      const data = await getSightings();
      if (!data) {
        throw new Error("No sightings data loaded");
      }
      setSightingsData(data);
    } catch (error) {
      setLoadingError("An error has occurred." + error);
    }
  };

  useEffect(() => {
    fetchSightings();
  }, []);

  useEffect(() => {
    const renderMap = () => {
      const iconFeatures: Feature[] = [];

      const setIconFeatures = () => {
        sightingsData?.sightings?.forEach((item) => {
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

          iconFeature.setId(id);
          iconFeature.setProperties({
            species: speciesName,
            sightingDate: sightingDate,
            quantity: quantity,
            longitude: longitude,
            latitude: latitude,
            imageSource: imageSource,
          });

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

      const featuresVectorLayer = new VectorLayer({
        source: new VectorSource({ features: iconFeatures }),
      });

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
        map.forEachFeatureAtPixel(event.pixel, (feature) => {
          const key = feature.getId();
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
        });
        if (!map.hasFeatureAtPixel(event.pixel)) {
          setPopupWhaleSightingInfo(null);
        }
      });
      setMapRendered(true);
      return () => map.setTarget();
    };
    if (
      !mapRendered &&
      sightingsData?.sightings &&
      sightingsData.sightings.length > 0
    ) {
      renderMap();
    }
  }, [sightingsData, mapRendered]);

  return (
    <div>
      {loadingError.length > 0 && <p>Error loading sighting information</p>}
      <div ref={mapRef} className="map-container"></div>
      <div ref={popupRef} className="ol-popup">
        <Popup whaleSightingInfo={popupWhaleSightingInfo} />
      </div>
    </div>
  );
};
