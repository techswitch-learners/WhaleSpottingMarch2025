import { useEffect, useRef, useState } from "react"
import { Map, View } from "ol"
import TileLayer from "ol/layer/Tile"
import OSM from "ol/source/OSM"
import { toLonLat } from "ol/proj"

function MapComponent() {
  const mapRef = useRef(null!)
  const[longitude, setLongitude] = useState<number|null>(null);
  const[latitude, setLatitude] = useState<number|null>(null);

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
        setLatitude(longitudelatitude[1]);
        setLongitude(longitudelatitude[0]);
    });
    return () => map.setTarget( )
  }, [])

  return (
    <div>
        <div
        style={{ height: "80vh", width: "80vh" }}
        ref={mapRef}
        className="map-container">
        </div>
        <h1>Coordinates</h1>
        <p>Longitude: {longitude}</p>
        <p>Latitude: {latitude}</p>
    </div>
  )
}

export default MapComponent