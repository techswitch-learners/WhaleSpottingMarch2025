export interface SightingsResponse {
  id: number;
  speciesId: number;
  speciesName: string;
  description: string;
  sightingDate: string;
  reportDate: string;
  quantity: number;
  latitude: number;
  longitude: number;
  imageSource: string;
}

export interface Species {
  id: number;
  speciesName: string;
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

export interface FilterSigtings {
  PageNumber: number;
  PageSize: number;
  SpeciesId: number | null;
  HasImage: boolean | null;
  SightingStartDate: Date | null;
  SightingEndDate: Date | null;
}
