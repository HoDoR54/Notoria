import { RegistrationRequest, UserResponse } from "../Types/userFormTypes";
import axios from "axios";
import { yupResolver } from "@hookform/resolvers/yup";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { regisReqValidationSchema } from "../Services/formAuth";
import Button from "../Components/Button";
import InputField from "../Components/InputField";

const UserRegistration = () => {
  const [currentUser, setCurrentUser] = useState<UserResponse>();
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<RegistrationRequest>({
    resolver: yupResolver(regisReqValidationSchema),
  });

  const onSubmit = async (data: RegistrationRequest) => {
    const fetchedData = await fetchRegistrationData(
      data.name,
      data.email,
      data.password
    );
    setCurrentUser(fetchedData);
    reset();
  };

  const fetchRegistrationData = async (
    nameInput: string,
    emailInput: string,
    passwordInput: string
  ) => {
    setLoading(true);
    const url = "https://localhost:7060/api/users/register";
    const requestBody: RegistrationRequest = {
      name: nameInput,
      email: emailInput,
      password: passwordInput,
      profilePicUrl: null,
    };
    try {
      const response = await axios.post(url, requestBody);
      return response.data;
    } catch (error) {
      if (axios.isAxiosError(error)) {
        console.error("Axios error:", error.response?.data || error.message);
        setError(error.response?.data || "An error occurred");
      } else {
        console.error("Unexpected error:", error);
        setError("An unexpected error occurred");
      }
      return "Failed";
    } finally {
      setLoading(false);
    }
  };

  return (
    <form
      onSubmit={handleSubmit(onSubmit)}
      className="flex flex-col items-center justify-center gap-3 rounded-lg shadow-xl border-2 border-gray-100 py-10 px-8"
    >
      <h1 className="text-xl font-semibold">Welcome to Notoria</h1>

      <InputField
        label="Full name:"
        type="text"
        id="name"
        register={register}
        error={errors.name}
        placeholder="e.g. Hpone Tauk Nyi"
      />
      <InputField
        label="Email:"
        type="email"
        id="email"
        register={register}
        error={errors.email}
        placeholder="e.g. hponetaukyou@gmail.com"
      />
      <InputField
        label="Password:"
        type="password"
        id="password"
        register={register}
        error={errors.password}
        placeholder="e.g. sigmaTauk007#!@"
      />

      <Button additionalStyling="w-full my-3" primary={true} type="submit">
        Sign Up
      </Button>

      <div className="flex items-center w-full py-2">
        <div className="flex-1 h-[0.5px] bg-gray-500" />
        <span className="px-3 text-gray-600 text-sm">OR</span>
        <div className="flex-1 h-[0.5px] bg-gray-500" />
      </div>

      <Button primary={false} type="button" additionalStyling="w-full my-3">
        <i className="fa-brands fa-google mx-3"></i>
        <span>Sign up with google</span>
      </Button>

      <p className="text-blue-950 text-sm">
        Already have an account?{" "}
        <span className="font-semibold hover:underline hover:text-orange-600 cursor-pointer">
          Log in
        </span>
      </p>
    </form>
  );
};

export default UserRegistration;
