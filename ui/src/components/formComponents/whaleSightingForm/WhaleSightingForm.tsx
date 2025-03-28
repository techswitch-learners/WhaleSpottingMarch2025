import React, { useState } from "react";
import Calendar from "react-calendar";
import "react-calendar/dist/Calendar.css";
import { formatDate } from "react-calendar/dist/esm/shared/dateFormatter.js";



type ValuePiece = Date | null;

type Value = ValuePiece | [ValuePiece, ValuePiece];

interface WhaleSighting {
    // Date:Calendar,
    species: number; //must be passed as a number
    description: string;
    sightingDate: Value; //date
    quantity: number; // number
    latitude: number;
    longitude: number; //number
    imgSrc: string;
}

export const WhaleSightingForm = () => {
    const [formData, setFormData] = useState<WhaleSighting>({
        species: 0,
        description: "",
        sightingDate: new Date(),
        quantity: 0,
        latitude: 0,
        longitude: 0,
        imgSrc: "",
    });



    const [dateValue, setDate] = useState<Value>(new Date());

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>): void => {
        event.preventDefault();
        console.log(formData);
        console.log(JSON.stringify(formData));
        const headers =
        {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "*",
            "Access-Control-Allow-Methods": "POST, PATCH, OPTIONS"
        }

        fetch(import.meta.env.VITE_APP_API_HOST + "/Sighting/createSighting",
            {
                method: "POST",
                body: JSON.stringify(formData),
                headers: headers

            }).then((response) => {
                if (response.status == 200) {
                    console.log("POST REQUEST SUCCESS.");
                }
                else {
                    console.log("Response status: " + response.status);
                }
            });
    };

    const handleChange = (
        event:
            | React.ChangeEvent<HTMLInputElement>
            | React.ChangeEvent<HTMLSelectElement>
            | React.ChangeEvent<HTMLTextAreaElement>
    ) => {
        const { name, value } = event.target;
        setFormData({ ...formData, [name]: value });
    };

    // const renderCalendar = () => (
    //     <div>
    //       <Calendar
    //         onChange={handleChangeCalendar}
    //         value={selectedEarthDate}
    //         minDate={getMinDate(manifestData?.photo_manifest?.landing_date)}
    //         maxDate={getYesterday()}
    //       />
    //     </div>
    //   );

    const onCalendarChange = (dateValue: Value) => {
        setDate(dateValue);
        formData.sightingDate = dateValue
    };
    return (
        <div>
            <h2>Whale Sighting Form</h2>
            <p>Tell us about the whale that you saw. </p>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Date: </label>
                    <Calendar onChange={onCalendarChange} value={dateValue} maxDate={new Date()}></Calendar>
                    <p> Selected Date: {formData.sightingDate?.toString()}</p>
                    <label htmlFor="description">Description:</label>
                    <textarea
                        id="description"
                        name="description"
                        placeholder="Describe what you saw!"
                        rows={5}
                        cols={30}
                        value={formData.description}                       
                        onChange={handleChange}
                        maxLength={255}
                        required
                    ></textarea> 
                </div>
                <label htmlFor="species">
                    Species:
                    <select
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
                    <p>Selected option: {formData.species}</p>
                </label>
                <div>
                    <label htmlFor="quantity">Quantity:</label>
                    <input
                        id="quantity"
                        name="quantity"
                        type="number"
                        min={1}
                        value={formData.quantity}
                        onChange={handleChange}
                        required
                    ></input>
                </div>
                <div>
                    <label htmlFor="latitude">Latitude: </label>
                    <input
                        id="latitude"
                        name="latitude"
                        type="number"
                        value={formData.latitude}
                        minLength={10}
                        maxLength={10}
                        onChange={handleChange}
                        required
                    ></input>
                </div>
                <div>
                    <label htmlFor="longitude">Longitude:</label>
                    <input
                        id="longitude"
                        name="longitude"
                        type="number"
                        value={formData.longitude}
                        minLength={10}
                        maxLength={10}
                        onChange={handleChange}
                        required
                    ></input>
                </div>
                <div>
                    <label htmlFor="image">Image:</label>
                    <input
                        id="image"
                        name="imgSrc"
                        type="url"
                        value={formData.imgSrc}
                        onChange={handleChange}
                        required
                    ></input>
                </div>
                <button type="submit">Submit</button>
            </form>
        </div>
    );
};
