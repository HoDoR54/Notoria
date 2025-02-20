import { RegistrationRequest } from "../Types/UserFormTypes";
import axios from "axios";
import { yupResolver } from "@hookform/resolvers/yup";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { regisReqValidationSchema } from "../Services/formAuth";
import PrimaryButton from "../Components/PrimaryButton";
import { UserResponse } from "../Types/UserFormTypes";

const UserRegistration = () => {
  const [currentUser, setCurrentUser] = useState<UserResponse>();
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<RegistrationRequest>({
    resolver: yupResolver(regisReqValidationSchema),
  });

  const onSubmit = async (data: RegistrationRequest) => {
    const fetchedData = await fetchData(data.name, data.email, data.password);
    console.log("Response Data:", fetchedData);
    setCurrentUser(fetchedData);
  };

  const fetchData = async (
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
      className="flex flex-col items-center justify-center gap-3"
    >
      <h1 className="text-xl font-semibold">Create an account</h1>

      {/* name field */}
      <div className="flex flex-col gap-1">
        <label htmlFor="name" className="text-base font-thin">
          Full name:
        </label>
        <input
          type="text"
          id="name"
          placeholder="e.g. Hpone Tauk Nyi"
          {...register("name")}
          className="border-1 rounded-md pr-2 py-1 pl-3 min-w-[300px]"
        />
        {errors.name && <p className="text-red-500">{errors.name.message}</p>}
      </div>

      {/* email field */}
      <div className="flex flex-col gap-1">
        <label htmlFor="email" className="text-base font-thin">
          Email:
        </label>
        <input
          type="email"
          id="email"
          placeholder="e.g. Hpone Tauk Nyi"
          {...register("email")}
          className="border-1 rounded-md pr-2 py-1 pl-3 min-w-[300px]"
        />
        {errors.email && <p className="text-red-500">{errors.email.message}</p>}
      </div>

      {/* password field */}
      <div className="flex flex-col gap-1">
        <label htmlFor="password" className="text-base font-thin">
          Password:
        </label>
        <input
          id="password"
          type="password"
          placeholder="e.g. sigmaTauk007#!@"
          {...register("password")}
          className="border-1 rounded-md pr-2 py-1 pl-3 min-w-[300px]"
        />
        {errors.password && (
          <p className="text-red-500">{errors.password.message}</p>
        )}
      </div>

      <div className="flex items-center justify-end w-full">
        <PrimaryButton text="Sign up" />
      </div>
    </form>
  );
};

export default UserRegistration;
