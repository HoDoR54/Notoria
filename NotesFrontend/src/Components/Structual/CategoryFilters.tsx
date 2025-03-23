import SideBarItem from "../Reusable/SideBarItem";
import SideBarBlock from "../Reusable/SideBarBlock";
import { useSelector } from "react-redux";
import { RootState } from "../../Redux/store";

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
        icon="fa-solid fa-house"
      />
      <SideBarItem
        text={currentLanguage === "E" ? "Archived" : "သိမ်းဆည်းမှတ်စုများ"}
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
