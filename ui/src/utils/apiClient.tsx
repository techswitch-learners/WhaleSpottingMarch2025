import { WhaleSighting } from "../components/formComponents/whaleSightingForm/WhaleSightingForm";

export const fetchPOSTRequest = async (
  formData: WhaleSighting,
  controllerEndpoint: string,
) => {
  const fetchResponse = await fetch(
    import.meta.env.VITE_APP_API_HOST + controllerEndpoint,
    {
      method: "POST",
      body: JSON.stringify(formData),
      headers: new Headers({ "Content-Type": "application/json" }),
    },
  );
  return fetchResponse.status;
};
