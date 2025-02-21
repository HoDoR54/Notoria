import { FormEventHandler, ReactNode } from "react";

interface FormWrapperProps {
  onSubmit: FormEventHandler<HTMLFormElement>;
  children: ReactNode;
  title: string;
}

const FormWrapper = ({ onSubmit, children, title }: FormWrapperProps) => {
  return (
    <form
      onSubmit={onSubmit}
      className="flex flex-col items-center justify-center gap-3 rounded-lg shadow-xl border-2 border-gray-100 py-10 px-8"
    >
      <h1 className="text-xl font-semibold">{title}</h1>
      {children}
    </form>
  );
};

export default FormWrapper;
