import axios from "axios";
import { LoginRequest, RegistrationRequest } from "../Types/userFormTypes";
import { setError, setLoading } from "../Redux/slices/uiSlice";

const baseUrl = "https://localhost:7000/api/auth";

export const fetchRegistrationData = async (
  dispatch: Function,
  nameInput: string,
  emailInput: string,
  passwordInput: string
) => {
  const requestBody: RegistrationRequest = {
    name: nameInput,
    email: emailInput,
    password: passwordInput,
    profilePicUrl: null,
  };
  try {
    dispatch(setLoading(true));
    const response = await axios.post(`${baseUrl}/register`, requestBody, {
      withCredentials: true,
    });
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
  const requestBody: LoginRequest = {
    email: emailInput,
    password: passwordInput,
  };

  try {
    dispatch(setLoading(true));
    const response = await axios.post(`${baseUrl}/login`, requestBody, {
      withCredentials: true,
    });
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

export const authenticateUser = async (dispatch: Function) => {
  try {
    const response = await axios.post(
      `${baseUrl}/authenticate`,
      {},
      {
        withCredentials: true,
      }
    );
    console.log(response.data);
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
