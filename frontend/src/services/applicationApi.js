import api from "./api";

export const submitApplication = async (jobListingId) => {
  try {
    const response = await api.post("/applications/apply", {
      jobListingId: jobListingId
    });
    return response.data;
  } catch (error) {
    console.error("Error submitting application:", error);
    throw error;
  }
};

export const getApplicationStatus = async (applicationId) => {
  try {
    const response = await api.get(`/applications/${applicationId}`);
    return response.data;
  } catch (error) {
    console.error("Error fetching application status:", error);
    throw error;
  }
};