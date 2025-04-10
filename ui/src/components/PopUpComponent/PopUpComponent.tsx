import "ol/ol.css";

export interface PopupProps {
  whaleSightingInfo: {
    id: number;
    speciesName: string;
    sightingDate: string;
    quantity: number;
    latitude: number;
    longitude: number;
    imageSource: string;
  } | null;
}

export const Popup = ({ whaleSightingInfo }: PopupProps) => {
  if (!whaleSightingInfo) {
    return null;
  }

  return (
    <>
      <div className="pop-content">
        <p>{whaleSightingInfo.speciesName} Sighting</p>
        <p>Latitude: {whaleSightingInfo.latitude}</p>
        <p>Longitude: {whaleSightingInfo.longitude}</p>
        <div>
          Image: <img src={whaleSightingInfo.imageSource} />
        </div>
      </div>
    </>
  );
};
