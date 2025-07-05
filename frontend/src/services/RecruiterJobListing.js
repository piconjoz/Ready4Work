import api from "./api";

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

export const getJobListingDetails = async () =>{
  try {
    console.log("Sending DTO over");
  } catch (error) {
    throw error;
  }
}
