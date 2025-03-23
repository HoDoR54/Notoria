import { useSelector } from "react-redux";
import PreviewItem from "./PreviewItem";
import SideBarBlock from "./SideBarBlock";
import { RootState } from "../Redux/store";

const notes = ["note 1", "note 2", "note 3", "note 4", "note 5"];

const Preview = () => {
  const { currentLanguage } = useSelector((state: RootState) => state.language);
  return (
    <SideBarBlock
      title={`${currentLanguage === "E" ? "all notes" : "အားလုံး"}`}
      border={false}
    >
      {notes.map((note) => (
        <PreviewItem key={note} />
      ))}
      <div className="min-h-10" />
    </SideBarBlock>
  );
};

export default Preview;
