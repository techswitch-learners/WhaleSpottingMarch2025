export const fetchLocationData = async (
  latitude: number,
  longitude: number,
  radius: number,
) => {
  const locationControllerEndpoint = `/Location?latitude=${latitude}&longitude=${longitude}&radius=${radius}`;
  const apiUrl = import.meta.env.VITE_APP_API_HOST + locationControllerEndpoint;
  try {
    const response = await fetch(apiUrl);
    if (!response.ok)
      throw new Error(
        `Unable to Fetch location endpoint response. Status: ${response.status}`,
      );
    const locationData = await response.json();
    return locationData;
  } catch (error) {
    console.error("Error fetching data:", error);
    return null;
  }
};
