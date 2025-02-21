import { useLocation } from "react-router-dom";

const Dashboard = () => {
  const location = useLocation();
  const data = location.state || {};

  console.log("Dashboard received data:", data);

  return (
    <div>
      <h1>Welcome, {data.user?.name || "Guest"}!</h1>
    </div>
  );
};

export default Dashboard;
