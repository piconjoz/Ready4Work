import axios from "axios";
import { getToken, refreshToken, logout, needsRefresh } from "./authAPI";

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL || "http://127.0.0.1:5050/api",
  timeout: 10000,
  withCredentials: true,
});

// Request interceptor to add JWT token and handle refresh
api.interceptors.request.use(
  async (config) => {
    // Check if token needs refresh before making request
    if (needsRefresh()) {
      try {
        await refreshToken();
        console.log("Token refreshed automatically");
      } catch (error) {
        console.error("Auto-refresh failed:", error);
        // Don't logout here, let the response interceptor handle it
      }
    }

    // Add current token to request
    const token = getToken();
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Response interceptor to handle token expiration
api.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;

    // If we get 401 and haven't already tried to refresh
    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;

      try {
        // Try to refresh token
        await refreshToken();
        console.log("Token refreshed after 401");

        // Retry original request with new token
        const newToken = getToken();
        if (newToken) {
          originalRequest.headers.Authorization = `Bearer ${newToken}`;
          return api(originalRequest);
        }
      } catch (refreshError) {
        console.error("Refresh failed after 401:", refreshError);
        // Refresh failed - logout user
        await logout();
        window.location.href = "/auth/login";
        return Promise.reject(refreshError);
      }
    }

    return Promise.reject(error);
  }
);

export default api;
