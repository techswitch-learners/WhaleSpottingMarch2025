export interface SightingsResponse {
  id: number;
  species: Species;
  description: string;
  sightingDate: string;
  reportDate: string;
  quantity: number;
  location: Location;
  imageSource: string;
}

export interface Species {
  id: number;
  speciesName: string;
}

export interface Location {
  id: number;
  latitude: number;
  longitude: number;
}

export interface Login {
  username: string;
  password: string;
}

export interface Registration {
  name: string;
  username: string;
  email: string;
  password: string;
}
