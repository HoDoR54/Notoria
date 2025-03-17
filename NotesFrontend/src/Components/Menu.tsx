import { useSelector } from "react-redux";
import { RootState } from "../Redux/store";
import SideBarBlock from "./SideBarBlock";
import SideBarItem from "./SideBarItem";

const Menu = () => {
  const { isMobile } = useSelector((state: RootState) => state.currentScreen);
  const { currentLanguage } = useSelector((state: RootState) => state.language);

  return (
    <section
      className={`h-full ${
        isMobile
          ? "flex-1"
          : "col-span-2 shadow z-40 border-r-1 border-gray-300"
      }`}
    >
      {/* previews */}
      <SideBarBlock>
        <SideBarItem
          icon="fa-solid fa-house"
          text={currentLanguage === "E" ? "All notes" : "မှတ်စုအားလုံး"}
          onClick={() => {}}
        />
        <SideBarItem
          icon="fa-solid fa-box-archive"
          text={
            currentLanguage === "E"
              ? "Archived notes"
              : "archive လုပ်ထားသော မှတ်စုများ"
          }
          onClick={() => {}}
        />
      </SideBarBlock>
      <SideBarBlock title={currentLanguage === "E" ? "tags" : "အမျိုးအစားများ"}>
        <SideBarItem icon="fa-solid fa-tag" text="Tag" onClick={() => {}} />
        <SideBarItem icon="fa-solid fa-tag" text="Tag" onClick={() => {}} />
        <SideBarItem icon="fa-solid fa-tag" text="Tag" onClick={() => {}} />
      </SideBarBlock>
    </section>
  );
};

export default Menu;
