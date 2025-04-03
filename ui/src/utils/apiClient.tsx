// import { WhaleSighting } from "../components/formComponents/whaleSightingForm/WhaleSightingForm";

export const fetchPOSTRequest = async (
  formData: FormData,
  controllerEndpoint: string,
) => {
  const fetchResponse = await fetch(
    import.meta.env.VITE_APP_API_HOST + controllerEndpoint,
    {
      method: "POST",
      body: formData,
      // headers: new Headers({ "Content-Type": "application/json" }),
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
