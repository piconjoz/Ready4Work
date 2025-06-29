import React, { createContext, useContext, useState, useEffect } from "react";
import {
  validateTokenWithBackend,
  getStoredToken,
  clearAuthData,
} from "../utils/jwtUtils";
import { login as loginAPI, logout as logoutAPI } from "../services/authAPI";

const AuthContext = createContext();

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const [token, setToken] = useState(null);

  // Initialize auth state on app startup
  useEffect(() => {
    initializeAuth();
  }, []);

  const initializeAuth = async () => {
    setIsLoading(true);

    const storedToken = getStoredToken();
    if (!storedToken) {
      setIsLoading(false);
      return;
    }

    try {
      const validationResult = await validateTokenWithBackend(storedToken);

      if (validationResult.isValid) {
        setUser({
          userId: validationResult.userId,
          userType: validationResult.userType,
          email: validationResult.email,
          firstName: validationResult.firstName,
          lastName: validationResult.lastName,
          isActive: validationResult.isActive,
          isVerified: validationResult.isVerified,
        });
        setToken(storedToken);
        setIsAuthenticated(true);
      } else {
        // Token is invalid, clear everything
        clearAuth();
      }
    } catch (error) {
      console.error("Auth initialization failed:", error);
      clearAuth();
    } finally {
      setIsLoading(false);
    }
  };

  const login = async (loginData) => {
    setIsLoading(true);
    try {
      // Your existing login API call
      const response = await loginAPI(loginData);

      // Store token
      localStorage.setItem("token", response.token);
      localStorage.setItem("user", JSON.stringify(response.user));
      localStorage.setItem("tokenExpiry", response.expiresAt);

      // Update context state
      setUser(response.user);
      setToken(response.token);
      setIsAuthenticated(true);

      return response;
    } catch (error) {
      throw error;
    } finally {
      setIsLoading(false);
    }
  };

  const logout = async () => {
    try {
      // Call your logout API
      await logoutAPI();
    } catch (error) {
      console.error("Logout API failed:", error);
    } finally {
      clearAuth();
    }
  };

  const clearAuth = () => {
    setUser(null);
    setToken(null);
    setIsAuthenticated(false);
    clearAuthData();
  };

  // Validate current user's access to a specific userType
  const validateUserAccess = async (requiredUserType) => {
    if (!token) return { isValid: false, reason: "No token" };

    try {
      const validationResult = await validateTokenWithBackend(token);

      if (!validationResult.isValid) {
        return { isValid: false, reason: "Invalid token" };
      }

      if (validationResult.userType !== requiredUserType) {
        return {
          isValid: false,
          reason: "Insufficient permissions",
          actualUserType: validationResult.userType,
        };
      }

      // Update user info in case anything changed
      setUser({
        userId: validationResult.userId,
        userType: validationResult.userType,
        email: validationResult.email,
        firstName: validationResult.firstName,
        lastName: validationResult.lastName,
        isActive: validationResult.isActive,
        isVerified: validationResult.isVerified,
      });

      return { isValid: true, user: validationResult };
    } catch (error) {
      console.error("Access validation failed:", error);
      return { isValid: false, reason: "Validation error" };
    }
  };

  const value = {
    user,
    isAuthenticated,
    isLoading,
    token,
    login,
    logout,
    clearAuth,
    validateUserAccess,
    initializeAuth,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};
