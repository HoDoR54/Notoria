import { useSelector } from "react-redux";
import { RootState } from "../Redux/store";
import Controls from "./Controls";

const Note = () => {
  const isEmpty = true;
  const { isMobile } = useSelector((state: RootState) => state.currentScreen);
  return (
    <section
      className={`h-full bg-white flex flex-col ${
        isMobile ? "flex-1" : "col-span-7 z-20"
      }`}
    >
      {isEmpty ? (
        <div className="w-full h-full flex flex-col items-center justify-center">
          <span className="text-gray-400 font-bold text-3xl">
            WELCOME TO NOTORIA
          </span>
          <span className="text-gray-400 text-sm">
            Designed and developed by Hpone Tauk Nyi
          </span>
          <span className="text-gray-400 text-sm">hponetaukyou@gmail.com</span>
        </div>
      ) : (
        <textarea className="w-full h-full p-5 focus:outline-none resize-none"></textarea>
      )}
      <Controls />
    </section>
  );
};

export default Note;
