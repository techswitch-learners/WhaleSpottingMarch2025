import React, { useState } from "react";
interface WhaleSighting {
    // Date:Calendar,
    species: string; //must be passed as a number
    description: string;
    sightingDate: string; //date
    quantity: string; // number
    latitude: string; // number
    longitude: string; //number
    imgSrc: string; 
}

export const WhaleSightingForm = () => {
    const [formData, setFormData] = useState<WhaleSighting>({
        species: "",
        description: "",
        sightingDate: "",
        quantity: "",
        latitude: "",
        longitude: "",
        imgSrc: "",
    });

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>): void => {
        event.preventDefault();
        console.log(formData);
        console.log(JSON.stringify(formData));
        const headers =
        {
            "Content-Type":"application/json",
            "Access-Control-Allow-Origin":"*"
            // ,
            // "Access-Control-Allow-Methods":"POST, PATCH, OPTIONS"
        }
            
        fetch("http://localhost:5067" + "/Sighting/createSighting", { //set up .env variable
            method: "POST",
            // mode: 'no-cors',
            body: JSON.stringify(formData),
            // headers: {
            //     "Content-Type": "application/json",
            //     'Access-Control-Allow-Origin':'*',
            //     'Access-Control-Allow-Methods':'POST,PATCH,OPTIONS',
            //     Accept: 'application/json'
            // },
            headers: headers

        }).then((response) => {
            console.log("In 'then' block");
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
    ) => {
        const { name, value } = event.target;
        setFormData({ ...formData, [name]: value });
        console.log(
            `${formData.species}, hello ${formData.description} ${formData.quantity} ${formData.latitude} ${formData.longitude} ${formData.imgSrc}`
        );
    };

    
    return (
        <div>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="description">Description:</label>
                    <input
                        id="description"
                        name="description"
                        placeholder="Description"
                        type="text"
                        value={formData.description}
                        onChange={handleChange}
                    ></input>
                </div>
                <label htmlFor="species">
                    Species:
                    <select
                        name="species"
                        value={formData.species}
                        onChange={handleChange}
                    >
                        <option value="Hello">Please select a species</option>
                        <option value="Beluga Whale">Beluga Whale</option>
                        <option value="Humpback Whale">Humpback Whale</option>
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
                    ></input>
                </div>
                <div>
                    <label htmlFor="latitude">Latitude: </label>
                    <input
                        id="latitude"
                        name="latitude"
                        type="text"
                        value={formData.latitude}
                        minLength={10}
                        maxLength={10}
                        onChange={handleChange}
                    ></input>
                </div>
                <div>
                    <label htmlFor="longitude">Longitude:</label>
                    <input
                        id="longitude"
                        name="longitude"
                        type="text"
                        value={formData.longitude}
                        minLength={10}
                        maxLength={10}
                        onChange={handleChange}
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
                    ></input>
                </div>
                <button type="submit">Submit</button>
            </form>
        </div>
    );
};


