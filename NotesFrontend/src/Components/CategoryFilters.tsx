import SideBarItem from "./SideBarItem";
import SideBarBlock from "./SideBarBlock";
import { useSelector } from "react-redux";
import { RootState } from "../Redux/store";

const CategoryFilters = () => {
  const { currentLanguage } = useSelector((state: RootState) => state.language);

  const handleFilter = () => {};

  return (
    <SideBarBlock
      title={currentLanguage === "E" ? "your notes" : "သင့်ရဲ့ မှတ်စုများ"}
      border={true}
    >
      <SideBarItem
        text={currentLanguage === "E" ? "All" : "အားလုံး"}
        onClick={() => handleFilter()}
        icon="fa-solid fa-note-sticky"
      />
      <SideBarItem
        text={currentLanguage === "E" ? "Archived" : "အထွေကြီးမှတ်စုများ"}
        onClick={() => handleFilter()}
        icon="fa-solid fa-archive"
      />
      <SideBarItem
        text={currentLanguage === "E" ? "Favorites" : "အကြိုက်ဆုံးများ"}
        onClick={() => handleFilter()}
        icon="fa-solid fa-star"
      />
    </SideBarBlock>
  );
};

export default CategoryFilters;
