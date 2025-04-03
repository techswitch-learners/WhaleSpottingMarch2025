// MapComponent.js
import { useEffect, useRef, useState } from "react";
import { Map, View } from "ol";
import TileLayer from "ol/layer/Tile";
import OSM from "ol/source/OSM";
import { toLonLat } from "ol/proj";
import "./LocationPicker.scss";


function LocationPicker() {
  const mapRef = useRef(null!)
  const[coordinates, setCoordinates] = useState<number[]|null>(null);
 
  useEffect(() => {
    const osmLayer = new TileLayer({
      preload: Infinity,
      source: new OSM(),
    })

    const map = new Map({
      target: mapRef.current,
      layers: [osmLayer],
      view: new View({
        center: [0, 0],
        zoom: 0,
      }),
    });

    map.on('click', (event) => {
        const clickedCoordinate = event.coordinate;
        const longitudelatitude = toLonLat(clickedCoordinate);
        setCoordinates(longitudelatitude);
    });
    return () => map.setTarget( )
  }, [])

  return (
    <div className="location-picker-container">
    <div
    //   style={{ height: "80vh", width: "80vh" }}
      //style={{ height: "450px", width: "450px" }}
      ref={mapRef}
      className="location-picker">
    </div>
    {/* {coordinates &&(
     <div> 
    <p>Coordinates</p>
    <p>Longitude: {coordinates[0]}</p>
    <p>Latitude: {coordinates[1]}</p>
    </div>
    )} */}
    </div>
  )
}

export default LocationPicker