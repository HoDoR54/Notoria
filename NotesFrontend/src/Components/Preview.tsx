import { useSelector } from "react-redux";
import { RootState } from "../Redux/store";
import { Link } from "react-router-dom";

const Preview = () => {
  const { isMobile } = useSelector((state: RootState) => state.currentScreen);

  return (
    <section
      className={`bg-gray-400 h-full p-5 ${isMobile ? "flex-1" : "col-span-2"}`}
    >
      Preview
      <br />
      {isMobile && (
        <Link to={"/note"} className="hover:underline">
          To note
        </Link>
      )}
    </section>
  );
};

export default Preview;
