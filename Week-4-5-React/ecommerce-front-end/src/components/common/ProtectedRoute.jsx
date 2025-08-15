import { Navigate, useLocation } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";

// This is a higher-order component that wraps around the children components
// It takes in the children components and the required roles as props
// If the user is authenticated and has the required role, it renders the children components
// If the user is not authenticated, it redirects them to the login page
export const ProtectedRoute = ({ children, roles }) => {

  // useAuth is a custom hook that provides the authentication state and user information
  const { isAuthenticated, user } = useAuth();

  // useLocation is a hook that returns the current location object
  // The location object contains information about the current URL
  // We use this to redirect the user back to the page they were trying to access after logging in
  const location = useLocation();

  // If the user is not authenticated, redirect them to the login page
  if (!isAuthenticated) {
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  // If the user is authenticated but does not have the required role, redirect them to the unauthorized page
  // In this specific case, we check if the user has a role and if it is included in the roles array
  if (roles && !roles.includes(user?.role)) {
    return <Navigate to="/unauthorized" replace />;
  }

  // If the user is authenticated and has the required role, render the children components
  return children;
};
