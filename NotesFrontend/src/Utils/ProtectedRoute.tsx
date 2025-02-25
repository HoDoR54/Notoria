import { useNavigate } from "react-router-dom";
import { ReactNode, useEffect, useState } from "react";
import { authenticateUser } from "../Services/fetchAuth";
import { useDispatch } from "react-redux";
import { setCurrentUser } from "../Redux/slices/currentUserSlice";

interface ProtectedRouteProps {
  children: ReactNode;
}

const ProtectedRoute = ({ children }: ProtectedRouteProps) => {
  const [isAuthenticated, setIsAuthenticated] = useState<boolean | null>(null);
  const navigate = useNavigate();
  const dispatch = useDispatch();

  useEffect(() => {
    const verifyAuth = async () => {
      const verification = await authenticateUser(dispatch);
      const isVerified = verification.valid;
      dispatch(setCurrentUser(verification.user));
      setIsAuthenticated(isVerified);

      if (!isVerified) {
        navigate("/login");
      }
    };

    verifyAuth();
  }, [navigate]);

  if (isAuthenticated === null) {
    return <p>Loading...</p>;
  }

  return isAuthenticated ? <>{children}</> : null;
};

export default ProtectedRoute;
