import axios from "axios";
import { LoginRequest, RegistrationRequest } from "../Types/userFormTypes";
import { setError, setLoading } from "../Redux/slices/uiSlice";

export const fetchRegistrationData = async (
  dispatch: Function,
  nameInput: string,
  emailInput: string,
  passwordInput: string
) => {
  const url = "https://localhost:7060/api/users/register";
  const requestBody: RegistrationRequest = {
    name: nameInput,
    email: emailInput,
    password: passwordInput,
    profilePicUrl: null,
  };
  try {
    dispatch(setLoading(true));
    const response = await axios.post(url, requestBody);
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error("Axios error:", error.response?.data || error.message);
      dispatch(setError(error.response?.data || "An error occurred"));
    } else {
      console.error("Unexpected error:", error);
      dispatch(setError("An unexpected error occurred"));
    }
    return "Failed";
  } finally {
    dispatch(setLoading(false));
  }
};

export const fetchLoginData = async (
  dispatch: Function,
  emailInput: string,
  passwordInput: string
) => {
  const url = "https://localhost:7060/api/users/login";
  const requestBody: LoginRequest = {
    email: emailInput,
    password: passwordInput,
  };

  try {
    dispatch(setLoading(true));
    const response = await axios.post(url, requestBody);
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error("Axios error:", error.response?.data || error.message);
      dispatch(setError(error.response?.data || "An error occurred"));
    } else {
      console.error("Unexpected error:", error);
      dispatch(setError("An unexpected error occurred"));
    }
    return "Failed";
  } finally {
    dispatch(setLoading(false));
  }
};
