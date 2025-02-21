import * as Yup from "yup";

export const regisReqValidationSchema = Yup.object({
  name: Yup.string().required("Name is required"),
  email: Yup.string()
    .email("Invalid email address")
    .required("an email is required"),
  password: Yup.string()
    .required("A password is required")
    .min(8, "Password must be at least 8 characters")
    .matches(/[0-9]/, "Password must contain at least one number")
    .matches(/[\W_]/, "Password must contain at least one special character")
    .notOneOf(["password", "12345678", "qwerty"], "Password is too common"),
  profilePicUrl: Yup.string().url("Invalid URL format").nullable(),
});

export const loginReqValidationSchema = Yup.object({
  email: Yup.string().required("An email is required"),
  password: Yup.string().required("A password is required"),
});
