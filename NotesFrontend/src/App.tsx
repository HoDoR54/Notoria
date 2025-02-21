import { useSelector } from "react-redux";
import Dashboard from "./Pages/Dashboard";
import NotFound from "./Pages/NotFound";
import UserLogin from "./Pages/UserLogin";
import UserRegistration from "./Pages/UserRegistration";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { RootState } from "./Redux/store";

const App = () => {
  const { loading } = useSelector((state: RootState) => state.ui);

  return (
    <Router>
      {loading ? (
        <section className="w-screen h-screen grid items-center justify-center">
          Loading...
        </section>
      ) : (
        <section className="w-screen h-screen grid items-center justify-center">
          <Routes>
            <Route path="/" element={<UserRegistration />} />
            <Route path="*" element={<NotFound />} />
            <Route path="/login" element={<UserLogin />} />
            <Route path="/dashboard" element={<Dashboard />} />
          </Routes>
        </section>
      )}
    </Router>
  );
};

export default App;
