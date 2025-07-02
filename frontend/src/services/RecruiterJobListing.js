import api from "./api";

// services/jobListingService.js
export const listJobListings = async () => {
  try {
    const listingResponseDTO = await api.get('jobListings/listings')
    return listingResponseDTO.data;
  }
  catch (error)
  {
    console.error("API Error:", error);
    throw error;
  }
}
