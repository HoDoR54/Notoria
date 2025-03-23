import CurrentNote from "../Components/CurrentNote";
import Header from "../Components/Header";
import Menu from "../Components/Menu";
import Notes from "../Components/Notes";

const DesktopLayout = () => {
  return (
    <section className="h-screen w-screen flex flex-col">
      <Header />

      {/* main container */}
      <section className="flex-1 flex overflow-hidden">
        <Menu />
        <Notes />
        <CurrentNote />
      </section>
    </section>
  );
};

export default DesktopLayout;
