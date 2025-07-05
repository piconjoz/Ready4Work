import api from './api';

/**
 * Uploads a resume file for the current user.
 * @param {File} file - The resume PDF or image to upload.
 * @returns {Promise<Object>} The response data from the backend.
 */
export const uploadResume = async (file, applicantId) => {
  const form = new FormData();
  form.append('File', file);
  form.append('ApplicantId', applicantId);
  const response = await api.post('/resumes/upload', form, {
    headers: { 'Content-Type': 'multipart/form-data' },
    timeout: 300000  // 5 minutes timeout for large file uploads
  });
  return response.data;
};

/**
 * Retrieves all resumes for the current user.
 * @returns {Promise<Array>} Array of resume records.
 */
export const listResumes = async () => {
  const user = getCurrentUser();
  if (!user?.applicantId) throw new Error("Not signed in");

  const response = await api.get(`/resumes/user/${user.applicantId}`);
  return response.data;
};

/**
 * Deletes a specific resume by its ID.
 * @param {number} resumeId - The ID of the resume to delete.
 * @returns {Promise<void>}
 */
export const deleteResume = async (resumeId) => {
  await api.delete(`/resumes/${resumeId}`);
};