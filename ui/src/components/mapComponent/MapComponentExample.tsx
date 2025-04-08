import React, { useEffect, useState } from 'react';
import ol from 'ol';
import 'ol/ol.css';

const MapComponent: React.FC = () => {
  const [modalVisible, setModalVisible] = useState(false);
  const [modalContent, setModalContent] = useState({
    title: '',
    imgSrc: '',
    speciesName: '',
    sightingDate: '',
    quantity: 0
  });
  const [clusterDistance, setClusterDistance] = useState(40); // default cluster distance

  const jsonWhaleData = [
    {
      id: 1,
      species: {
        id: 1,
        speciesName: "Blue Whale",
      },
      speciesId: 1,
      description: "Details of Sighting 1",
      sightingDate: "2024-03-01T13:21:33Z",
      reportDate: "2024-03-02T13:21:33Z",
      quantity: 1,
      location: {
        id: 1,
        latitude: 41.9028,
        longitude: -60,
      },
      locationId: 1,
      imageSource: "http://localhost:5067/images/blue-whale.jpg",
    },
    {
      id: 2,
      species: {
        id: 2,
        speciesName: "Humpback Whale",
      },
      speciesId: 2,
      description: "Details of Sighting 2",
      sightingDate: "2024-03-02T13:21:33Z",
      reportDate: "2024-03-03T13:21:33Z",
      quantity: 1,
      location: {
        id: 2,
        latitude: 57.808243,
        longitude: -146.412739,
      },
      locationId: 2,
      imageSource: "http://localhost:5067/images/orca-whale.jpg",
    },
  ];

  useEffect(() => {
    const createStyle = (src: string, img?: HTMLImageElement) => new ol.style.Style({
      image: new ol.style.Icon({
        anchor: [0.5, 0.96],
        crossOrigin: 'anonymous',
        src,
        img,
        imgSize: img ? [img.width, img.height] : undefined
      })
    });

    const iconFeatures: ol.Feature[] = [];

    const setIconFeatures = () => {
      jsonWhaleData.forEach((item) => {
        const { location, species, sightingDate, quantity, imageSource } = item;
        
        const iconFeature = new ol.Feature(new ol.geom.Point(ol.proj.fromLonLat([location.longitude, location.latitude])));
        iconFeature.setId(item.id.toString()); // Unique ID for each sighting
        iconFeature.set('style', createStyle('https://openlayers.org/en/latest/examples/data/icon.png'));
        iconFeature.setProperties({
          title: `${species.speciesName} Sighting`,
          imgSrc: imageSource,
          speciesName: species.speciesName,
          sightingDate: sightingDate,
          quantity: quantity
        });
        iconFeatures.push(iconFeature);
      });
    };

    setIconFeatures();

    const source = new ol.source.Vector({ features: iconFeatures });

    const clusterSource = new ol.source.Cluster({
      distance: clusterDistance,
      source
    });

    const unclusteredLayer = new ol.layer.Vector({
      source,
      style: (feature) => feature.get('style'),
      maxResolution: 2000
    });

    const clusters = new ol.layer.Vector({
      source: clusterSource,
      style: (feature) => {
        const size = feature.get('features').length;
        const style = new ol.style.Style({
          image: new ol.style.Circle({
            radius: 10,
            stroke: new ol.style.Stroke({ color: '#fff' }),
            fill: new ol.style.Fill({ color: '#3399CC' })
          }),
          text: new ol.style.Text({
            text: size.toString(),
            fill: new ol.style.Fill({ color: '#fff' })
          })
        });
        return style;
      },
      minResolution: 2001
    });

    const raster = new ol.layer.Tile({
      source: new ol.source.OSM()
    });

    const map = new ol.Map({
      target: 'map',
      layers: [raster, clusters, unclusteredLayer],
      view: new ol.View({
        center: ol.proj.fromLonLat([30.5191, 50.4227]), // Default center
        zoom: 6
      })
    });

    const handleMapClick = (event: any) => {
      map.forEachFeatureAtPixel(event.pixel, (feature) => {
        const key = feature.getId();
        if (key) {
          const properties = feature.getProperties();
          setModalContent({
            title: properties.title,
            imgSrc: properties.imgSrc,
            speciesName: properties.speciesName,
            sightingDate: properties.sightingDate,
            quantity: properties.quantity
          });
          setModalVisible(true);
        }
      });
    };

    map.on('click', handleMapClick);

    return () => {
      // Cleanup map when the component unmounts
      map.setTarget(null);
    };
  }, [clusterDistance]);

  const closeModal = () => {
    setModalVisible(false);
  };

  return (
    <div>
      <div id="map" className="map" style={{ height: '500px', width: '100%' }}></div>
      
      <form>
        <label>Cluster distance</label>
        <input
          id="distance"
          type="range"
          min="0"
          max="100"
          step="1"
          value={clusterDistance}
          onChange={(e) => setClusterDistance(Number(e.target.value))}
        />
      </form>

      {/* Modal */}
      {modalVisible && (
        <div id="myModal" className="modal" style={{
          display: modalVisible ? 'block' : 'none',
          position: 'fixed',
          zIndex: 1,
          left: 0,
          top: 0,
          width: '100%',
          height: '100%',
          overflow: 'auto',
          backgroundColor: 'rgba(0, 0, 0, 0.4)',
        }} onClick={closeModal}>
          <div className="modal-content" style={{
            backgroundColor: '#fefefe',
            margin: '15% auto',
            padding: '20px',
            border: '1px solid #888',
            width: '550px',
          }}>
            <span className="close" style={{ fontSize: '28px', fontWeight: 'bold', cursor: 'pointer' }} onClick={closeModal}>×</span>
            <h2>{modalContent.speciesName}</h2>
            <img src={modalContent.imgSrc} alt={modalContent.speciesName} style={{ width: '100%' }} />
            <p><strong>Sighting Date:</strong> {new Date(modalContent.sightingDate).toLocaleDateString()}</p>
            <p><strong>Quantity:</strong> {modalContent.quantity}</p>
          </div>
        </div>
      )}
    </div>
  );
};

export default MapComponentExample;
