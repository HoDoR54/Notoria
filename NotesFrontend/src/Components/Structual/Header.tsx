import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../../Redux/store";
import { toggleLanguage } from "../../Redux/slices/languageSlice";

const Header = () => {
  const dispatch = useDispatch();
  const { currentLanguage } = useSelector((state: RootState) => state.language);

  return (
    <header
      id="header"
      className="flex min-w-screen px-3 py-5 justify-between shadow z-50 border-b-1 border-gray-300"
    >
      <h1 className="text-blue-950 text-2xl font-bold">Notoria</h1>
      <div className="flex gap-3 items-center justify-center">
        <div
          onClick={() => dispatch(toggleLanguage())}
          className="flex items-center justify-center cursor-pointer px-3 py-1 rounded-full border-2 border-blue-950 hover:bg-blue-100"
        >
          <i className="fa-solid fa-globe text-xl mr-3 text-blue-950"></i>
          <span>{currentLanguage === "E" ? "English" : "မြန်မာ"}</span>
        </div>
        <div className="flex items-center justify-center cursor-pointer">
          <i className="fa-solid fa-gear text-xl mr-3 text-blue-950"></i>
        </div>
      </div>
    </header>
  );
};

export default Header;
