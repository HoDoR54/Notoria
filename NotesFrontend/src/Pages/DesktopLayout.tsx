import Header from "../Components/Header";
import Menu from "../Components/Menu";
import Preview from "../Components/Preview";
import Note from "../Components/Note";

const DesktopLayout = () => {
  return (
    <section className="min-h-screen min-w-screen flex flex-col">
      <Header />

      {/* main container */}
      <section className="flex-1 grid grid-cols-12">
        <Menu />
        <Preview />
        <Note />
      </section>
    </section>
  );
};

export default DesktopLayout;
