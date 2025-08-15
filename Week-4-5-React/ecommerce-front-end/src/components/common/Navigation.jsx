import { Link } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";

// This component is responsible for rendering the navigation bar
// It uses the useAuth context to determine if the user is authenticated
// and conditionally renders the navigation links
// If the user is authenticated, it shows the Dashboard link and a Logout button
// If the user is not authenticated, it shows the Login link
// The Logout button calls the logout function from the useAuth context
export const Navigation = () => {

  // useAuth hook provides authentication state and methods
  const { isAuthenticated, logout } = useAuth();

  
  return (
    <nav>
      {isAuthenticated && (
        <>
          <Link to="/">Dashboard</Link>
          <button onClick={logout}>Logout</button>
        </>
      )}
      {!isAuthenticated && <Link to="/login">Login</Link>}
    </nav>
  );
};
