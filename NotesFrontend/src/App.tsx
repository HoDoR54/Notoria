import { useSelector } from "react-redux";
import NotFound from "./Pages/NotFound";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { RootState } from "./Redux/store";
import Dashboard from "./Pages/Dashboard";
import Home from "./Pages/Mobile/Home";
import Note from "./Pages/Mobile/Note";
import Settings from "./Pages/Mobile/Settings";
import Account from "./Pages/Mobile/Account";

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
      <section className="w-screen h-screen">
        <Routes>
          <Route path="*" element={<NotFound />} />
          <Route path="/" element={<Dashboard />}>
            <Route path="/" element={<Home />} />
            <Route path="/note" element={<Note />} />
            <Route path="/settings" element={<Settings />} />
            <Route path="/account" element={<Account />} />
          </Route>
        </Routes>
      </section>
    </Router>
  );
};

export default App;
