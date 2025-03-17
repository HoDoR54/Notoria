import { useSelector } from "react-redux";
import NotFound from "./Pages/NotFound";
import UserLogin from "./Pages/UserLogin";
import UserRegistration from "./Pages/UserRegistration";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { RootState } from "./Redux/store";
import Dashboard from "./Pages/Dashboard";
import Note from "./Components/Note";
import MobileHome from "./Components/MobileHome";

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
          <Route path="/" element={<Dashboard />}>
            <Route path="note" element={<Note />} />
            <Route path="/" element={<MobileHome />} />
          </Route>
        </Routes>
      </section>
    </Router>
  );
};

export default App;
