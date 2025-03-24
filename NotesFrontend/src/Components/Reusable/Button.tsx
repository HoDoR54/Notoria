import React from "react";

interface ButtonProps {
  children?: React.ReactNode;
  additionalStyling?: string;
  primary: boolean;
  icon?: string;
  type?: "button" | "submit" | "reset";
}

const Button: React.FC<ButtonProps> = ({
  children,
  additionalStyling,
  primary,
  icon,
  type,
}) => {
  return (
    <button
      type={type}
      className={`px-4 py-2 box-border text-sm rounded-md hover:brightness-90 active:brightness-100 cursor-pointer ${
        primary
          ? "bg-blue-950 text-white"
          : "bg-gray-200 text-blue-950 border-1"
      } ${additionalStyling}`}
    >
      {icon && <i className={`${icon} ${children && "mr-2"}`}></i>}
      {children}
    </button>
  );
};

export default Button;
