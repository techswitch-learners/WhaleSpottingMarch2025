export interface SightingsResponse {
  totalCount: number;
  sightings: ApiSighting[];
}

export interface ApiSighting {
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
}

export interface PendingApprovalModel {
  id: number;
  species: Species;
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

export interface DailyWeatherForecast {
  date: Date;
  description: string;
  minTemperatureInCelcius: number;
  maxTemperatureInCelcius: number;
  visibilityInKm: number;
  maxWindSpeedInKmPerHour: number;
  totalPrecipitationInMilimeters: number;
}

export interface TopSpecies {
  species: string;
  numSightings: number;
  lastSeen: Date;
}

export interface LocationSearchResponse {
  weatherForecast: DailyWeatherForecast[];
  topSpecies: TopSpecies[];
  recentSightings: ApiSighting[];
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
export interface CreateReviewRequest {
  sightingId: number;
  approved: boolean;
  comments: string;
  updatedSighting?: UpdateSightingRequest;
}

export interface UpdateSightingRequest {
  species: Species;
  description: string;
}
