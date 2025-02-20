import * as Yup from "yup";

export const regisReqValidationSchema = Yup.object({
  name: Yup.string().required("Name is required"),
  email: Yup.string()
    .email("Invalid email address")
    .required("Email is required"),
  password: Yup.string()
    .required("Password is required")
    .min(8, "Password must be at least 8 characters"),
  profilePicUrl: Yup.string().url("Invalid URL format").nullable(),
});
