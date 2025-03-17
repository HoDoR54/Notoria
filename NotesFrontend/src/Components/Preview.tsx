import { useSelector } from "react-redux";
import { RootState } from "../Redux/store";
import Button from "./Button";
import SideBarBlock from "./SideBarBlock";
import PreviewItem from "./PreviewItem";

const Preview = () => {
  const { isMobile } = useSelector((state: RootState) => state.currentScreen);
  const { currentLanguage } = useSelector((state: RootState) => state.language);

  return (
    <section
      className={`shadow flex flex-col relative ${
        isMobile
          ? "flex-1 overflow-y-scroll"
          : "col-span-3 z-30 border-r-1 border-gray-300 overflow-y-auto"
      }`}
    >
      <div className={`w-full flex px-3 py-5 ${isMobile ? "hidden" : ""}`}>
        <Button primary={true} additionalStyling={isMobile ? "" : "w-full"}>
          {currentLanguage === "E" ? "Create a new note" : "မှတ်စုအသစ် ယူမယ်"}
        </Button>
      </div>
      <SideBarBlock
        title={currentLanguage === "E" ? "all notes" : "မှတ်စုအားလုံး"}
      >
        <PreviewItem></PreviewItem>
        <PreviewItem></PreviewItem>
        <PreviewItem></PreviewItem>
      </SideBarBlock>
      {isMobile && (
        <div className="fixed bg-white bottom-3 right-3 shadow border-2 border-gray-200 rounded-full w-[4rem] h-[4rem] flex justify-center items-center">
          <i className="fa-solid fa-plus"></i>
        </div>
      )}
    </section>
  );
};

export default Preview;
