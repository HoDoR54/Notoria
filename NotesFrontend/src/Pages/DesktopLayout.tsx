import Header from "../Components/Header";
import LeftBar from "../Components/LeftBar";
import Preview from "../Components/Preview";
import Note from "../Components/Note";

const DesktopLayout = () => {
  return (
    <section className="min-h-screen min-w-screen flex flex-col">
      <Header />

      {/* main container */}
      <section className="flex-1 bg-gray-300 grid grid-cols-12">
        <LeftBar />
        <Preview />
        <Note />
      </section>
    </section>
  );
};

export default DesktopLayout;
