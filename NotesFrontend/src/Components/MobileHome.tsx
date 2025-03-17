import Header from "./Header";
import Preview from "./Preview";

const MobileHome = () => {
  return (
    <section className="flex flex-col min-h-screen overflow-y-scroll">
      <Header />
      <Preview />
    </section>
  );
};

export default MobileHome;
