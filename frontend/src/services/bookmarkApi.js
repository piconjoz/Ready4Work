

import api from "./api";

/**
 * Fetch all bookmarks for a given applicant.
 * @param {number} applicantId
 * @returns {Promise<Array<{ bookmarkId: number, applicantId: number, jobsId: number, createdAt: string }>>}
 */
export function getBookmarks(applicantId) {
  return api.get(`/bookmarks/${applicantId}`)
    .then(response => response.data);
}

/**
 * Add a bookmark for a given applicant and job.
 * @param {number} applicantId 
 * @param {number} jobsId 
 * @returns {Promise<{ message: string }>}
 */
export function addBookmark(applicantId, jobsId) {
  return api.post(`/bookmarks/${applicantId}/${jobsId}`)
    .then(response => response.data);
}

/**
 * Remove a specific bookmark.
 * @param {number} applicantId 
 * @param {number} jobsId 
 * @returns {Promise<{ message: string }>}
 */
export function removeBookmark(applicantId, jobsId) {
  return api.delete(`/bookmarks/${applicantId}/${jobsId}`)
    .then(response => response.data);
}

/**
 * Clear all bookmarks for a given applicant.
 * @param {number} applicantId 
 * @returns {Promise<{ message: string }>}
 */
export function clearBookmarks(applicantId) {
  return api.delete(`/bookmarks/${applicantId}/clear`)
    .then(response => response.data);
}