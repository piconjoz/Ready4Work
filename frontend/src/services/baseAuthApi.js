// baseAuthApi.js - Simple API instance just for authentication
import axios from "axios";

const baseAuthApi = axios.create({
  baseURL: import.meta.env.VITE_API_URL || "http://localhost:5050/api",
  timeout: 10000,
  withCredentials: true,
});

// NO interceptors here - just plain API calls for authentication
export default baseAuthApi;
