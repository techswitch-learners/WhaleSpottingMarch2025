import { WhaleSighting } from "../components/formComponents/whaleSightingForm/WhaleSightingForm";
import { Login } from "../models/apiModels";

export const fetchPOSTRequest = async (
  formData: FormData,
  controllerEndpoint: string,
) => {
  const fetchResponse = await fetch(
    import.meta.env.VITE_APP_API_HOST + controllerEndpoint,
    {
      method: "POST",
      body: formData,
    },
  );
  return fetchResponse.status;
};

export const getSightings = async () => {
  const fetchResponse = await fetch(
    import.meta.env.VITE_APP_API_HOST + "/Sighting",
  );
  const data = await fetchResponse.json();
  return data;
};

export const getAllSpecies = async () => {
  const fetchResponse = await fetch(
    import.meta.env.VITE_APP_API_HOST + "/Species/",
  );
  const data = await fetchResponse.json();
  return data;
};

export const login = async (login: Login) => {
  const fetchResponse = await fetch(
    import.meta.env.VITE_APP_API_HOST + "/Account/login",
    {
      method: "POST",
      body: JSON.stringify(login),
      headers: new Headers({ "Content-Type": "application/json" }),
    },
  );
  return fetchResponse;
};
