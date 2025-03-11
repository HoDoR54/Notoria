import FormWrapper from "../Components/FormWrapper";
import { LoginRequest } from "../Types/userFormTypes";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { loginReqValidationSchema } from "../Services/formAuth";
import InputField from "../Components/InputField";
import { useDispatch } from "react-redux";
import { fetchLoginData } from "../Services/fetchAuth";
import Button from "../Components/Button";
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";

const UserLogin = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<LoginRequest>({
    resolver: yupResolver(loginReqValidationSchema),
  });

  const onSubmit = async (data: LoginRequest) => {
    const fetchedData = await fetchLoginData(
      dispatch,
      data.email,
      data.password
    );
    console.log("Logged In...");

    reset();
    navigate("/NotFound");
    fetchedData !== false && console.log("Routed to dashboard");
  };

  return (
    <FormWrapper
      onSubmit={handleSubmit(onSubmit)}
      title="Log in to your account"
    >
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

      <Button primary={true} additionalStyling="w-full my-3">
        Log in
      </Button>

      <div className="flex items-center w-full py-2">
        <div className="flex-1 h-[0.5px] bg-gray-500" />
        <span className="px-3 text-gray-600 text-sm">OR</span>
        <div className="flex-1 h-[0.5px] bg-gray-500" />
      </div>

      <Button primary={false} additionalStyling="w-full my-3">
        <i className="fa-brands fa-google mx-3"></i>
        <span>Sign up with google (not yet)</span>
      </Button>

      <p className="text-blue-950 text-sm">
        Don't have an account?{" "}
        <Link to={"/registration"}>
          <span className="font-semibold hover:underline hover:text-orange-600 cursor-pointer">
            Sign up
          </span>
        </Link>
      </p>
    </FormWrapper>
  );
};

export default UserLogin;
