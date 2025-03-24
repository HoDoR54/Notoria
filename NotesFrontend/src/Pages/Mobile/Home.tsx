import { useNavigate } from "react-router-dom";
import Header from "../../Components/Structual/Header";
import Preview from "../../Components/Structual/Preview";
import React, { ReactNode } from "react";

const Home = () => {
  return (
    <section className="w-full h-full flex flex-col">
      <Header />
      <Preview />

      <ul className="flex gap-3 w-full items-center justify-center py-3">
        <TestingLinks route="/note">Notes</TestingLinks>
        <TestingLinks route="/settings">Settings</TestingLinks>
        <TestingLinks route="/account">Account</TestingLinks>
      </ul>
    </section>
  );
};

interface TestingLinksProps {
  children: ReactNode;
  route: string;
}

const TestingLinks: React.FC<TestingLinksProps> = ({ children, route }) => {
  const navigate = useNavigate();

  return (
    <li
      className="py-2 px-4 border border-gray-400 rounded-full hover:bg-amber-100"
      onClick={() => navigate(route)}
    >
      {children}
    </li>
  );
};

export default Home;
