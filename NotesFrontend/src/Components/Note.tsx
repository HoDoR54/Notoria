import { useSelector } from "react-redux";
import { RootState } from "../Redux/store";

const Note = () => {
  const { isMobile } = useSelector((state: RootState) => state.currentScreen);
  return (
    <section
      className={`bg-amber-300 h-full p-5 ${
        isMobile ? "flex-1" : "col-span-8"
      }`}
    >
      Note
    </section>
  );
};

export default Note;
