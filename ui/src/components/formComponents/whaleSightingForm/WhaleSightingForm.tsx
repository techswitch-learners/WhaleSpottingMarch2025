import React, { useState } from "react";
import Calendar from "react-calendar";
import "react-calendar/dist/Calendar.css";
import "./WhaleSightingForm.scss";

type ValuePiece = Date | null;

type Value = ValuePiece | [ValuePiece, ValuePiece];

interface WhaleSighting {
  species: number;
  description: string;
  sightingDate: Value;
  quantity: number;
  latitude: number;
  longitude: number;
  imageSource: string;
}

export const WhaleSightingForm = () => {
  const [formData, setFormData] = useState<WhaleSighting>({
    species: 0,
    description: "",
    sightingDate: new Date(),
    quantity: 0,
    latitude: 0,
    longitude: 0,
    imageSource: "",
  });

  const [dateValue, setDate] = useState<Value>(new Date());

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>): void => {
    event.preventDefault();
    console.log(formData);
    console.log(JSON.stringify(formData));
    const headers = {
      "Content-Type": "application/json",
      "Access-Control-Allow-Origin": "*",
      "Access-Control-Allow-Methods": "POST, PATCH, OPTIONS",
    };

    fetch(import.meta.env.VITE_APP_API_HOST + "/Sighting/createSighting", {
      method: "POST",
      body: JSON.stringify(formData),
      headers: headers,
    }).then((response) => {
      if (response.status == 200) {
        console.log("POST REQUEST SUCCESS.");
      } else {
        console.log("Response status: " + response.status);
      }
    });
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
  return (
    <div className="createSightingForm">
      <h2>Whale Sighting Form</h2>
      <p>Tell us about the whale that you saw.</p>
      <p>* (asterisk) denotes a required field.</p>
      <form onSubmit={handleSubmit}>
        <div className="field">
          <div className="calendar">
            <label>
              Date:
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
            <option value="1">Blue Whale</option>
            <option value="2">Humpback Whale</option>
            <option value="3">Sperm Whale</option>
            <option value="4">Orca</option>
            <option value="5">Fin Whale</option>
            <option value="6">Minke Whale</option>
            <option value="7">Beluga Whale</option>
            <option value="8">Gray Whale</option>
            <option value="9">Right Whale</option>
            <option value="10">Bowhead Whale</option>
            <option value="11">Unknown</option>
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
            name="imageSource"
            type="url"
            placeholder="Url to your image"
            value={formData.imageSource}
            onChange={handleChange}
          ></input>
        </div>
        <button type="submit">Submit</button>
      </form>
    </div>
  );
};
