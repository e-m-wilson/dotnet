import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { AuthProvider } from "./context/AuthContext";
import { ProtectedRoute } from "./components/common/ProtectedRoute";
import { Dashboard } from "./pages/Dashboard";
import { Unauthorized } from "./pages/Unauthorized";
import { LoginForm } from "./components/auth/LoginForm";
import { Navigation } from "./components/common/Navigation";

export default function App() {

  return (
    <AuthProvider>
      <Router>
        <Navigation />
        {/* Define routes here */}
        <Routes>
          {/* Public routes */}
          <Route path="/login" element={<LoginForm />} />
          <Route path="/unauthorized" element={<Unauthorized />} />
          {/* Protected route using higher order component pattern */}
          <Route
            path="/"
            element={
              <ProtectedRoute> {/* This is a protected route that requires authentication */}
                <Dashboard /> {/* This is the dashboard page that is only accessible to authenticated users */}
              </ProtectedRoute>
            }
          />
        </Routes>
      </Router>
    </AuthProvider>
  );
}
