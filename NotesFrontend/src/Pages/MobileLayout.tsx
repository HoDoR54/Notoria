import { Routes, Route } from "react-router-dom";
import Header from "../Components/Header";
import Preview from "../Components/Preview";
import Note from "../Components/Note";

const MobileLayout = () => {
  return (
    <section className="min-w-screen min-h-screen flex flex-col">
      <Header />
      <Routes>
        <Route path="/" element={<Preview />} />
        <Route path="/note" element={<Note />} />
      </Routes>
    </section>
  );
};

export default MobileLayout;
