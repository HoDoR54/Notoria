import { useSelector } from "react-redux";
import Button from "./Button";
import { RootState } from "../Redux/store";

const Controls = () => {
  const { currentLanguage } = useSelector((state: RootState) => state.language);
  return (
    <div className="py-3 px-5 flex justify-between shadow border-t border-gray-300">
      <div className="flex gap-2">
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
          icon="fa-solid fa-upload"
        >
          {currentLanguage === "E" ? "Save" : "သိမ်းမယ်"}
        </Button>
      </div>
      <div>
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
