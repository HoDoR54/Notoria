import React from "react";

interface InputFieldProps {
  placeholder?: string;
  label?: string;
}

const InputField: React.FC<InputFieldProps> = ({ placeholder, label }) => {
  return (
    <div className="relative flex max-w-fit rounded-full">
      <input
        type="text"
        placeholder={placeholder}
        name={label}
        className="pl-10 py-1 outline-1 box-border min-w-[250px] rounded-full"
      ></input>

      <div className="flex items-center justify-center px-2 pt-0.5 absolute left-0 top-0 bottom-0">
        <i className="fa-solid fa-search text-gray-600"></i>
      </div>
    </div>
  );
};

export default InputField;
