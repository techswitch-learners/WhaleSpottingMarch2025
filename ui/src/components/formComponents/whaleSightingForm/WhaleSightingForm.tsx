import React, { useState, useEffect } from "react";
import Calendar from "react-calendar";
import "react-calendar/dist/Calendar.css";
import "./WhaleSightingForm.scss";
import { fetchPOSTRequest, getAllSpecies } from "../../../utils/apiClient";
import { useNavigate } from "react-router-dom";

type ValuePiece = Date | null;

type Value = ValuePiece | [ValuePiece, ValuePiece];

export interface WhaleSighting {
  species: number;
  description: string;
  sightingDate: Value;
  quantity: number;
  latitude: number;
  longitude: number;
}

interface Species {
  id: number;
  speciesName: string;
}

export const WhaleSightingForm = () => {
  const [formData, setFormData] = useState<WhaleSighting>({
    species: 0,
    description: "",
    sightingDate: new Date(),
    quantity: 0,
    latitude: 0,
    longitude: 0,
  });

  const [dateValue, setDate] = useState<Value>(new Date());
  const [errorMessage, setErrorMessage] = useState("");
  const [selectedFile, setSelectedFile] = useState<File | null>(null);
  const [preview, setPreview] = useState<string | null>(null);
  const [speciesOptions, setSpeciesOptions] = useState<Species[]>([]);
  const [error, setError] = useState(false);

  useEffect(() => {
    fetch(import.meta.env.VITE_APP_API_HOST + "/Species")
      .then((response) => response.json())
      .then((data) => {
        setSpeciesOptions(data);
        console.log(`Species data: ${data}`);
      })
      .catch((error) => {
        setError(true);
        console.log(error);
      });
  }, []);

  const navigate = useNavigate();

  const handleSubmit = async (
    event: React.FormEvent<HTMLFormElement>,
  ): Promise<void> => {
    event.preventDefault();

    const newFormDataWithImage = new FormData();
    Object.entries(formData).forEach(([key, value]) => {
      value = key === "sightingDate" ? value.toISOString() : value;
      newFormDataWithImage.append(key, value);
    });

    if (selectedFile) {
      newFormDataWithImage.append("image", selectedFile);
      newFormDataWithImage.append("ImageSource", selectedFile.name);
    }

    try {
      const response = await fetchPOSTRequest(
        newFormDataWithImage,
        "/Sighting/createSighting",
      );
      if (response >= 300) {
        setErrorMessage("An error has occurred.");
      } else {
        navigate("/ViewSightings");
      }
    } catch {
      setErrorMessage("An error has occurred.");
    }
  };

  const handleChange = (
    event:
      | React.ChangeEvent<HTMLInputElement>
      | React.ChangeEvent<HTMLSelectElement>
      | React.ChangeEvent<HTMLTextAreaElement>,
  ) => {
    const { name, value } = event.target;
    setFormData({ ...formData, [name]: value });
  };

  const onCalendarChange = (dateValue: Value) => {
    setDate(dateValue);
    formData.sightingDate = dateValue;
  };

  useEffect(() => {
    if (!selectedFile) {
      setPreview(null);
      return;
    }

    const objectUrl = URL.createObjectURL(selectedFile);
    setPreview(objectUrl);

    return () => URL.revokeObjectURL(objectUrl);
  }, [selectedFile]);

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.files) {
      setSelectedFile(event.target.files[0]);
    }
  };
  if (error) {
    return <div>Error loading species from backend</div>;
  }

  return (
    <div className="createSightingForm">
      <h2>Report your whale sighting</h2>
      <p>Tell us about the whale that you saw using the form below.</p>
      <p>* (asterisk) denotes a required field.</p>
      {errorMessage.length > 0 && (
        <p className="errorMessage">{errorMessage}</p>
      )}
      <form onSubmit={handleSubmit}>
        <div className="field">
          <div className="calendar">
            <label>
              Sighting date:
              <span className="requiredField">*</span>
            </label>
            <Calendar
              onChange={onCalendarChange}
              value={dateValue}
              maxDate={new Date()}
            ></Calendar>
          </div>
        </div>
        <div className="field">
          <label htmlFor="description">
            Description:
            <span className="requiredField">*</span>
          </label>
          <textarea
            id="description"
            name="description"
            placeholder="Describe what you saw!"
            rows={5}
            value={formData.description}
            onChange={handleChange}
            maxLength={255}
            required
          ></textarea>
        </div>
        <div className="field">
          <label htmlFor="species">
            Species:
            <span className="requiredField">*</span>
          </label>
          <select
            className="inputStyle"
            name="species"
            value={formData.species}
            onChange={handleChange}
            required
          >
            <option value="">Please select a species</option>
            {speciesOptions &&
              speciesOptions.map(({ id, speciesName }) => (
                <option key={`speciesName-${id}`} value={id}>
                  {speciesName}
                </option>
              ))}
          </select>
        </div>
        <div className="field">
          <label htmlFor="quantity">
            Quantity:
            <span className="requiredField">*</span>
          </label>
          <input
            id="quantity"
            name="quantity"
            type="number"
            className="inputStyle"
            min={1}
            placeholder="Number of whales sighted"
            value={formData.quantity}
            onChange={handleChange}
            required
          ></input>
        </div>
        <div className="field">
          <label htmlFor="latitude">
            Latitude:
            <span className="requiredField">*</span>
          </label>
          <input
            id="latitude"
            name="latitude"
            type="number"
            className="inputStyle"
            placeholder="e.g. 40.741895"
            value={formData.latitude}
            minLength={10}
            maxLength={10}
            onChange={handleChange}
            required
          ></input>
        </div>
        <div className="field">
          <label htmlFor="longitude">
            Longitude:
            <span className="requiredField">*</span>
          </label>

          <input
            id="longitude"
            name="longitude"
            type="number"
            className="inputStyle"
            placeholder="e.g. -73.989308"
            value={formData.longitude}
            minLength={10}
            maxLength={10}
            onChange={handleChange}
            required
          ></input>
        </div>
        <div className="field">
          <label htmlFor="image">Image:</label>
          <input
            className="inputStyle"
            id="image"
            name="selectedFile"
            type="file"
            placeholder="Url to your image"
            onChange={handleFileChange}
          ></input>
        </div>
        {selectedFile && preview && (
          <div>
            <p>Image preview:</p>
            <img className="preview-image" src={preview} />
          </div>
        )}
        <button type="submit">Submit</button>
      </form>
    </div>
  );
};
