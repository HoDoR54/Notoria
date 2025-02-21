import { RegistrationRequest, UserResponse } from "../Types/userFormTypes";
import { yupResolver } from "@hookform/resolvers/yup";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { regisReqValidationSchema } from "../Services/formAuth";
import Button from "../Components/Button";
import InputField from "../Components/InputField";
import { Link, useNavigate } from "react-router-dom";
import { fetchRegistrationData } from "../Services/fetchUser";
import { useDispatch } from "react-redux";
import FormWrapper from "../Components/FormWrapper";
import { goToRoute } from "../Utils/routingUtils";

const UserRegistration = () => {
  const [_, setCurrentUser] = useState<UserResponse | null>(null);
  const dispatch = useDispatch();
  const navigate = useNavigate();

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
      dispatch,
      data.name,
      data.email,
      data.password
    );

    if (fetchedData.status !== "Failed") {
      setCurrentUser(fetchedData);
      reset();
      goToRoute("/dashboard", { user: fetchedData }, navigate);
    }
  };

  return (
    <FormWrapper onSubmit={handleSubmit(onSubmit)} title="Welcome to Notoria">
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
        <span>Sign up with google (not yet)</span>
      </Button>

      <p className="text-blue-950 text-sm">
        Already have an account?{" "}
        <Link to={"/login"}>
          <span className="font-semibold hover:underline hover:text-orange-600 cursor-pointer">
            Log in
          </span>
        </Link>
      </p>
    </FormWrapper>
  );
};

export default UserRegistration;
