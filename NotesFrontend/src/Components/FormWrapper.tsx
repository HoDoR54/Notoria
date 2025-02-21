import { FormEventHandler, ReactNode } from "react";

interface FormWrapperProps {
  onSubmit: FormEventHandler<HTMLFormElement>;
  children: ReactNode;
}

const FormWrapper = ({ onSubmit, children }: FormWrapperProps) => {
  return (
    <form
      onSubmit={onSubmit}
      className="flex flex-col items-center justify-center gap-3 rounded-lg shadow-xl border-2 border-gray-100 py-10 px-8"
    >
      {children}
    </form>
  );
};

export default FormWrapper;
