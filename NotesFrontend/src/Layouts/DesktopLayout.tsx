import CurrentNote from "../Components/Structual/CurrentNote";
import Header from "../Components/Structual/Header";
import Menu from "../Components/Structual/Menu";
import Notes from "../Components/Structual/Notes";

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
