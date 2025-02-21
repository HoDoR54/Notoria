import React from "react";

interface ButtonProps {
  children: React.ReactNode; // Accepts anything React can render
  additionalStyling?: string;
  primary: boolean;
  type?: "button" | "submit" | "reset";
}

const Button: React.FC<ButtonProps> = ({
  children,
  additionalStyling,
  primary,
  type,
}) => {
  return (
    <button
      type={type}
      className={`px-4 py-2 rounded-md hover:brightness-90 active:brightness-100 cursor-pointer ${
        primary
          ? "bg-blue-950 text-white"
          : "bg-gray-200 text-blue-950 border-1"
      } ${additionalStyling}`}
    >
      {children}
    </button>
  );
};

export default Button;
