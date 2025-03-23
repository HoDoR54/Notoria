import { useSelector } from "react-redux";
import SideBarBlock from "./SideBarBlock";
import { RootState } from "../Redux/store";
import SideBarItem from "./SideBarItem";

const tags = ["tag1", "tag2", "tag3"];

const TagFilters = () => {
  const handleTagFilter = () => {};

  const { currentLanguage } = useSelector((state: RootState) => state.language);
  return (
    <SideBarBlock
      title={currentLanguage === "E" ? "Tags" : "တက်ဂ်များ"}
      border={true}
    >
      {tags.map((tag) => (
        <SideBarItem
          text={tag}
          key={tag}
          icon="fa-solid fa-hashtag"
          onClick={() => handleTagFilter()}
        />
      ))}
    </SideBarBlock>
  );
};

export default TagFilters;
