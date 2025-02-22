import { useDispatch, useSelector } from "react-redux";
import NotFound from "./Pages/NotFound";
import UserLogin from "./Pages/UserLogin";
import UserRegistration from "./Pages/UserRegistration";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import { RootState } from "./Redux/store";
import Dashboard from "./Pages/Dashboard";
import { checkAuthentication } from "./Services/fetchUser";
import { setIsLoggedIn } from "./Redux/slices/logInStatusSlice";
import { useEffect } from "react";

const App = () => {
  const { loading } = useSelector((state: RootState) => state.ui);
  const { isLoggedIn } = useSelector((state: RootState) => state.loginStatus);
  const dispatch = useDispatch();

  useEffect(() => {
    const checkAuthStatus = async () => {
      const isValid = await checkAuthentication();
      dispatch(setIsLoggedIn(isValid));
    };

    checkAuthStatus();
  }, [dispatch]);

  return (
    <Router>
      {loading ? (
        <section className="w-screen h-screen grid items-center justify-center">
          Loading...
        </section>
      ) : (
        <section className="w-screen h-screen grid items-center justify-center">
          <Routes>
            <Route path="/registration" element={<UserRegistration />} />
            <Route path="/login" element={<UserLogin />} />
            <Route path="*" element={<NotFound />} />
            <Route
              path="/"
              element={isLoggedIn ? <Dashboard /> : <Navigate to="/login" />}
            />
          </Routes>
        </section>
      )}
    </Router>
  );
};

export default App;
