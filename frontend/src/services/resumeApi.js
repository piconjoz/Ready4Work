import api from './api';

/**
 * Uploads a resume file for the current user.
 * @param {File} file - The resume PDF or image to upload.
 * @returns {Promise<Object>} The response data from the backend.
 */
export const uploadResume = async (file) => {
  const form = new FormData();
  form.append('File', file);
  const response = await api.post('/resumes/upload', form, {
    headers: { 'Content-Type': 'multipart/form-data' },
  });
  return response.data;
};

/**
 * Retrieves all resumes for the current user.
 * @returns {Promise<Array>} Array of resume records.
 */
export const listResumes = async () => {
  const response = await api.get('/resumes/user/1');
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