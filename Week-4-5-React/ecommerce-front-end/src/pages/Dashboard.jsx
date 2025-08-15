import { useAuth } from "../context/AuthContext";

// This component represents the dashboard page
// It displays a welcome message to the user and their role
// It also provides a logout button that allows the user to log out of the application
export const Dashboard = () => {
  
  const { user, logout } = useAuth(); // Get auth state and logout function

  return (
    <div className="dashboard">
      <h1>Dashboard</h1>
      <p>Welcome {user.name}!</p>
      {/* Display role from decoded JWT claims */}
      {user.role && <p>Your role: {user.role}</p>}
      <button onClick={logout}>Logout</button>
    </div>
  );
};
