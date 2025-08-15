import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";

// LoginForm component for user authentication
// This component handles user login and redirects to the dashboard upon successful login
// It uses the useAuth context to access authentication methods and state
// It also uses the useNavigate hook from react-router-dom for navigation
// The component maintains local state for user credentials and error messages
// It includes a form with email and password fields, and a submit button
// The form submission is handled by the handleSubmit function, which calls the login method from the useAuth context
// The handleSubmit function prevents the default form submission behavior, resets any previous error messages, and attempts to log in the user with the provided credentials
// If the login is successful, the user is redirected to the dashboard
// If the login fails, an error message is displayed
export const LoginForm = () => {

  // useAuth hook provides authentication methods and state
  const { login } = useAuth();
  
  // useNavigate hook from react-router-dom for navigation
  const navigate = useNavigate();

  // Local state for error messages and user credentials
  const [error, setError] = useState("");

  // Local state for user credentials
  const [credentials, setCredentials] = useState({
    email: "",
    password: "",
  });

  // handleSubmit function to manage form submission
  // It prevents the default form submission behavior, resets any previous error messages,
  // and attempts to log in the user with the provided credentials
  const handleSubmit = async (e) => {

    // Prevent default form submission
    e.preventDefault();

    // Reset error message
    setError("");

    // Attempt to log in the user with the provided credentials
    try {
      const success = await login(credentials);
      if (success) {
        navigate("/");
      }
    } catch (error) {
      setError("Invalid credentials. Please try again.");
    }
  };


  return (
    <div className="container">
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label>Email:</label>
          <input
            type="email"
            value={credentials.email}
            onChange={(e) => 
              setCredentials({ ...credentials, email: e.target.value })
            }
            required
          />
        </div>
        <div className="form-group">
          <label>Password:</label>
          <input
            type="password"
            value={credentials.password}
            onChange={(e) =>
              setCredentials({ ...credentials, password: e.target.value })
            }
            required
          />
        </div>
        {error && <div className="error">{error}</div>}
        <button type="submit">Login</button>
      </form>
    </div>
  );
};
