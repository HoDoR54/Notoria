import { Outlet } from "react-router-dom";

const MobileLayout = () => {
  return (
    <section className="min-w-screen min-h-screen">
      <Outlet />
    </section>
  );
};

export default MobileLayout;
