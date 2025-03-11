import { useSelector } from "react-redux";
import { RootState } from "../Redux/store";

const LeftBar = () => {
  const { isMobile } = useSelector((state: RootState) => state.currentScreen);
  return (
    <section
      className={`bg-gray-500 h-full p-5 ${isMobile ? "flex-1" : "col-span-2"}`}
    >
      LeftBar
    </section>
  );
};

export default LeftBar;
