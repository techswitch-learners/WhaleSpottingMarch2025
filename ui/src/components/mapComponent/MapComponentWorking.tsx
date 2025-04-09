import { useEffect, useRef, useState } from "react";
import { Map, Overlay, View } from "ol";
import TileLayer from "ol/layer/Tile";
// import { Vector } from "ol/source.js";
// import { VectorLayer } from "ol/layer.js";
import { Vector as VectorSource } from "ol/source.js";
import { Vector as VectorLayer } from "ol/layer.js";
import OSM from "ol/source/OSM";
import Feature from "ol/Feature.js";
import Point from "ol/geom/Point.js";
import { Icon, Style } from "ol/style.js";
import { fromLonLat } from "ol/proj";
import { SightingsResponse } from "../../models/apiModels";
// import { Species, Location } from "../../models/apiModels";
import "./MapComponent.scss";
import "ol/ol.css";
import whale_icon from "../../../../ui/public/whale-icon32.png";
import { getSightings } from "../../utils/apiClient";

export const MapComponentWorking = () => {
  const mapRef = useRef(null!);
  const popupRef = useRef<HTMLDivElement>(null!);

  // const [coordinates, setCoordinates] = useState<number[] | null>(null);

  const [sightingsData, setSightingsData] = useState<SightingsResponse[]>();
  const [popupWhaleSightingInfo, setPopupWhaleSightingInfo] = useState({
    id: 0,
    // speciesId: 0,
    speciesName: "",
    sightingDate: "",
    quantity: 0,
    longitude: 0,
    latitude: 0,
    imageSource: "",
  });
  const [whaleFeatures, setWhaleFeatures] = useState<Feature[]>();

  const fetchSightings = async () => {
    try {
      return getSightings();
    } catch (error) {
      console.error("There was a problem with the fetch operation:", error);
    }
  };

  useEffect(() => {
    const getSightings = async () => {
      const data = await fetchSightings();
      setSightingsData(data);
    };
    getSightings();
  }, []);

  // const jsonWhaleData: Array<SightingsResponse> = [
  //   {
  //     id: 1,
  //     speciesId: 1,
  //     speciesName: "Blue Whale",
  //     description: "Details of Sighting 1",
  //     sightingDate: "2024-03-01T13:21:33Z",
  //     reportDate: "2024-03-02T13:21:33Z",
  //     quantity: 1,
  //     latitude: 41.9028,
  //     longitude: -60,
  //     imageSource: "http://localhost:5067/images/blue-whale.jpg",
  //   },
  //   {
  //     id: 2,
  //     speciesId: 2,
  //     speciesName: "Humpback Whale",
  //     description: "Details of Sighting 2",
  //     sightingDate: "2024-03-02T13:21:33Z",
  //     reportDate: "2024-03-03T13:21:33Z",
  //     quantity: 1,
  //     latitude: 57.808243,
  //     longitude: -146.412739,
  //     imageSource: "http://localhost:5067/images/orca-whale.jpg",
  //   },
  // ];

  // this ensures map and array
  // 1. setup icon features

  // useEffect(() => {
  //   const iconFeatures: Feature[] = [];

  //   // create features from jsonWhaleData
  //   sightingsData?.forEach((item) => {
  //     const {
  //       id,
  //       speciesName,
  //       sightingDate,
  //       quantity,
  //       latitude,
  //       longitude,
  //       imageSource,
  //     } = item;

  //     const iconFeature = new Feature(
  //       new Point(fromLonLat([longitude, latitude])),
  //     );

  //     // sets unique ID for each icon https://openlayers.org/en/latest/apidoc/module-ol_Feature-Feature.html#setId
  //     iconFeature.setId(id);
  //     iconFeature.setProperties({
  //       species: speciesName,
  //       sightingDate: sightingDate,
  //       quantity: quantity,
  //       longitude: longitude,
  //       latitude: latitude,
  //       imgSrc: imageSource,
  //     });

  //     // set Style for each icon
  //     iconFeature.setStyle(
  //       new Style({
  //         image: new Icon({
  //           anchor: [0.5, 0.96],
  //           src: whale_icon, // https://openlayers.org/en/latest/apidoc/module-ol_style_Style.html#~StyleLike
  //         }),
  //       }),
  //     );

  //     iconFeatures.push(iconFeature);
  //   });

  //   if (iconFeatures.length == 0) {
  //     console.error("No icon features being loaded");
  //   } else {
  //     setWhaleFeatures(iconFeatures);
  //   }

  //   console.log(
  //     `item ${iconFeatures[0].getId()?.toString()} of iconFeatures =${iconFeatures[0]?.getProperties().toString()}`,
  //   );
  // }, [sightingsData]);

  // // new Vector Layer and Source
  // const featuresVectorLayer = new VectorLayer({
  //   source: new VectorSource({ features: iconFeatures }),
  // });

  useEffect(() => {
    const iconFeatures: Feature[] = [];

    const setIconFeatures = () => {
      // create features from jsonWhaleData
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
          imgSrc: imageSource,
        });

        // set Style for each icon
        iconFeature.setStyle(
          new Style({
            image: new Icon({
              anchor: [0.5, 0.96],
              src: whale_icon, // https://openlayers.org/en/latest/apidoc/module-ol_style_Style.html#~StyleLike
            }),
          }),
        );

        iconFeatures.push(iconFeature);
      });
    };

    setIconFeatures();
    // if (iconFeatures.length == 0) {
    //   console.error("No icon features being loaded");
    // } else {
    setWhaleFeatures(iconFeatures);
    // }

    // console.log(
    //   `item ${iconFeatures[0].getId()?.toString()} of iconFeatures =${iconFeatures[0]?.getProperties().toString()}`,
    // );

    // new Vector Layer and Source
    const featuresVectorLayer = new VectorLayer({
      source: new VectorSource({ features: whaleFeatures }),
    });

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
    });
    return () => map.setTarget();
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
