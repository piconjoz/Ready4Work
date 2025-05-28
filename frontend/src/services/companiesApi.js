import api from "./api";

export const getCompanies = async () => {
  try {
    const response = await api.get("/companies");
    return response.data;
  } catch (error) {
    console.error("Error fetching companies:", error);
    throw error;
  }
};
