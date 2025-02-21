import React from "react";
import { UseFormRegister, FieldError } from "react-hook-form";
import { RegistrationRequest } from "../Types/userFormTypes";

interface InputFieldProps {
  label: string;
  type: string;
  id: keyof RegistrationRequest;
  register: UseFormRegister<any>;
  error?: FieldError;
  placeholder: string;
}

const InputField: React.FC<InputFieldProps> = ({
  label,
  type,
  id,
  register,
  error,
  placeholder,
}) => {
  return (
    <div className="flex flex-col gap-1">
      <label htmlFor={id} className="text-base font-thin">
        {label}
      </label>
      <input
        type={type}
        id={id}
        placeholder={placeholder}
        {...register(id)}
        className="border-1 rounded-md px-3 py-2 pl-3 min-w-[300px]"
      />
      {error && <p className="text-red-500">{error.message}</p>}
    </div>
  );
};

export default InputField;
