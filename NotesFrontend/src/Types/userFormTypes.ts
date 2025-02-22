import { Preference } from "./preferenceTypes";

export interface RegistrationRequest {
  name: string;
  email: string;
  password: string;
  profilePicUrl?: string | null;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface UserResponse {
  id: string;
  name: string;
  email: string;
  createdAt: string;
  updatedAt?: string | null;
  notes?: string[];
  preference?: Preference | null;
}
