import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../Redux/store";
import { useEffect } from "react";
import { setCurrentSize } from "../Redux/slices/currentScreen";
import MobileLayout from "./MobileLayout";
import DesktopLayout from "./DesktopLayout";

const Dashboard = () => {
  const dispatch = useDispatch();
  const { isMobile } = useSelector((state: RootState) => state.currentScreen);

  useEffect(() => {
    const handleResize = () => {
      dispatch(setCurrentSize(window.innerWidth < 1024));
    };

    handleResize();
    window.addEventListener("resize", handleResize);
    return () => window.removeEventListener("resize", handleResize);
  }, [dispatch]);

  return isMobile ? <MobileLayout /> : <DesktopLayout />;
};

export default Dashboard;
