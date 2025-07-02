// frontend/src/services/adminAPI.js
import api from "./api";

// ============ ADMIN PROFILE & DASHBOARD ============
export const getAdminProfile = async () => {
  try {
    const response = await api.get("/admins/profile");
    return response.data;
  } catch (error) {
    console.error("Error fetching admin profile:", error);
    throw error;
  }
};

export const getAdminDashboard = async () => {
  try {
    const response = await api.get("/admins/dashboard");
    return response.data;
  } catch (error) {
    console.error("Error fetching admin dashboard:", error);
    throw error;
  }
};

export const getSystemInfo = async () => {
  try {
    const response = await api.get("/admins/system/info");
    return response.data;
  } catch (error) {
    console.error("Error fetching system info:", error);
    throw error;
  }
};

// ============ USER MANAGEMENT ============
export const getAllUsers = async () => {
  try {
    const response = await api.get("/admins/accounts");
    return response.data;
  } catch (error) {
    console.error("Error fetching users:", error);
    throw error;
  }
};

export const getUsersByType = async (userType) => {
  try {
    const response = await api.get(`/admins/accounts/type/${userType}`);
    return response.data;
  } catch (error) {
    console.error("Error fetching users by type:", error);
    throw error;
  }
};

export const getUserById = async (userId) => {
  try {
    const response = await api.get(`/admins/accounts/${userId}`);
    return response.data;
  } catch (error) {
    console.error("Error fetching user:", error);
    throw error;
  }
};

// Removed createUserAccount function - no longer needed

export const updateUserAccount = async (userId, userData) => {
  try {
    const response = await api.put(`/admins/accounts/${userId}`, userData);
    return response.data;
  } catch (error) {
    console.error("Error updating user account:", error);
    throw error;
  }
};

export const deleteUserAccount = async (userId) => {
  try {
    const response = await api.delete(`/admins/accounts/${userId}`);
    return response.data;
  } catch (error) {
    console.error("Error deleting user account:", error);
    throw error;
  }
};

export const toggleUserActivation = async (userId, isActive) => {
  try {
    const response = await api.put(`/admins/users/${userId}/activate`, {
      isActive,
    });
    return response.data;
  } catch (error) {
    console.error("Error toggling user activation:", error);
    throw error;
  }
};

// ============ COMPANY MANAGEMENT ============
export const getAllCompanies = async () => {
  try {
    const response = await api.get("/admins/companies");
    return response.data;
  } catch (error) {
    console.error("Error fetching companies:", error);
    throw error;
  }
};

export const deleteCompany = async (companyId) => {
  try {
    const response = await api.delete(`/admins/companies/${companyId}`);
    return response.data;
  } catch (error) {
    console.error("Error deleting company:", error);
    throw error;
  }
};