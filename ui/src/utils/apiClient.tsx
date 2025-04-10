import { Login, Registration } from "../models/apiModels";
import { FilterSightings } from "../models/apiModels";

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

export const getFilteredSightings = async (searchData: FilterSightings) => {
  const params = new URLSearchParams();

  if (searchData.PageNumber != null && searchData.PageNumber != 0)
    params.append("PageNumber", searchData.PageNumber.toString());

  if (searchData.PageSize != null && searchData.PageSize != 0)
    params.append("PageSize", searchData.PageSize.toString());

  if (searchData.HasImage != null)
    params.append("HasImage", searchData.HasImage.toString());

  if (searchData.SpeciesId != null && searchData.SpeciesId != 0)
    params.append("SpeciesId", searchData.SpeciesId.toString());

  if (
    searchData.Latitude != 0 &&
    searchData.Longitude != 0 &&
    searchData.Radius != 0
  ) {
    params.append("Latitude", searchData.Latitude.toString());
    params.append("Longitude", searchData.Longitude.toString());
    params.append("RadiusInKm", searchData.Radius.toString());
  }

  const formatDate = (date: string | Date): string => {
    return new Date(date).toISOString().replace("T", " ").slice(0, 19) + "+00";
  };

  if (searchData.SightingStartDate != null) {
    const formattedStartDate = formatDate(searchData.SightingStartDate);
    params.append("SightingStartDate", formattedStartDate);
  }

  if (searchData.SightingEndDate != null) {
    const formattedEndDate = formatDate(searchData.SightingEndDate);
    params.append("SightingEndDate", formattedEndDate);
  }

  const fetchResponse = await fetch(
    import.meta.env.VITE_APP_API_HOST + `/Sighting?${params.toString()}`,
  );
  const data = await fetchResponse.json();
  return data;
};

export const login = async (login: Login) => {
  const fetchResponse = await fetch(
    import.meta.env.VITE_APP_API_HOST + "/Account/login",
    {
      method: "POST",
      credentials: "include",
      body: JSON.stringify(login),
      headers: new Headers({
        "Content-Type": "application/json",
        "Access-Control-Allow-Credentials": "true",
      }),
    },
  );
  return fetchResponse;
};

export const logout = async () => {
  const fetchResponse = await fetch(
    import.meta.env.VITE_APP_API_HOST + "/Account/logout",
    {
      method: "POST",
      headers: new Headers({
        "Content-Type": "application/json",
        "Access-Control-Allow-Credentials": "true",
      }),
    },
  );
  return fetchResponse;
};

export const register = async (formData: Registration) => {
  const fetchResponse = await fetch(
    import.meta.env.VITE_APP_API_HOST + "/Account/register",
    {
      method: "POST",
      body: JSON.stringify(formData),
      headers: new Headers({ "Content-Type": "application/json" }),
    },
  );
  return fetchResponse;
};
