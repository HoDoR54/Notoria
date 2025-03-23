import CategoryFilters from "./CategoryFilters";
import SideBar from "../Reusable/SideBar";
import TagFilters from "./TagFilters";

const Menu = () => {
  return (
    <SideBar minWidth={200} maxWidth={300} borderSide="r">
      <CategoryFilters />
      <TagFilters />
    </SideBar>
  );
};

export default Menu;
