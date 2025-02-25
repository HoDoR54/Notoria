import { useSelector } from "react-redux";
import { RootState } from "../Redux/store";

const Dashboard = () => {
  const { currentUser } = useSelector((state: RootState) => state.currentUser);

  console.log("Dashboard received data:", currentUser);

  return <h1>Welcome, {currentUser?.name || "Guest"}!</h1>;
};

export default Dashboard;
