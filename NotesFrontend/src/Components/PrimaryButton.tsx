import React from "react";

interface PrimaryButtonProps {
  text: string;
  onClick?: () => void;
}

const PrimaryButton: React.FC<PrimaryButtonProps> = ({ text, onClick }) => {
  return (
    <button
      onClick={onClick}
      className="px-3 py-2 rounded-md bg-blue-400 cursor-pointer hover:brightness-90 active:brightness-100 text-white"
    >
      {text}
    </button>
  );
};

export default PrimaryButton;
