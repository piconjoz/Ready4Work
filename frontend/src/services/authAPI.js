import api from "./api";

export const onboardRecruiter = async (onboardingData) => {
    try {
        const response = await api.post("/auth/onboard/recruiter", onboardingData);
        return response.data;
    } catch (error) {
        console.error("Error onboarding recruiter:", error);
        throw error;
    }
};