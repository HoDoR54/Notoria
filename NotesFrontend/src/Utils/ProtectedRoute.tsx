import { useNavigate } from "react-router-dom";
import { ReactNode, useEffect, useState, useCallback } from "react";
import { authenticateUser } from "../Services/fetchAuth";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../Redux/store";

interface ProtectedRouteProps {
  children: ReactNode;
}

const ProtectedRoute = ({ children }: ProtectedRouteProps) => {
  console.log("Protected route");
  const [isAuthenticated, setIsAuthenticated] = useState<boolean | null>(null);
  const { currentUser } = useSelector((state: RootState) => state.currentUser);
  console.log(currentUser);
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const verifyAuth = useCallback(async () => {
    console.log("Authenticating");
    const isVerified = await authenticateUser(dispatch);
    setIsAuthenticated(isVerified);
  }, [dispatch]);

  useEffect(() => {
    verifyAuth();
    console.log("Authenticated");
  }, [verifyAuth]);

  // Redirect to login if not authenticated
  useEffect(() => {
    if (isAuthenticated === false) {
      console.log("Redirected to login");
      navigate("/login");
    }
  }, [isAuthenticated, navigate]);

  if (isAuthenticated === null) {
    return <p>Loading...</p>;
  }

  return isAuthenticated ? <>{children}</> : null;
};

export default ProtectedRoute;
