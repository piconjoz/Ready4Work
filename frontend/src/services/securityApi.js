import api from "./api"

// Check current password validity
export const validateCredentials = async (email, password) => {
  try {
    const response = await api.post("/auth/validate-credentials", {
      email,
      password,
    });
    return response.data; // e.g. { message: "Credentials valid" }
  } catch (err) {
    // Propagate backend error message or generic text
    const msg = err.response?.data?.message ?? err.message;
    throw new Error(msg);
  }
};

// Update the user’s password
export const updatePassword = async (newPassword) => {
  try {
    const response = await api.put("/users/password", {
      newPassword
    });
    return response.data; // e.g. { message: "Password updated successfully" }
  } catch (err) {
    const msg = err.response?.data?.message || "Password update failed";
    throw new Error(msg);
  }
};
