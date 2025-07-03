import baseApi from "./api";  // baseUrl = '/api'

export const getPreference = (id) =>
  baseApi.get(`/applicant/preferences/${id}`).then(r => r.data);
export const savePreference = (id, prefs) =>
  baseApi.post(`/applicant/preferences/${id}`, prefs).then(r => r.data);