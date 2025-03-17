// import { useSelector } from "react-redux";
// import { RootState } from "../Redux/store";

import Button from "./Button";

const Controls = () => {
  // const { isMobile } = useSelector((state: RootState) => state.currentScreen);

  return (
    <div className="mt-auto min-w-full flex justify-end bg-white gap-3 px-3 py-5 border-t border-gray-300 shadow">
      <Button primary={false}>
        <i className="fa-solid fa-trash mr-2"></i>
        Discard
      </Button>
      <Button primary={true}>
        <i className="fa-solid fa-save mr-2"></i>
        Save changes
      </Button>
    </div>
  );
};

export default Controls;
