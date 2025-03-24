import { useSelector } from "react-redux";
import Button from "../Reusable/Button";
import { RootState } from "../../Redux/store";

const Controls = () => {
  const { currentLanguage } = useSelector((state: RootState) => state.language);
  const { isMobile } = useSelector((state: RootState) => state.currentScreen);

  return (
    <div
      className={`py-3 px-5 flex justify-between shadow border-t border-gray-300 ${
        isMobile && "flex-col gap-2"
      }`}
    >
      <div className={`flex gap-2 ${isMobile && "w-full justify-end"}`}>
        <Button
          primary={false}
          additionalStyling="px-5"
          icon="fa-solid fa-trash"
        >
          {currentLanguage === "E" ? "Delete" : "ဖျက်မယ်"}
        </Button>
        <Button
          primary={true}
          additionalStyling="px-5"
          icon="fa-regular fa-file"
        >
          {currentLanguage === "E" ? "Save" : "သိမ်းမယ်"}
        </Button>
      </div>
      <div className={`${isMobile && "hidden"}`}>
        <Button
          primary={false}
          additionalStyling="px-5"
          icon="fa-solid fa-download"
        >
          {currentLanguage === "E" ? "Download" : "ဒေါင်းလုဒ်ဆွဲမယ်"}
        </Button>
      </div>
    </div>
  );
};

export default Controls;
