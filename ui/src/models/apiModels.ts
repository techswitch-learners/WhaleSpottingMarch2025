export interface SightingsResponse {
  totalCount: number;
  sightings: [
    {
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
      status: string;
      userName: string;
    },
  ];
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

export interface FilterSightings {
  PageNumber: number;
  PageSize: number;
  SpeciesId: number;
  HasImage: boolean;
  SightingStartDate: Date | null;
  SightingEndDate: Date | null;
  Latitude: number;
  Longitude: number;
  Radius: number;
}
