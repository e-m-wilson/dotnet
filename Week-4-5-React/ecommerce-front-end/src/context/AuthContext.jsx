import { createContext, useContext, useReducer, useEffect } from "react";

// Importing the jwtDecode function from the jwt-decode library
// This function is used to decode the JWT token and extract the claims
// https://www.npmjs.com/package/jwt-decode
import { jwtDecode } from "jwt-decode";
import { api } from "../services/api"; 


const AuthContext = createContext();

// This function normalizes the claims from the decoded JWT token
// It extracts the email, id, role, and name from the decoded token
// and returns them in a standardized format

// Below is an example of the decoded JWT token provided by ASP.NET Core Identity
/*
  {
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name": "pompompurin@example.com",
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier": "043f6cd9-17f3-401d-9f9c-80509d791d7d",
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress": "pompompurin@example.com",
    "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": "Admin",
    "exp": 1747142991,
    "iss": "localhost",
    "aud": "*"
  }
*/
const normalizeClaims = (decoded) => ({
  email:
    decoded[
      "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
    ],
  id: decoded[
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
  ],
  role: decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
  name: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
});


// This function is the reducer for the authentication state
const authReducer = (state, action) => {
  switch (action.type) {
    case "LOGIN":
      return { ...state, user: action.payload, isAuthenticated: true };
    case "LOGOUT":
      return { ...state, user: null, isAuthenticated: false };
    default:
      return state;
  }
};

//
export const AuthProvider = ({ children }) => {
  
  const [state, dispatch] = useReducer(authReducer, {
    user: null,
    isAuthenticated: false,
  });
  
  // This function checks if the JWT token is valid
  // It checks if the token exists in local storage and if it is not expired
  const checkTokenValidity = () => {
    
    // Check if the token exists in local storage
    const token = localStorage.getItem("jwt");
    if (!token) return false;
    
    // Decode the token and check if it is expired
    try {
      const decoded = jwtDecode(token);
      return decoded.exp * 1000 > Date.now();
    } catch (error) {
      return false;
    }
  };

  // This function logs in the user
  // It sends a POST request to the server with the user's credentials
  // If the login is successful, it stores the JWT token in local storage
  // and updates the authentication state with the user's information
  const login = async (credentials) => {
    const response = await api.post("/auth/login", credentials);
    const token = response.data.token;
    localStorage.setItem("jwt", token);
    const decoded = jwtDecode(token);
    dispatch({ type: "LOGIN", payload: normalizeClaims(decoded) });
    return true;
  };

  // This function logs out the user
  // It removes the JWT token from local storage and updates the authentication state
  // to indicate that the user is no longer authenticated
  // It also clears the user information from the state
  // and sets the isAuthenticated flag to false
  const logout = () => {
    localStorage.removeItem("jwt");
    dispatch({ type: "LOGOUT" });
  };

  // This useEffect hook checks if the token is valid when the component mounts
  // If the token is valid, it decodes the token and updates the authentication state
  // with the user's information
  // It also sets the isAuthenticated flag to true
  // This ensures that the user remains logged in even after refreshing the page
  useEffect(() => {
    if (checkTokenValidity()) {
      const token = localStorage.getItem("jwt");
      const decoded = jwtDecode(token);
      dispatch({ type: "LOGIN", payload: normalizeClaims(decoded) });
    }
  }, []);


  return (
    <AuthContext.Provider
      value={{ ...state, login, logout, checkTokenValidity }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
