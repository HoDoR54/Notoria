import { useSelector } from "react-redux";
import NotFound from "./Pages/NotFound";
import UserLogin from "./Pages/UserLogin";
import UserRegistration from "./Pages/UserRegistration";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { RootState } from "./Redux/store";
import Dashboard from "./Pages/Dashboard";
import ProtectedRoute from "./Utils/ProtectedRoute";

const App = () => {
  const { loading } = useSelector((state: RootState) => state.ui);

  if (loading) {
    return (
      <section className="w-screen h-screen grid items-center justify-center">
        Loading...
      </section>
    );
  }

  return (
    <Router>
      <section className="w-screen h-screen grid items-center justify-center">
        <Routes>
          <Route path="/registration" element={<UserRegistration />} />
          <Route path="/login" element={<UserLogin />} />
          <Route path="*" element={<NotFound />} />
          <Route
            path="/"
            element={
              <ProtectedRoute>
                <Dashboard />
              </ProtectedRoute>
            }
          />
        </Routes>
      </section>
    </Router>
  );
};

export default App;
