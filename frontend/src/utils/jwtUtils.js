import api from "../services/api";

// Validate token with backend
export const validateTokenWithBackend = async (token) => {
  try {
    const response = await api.post("/auth/validate", { token });
    return response.data;
  } catch (error) {
    console.error("Token validation failed:", error);
    return { isValid: false };
  }
};

// Get token from localStorage
export const getStoredToken = () => {
  return localStorage.getItem("token");
};

// Clear all auth data
export const clearAuthData = () => {
  localStorage.removeItem("token");
  localStorage.removeItem("user");
  localStorage.removeItem("tokenExpiry");
};
