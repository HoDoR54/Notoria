import { useSelector } from "react-redux";
import Button from "../Reusable/Button";
import SideBar from "../Reusable/SideBar";
import SideBarBlock from "../Reusable/SideBarBlock";
import { RootState } from "../../Redux/store";
import Preview from "./Preview";

const Notes = () => {
  const { currentLanguage } = useSelector((state: RootState) => state.language);
  return (
    <SideBar
      minWidth={300}
      maxWidth={400}
      // minHeight={window.innerHeight}
      borderSide="r"
    >
      <SideBarBlock border={true}>
        <Button
          primary={true}
          additionalStyling="w-full mb-3"
          icon="fa-solid fa-plus"
        >
          {currentLanguage === "E" ? "Add new" : "အသစ်ထည့်မယ်"}
        </Button>
      </SideBarBlock>

      <Preview />
    </SideBar>
  );
};

export default Notes;
