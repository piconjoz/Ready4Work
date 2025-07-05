import baseAuthApi from "./baseAuthApi"; // ← Much clearer naming!

// ============ AUTHENTICATION FUNCTIONS ============

// Login function
export const login = async (loginData) => {
  try {
    const response = await baseAuthApi.post("/auth/login", {
      email: loginData.username,
      password: loginData.password,
    });

    // Store access token and user data in localStorage
    if (response.data.token) {
      localStorage.setItem("token", response.data.token);
      localStorage.setItem("user", JSON.stringify(response.data.user));
      localStorage.setItem("tokenExpiry", response.data.expiresAt);
    }

    return response.data;
  } catch (error) {
    console.error("Error logging in:", error);
    throw error;
  }
};

// Refresh access token using httpOnly cookie
export const refreshToken = async () => {
  try {
    const response = await baseAuthApi.post("/auth/refresh");

    // Update stored access token
    if (response.data.token) {
      localStorage.setItem("token", response.data.token);
      localStorage.setItem("user", JSON.stringify(response.data.user));
      localStorage.setItem("tokenExpiry", response.data.expiresAt);
    }

    return response.data;
  } catch (error) {
    console.error("Error refreshing token:", error);
    throw error;
  }
};

// Logout function
export const logout = async () => {
  try {
    await baseAuthApi.post("/auth/logout");
  } catch (error) {
    console.error("Error during logout:", error);
  } finally {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    localStorage.removeItem("tokenExpiry");
  }
};

// ============ UTILITY FUNCTIONS ============

export const getCurrentUser = () => {
  const user = localStorage.getItem("user");
  return user ? JSON.parse(user) : null;
};

export const isAuthenticated = () => {
  const token = localStorage.getItem("token");
  const expiry = localStorage.getItem("tokenExpiry");

  if (!token || !expiry) return false;

  const expiryTime = new Date(expiry).getTime();
  const currentTime = new Date().getTime();
  const bufferTime = 5 * 60 * 1000; // 5 minutes

  return currentTime < expiryTime - bufferTime;
};

export const getToken = () => {
  return localStorage.getItem("token");
};

export const needsRefresh = () => {
  const expiry = localStorage.getItem("tokenExpiry");
  if (!expiry) return false;

  const expiryTime = new Date(expiry).getTime();
  const currentTime = new Date().getTime();
  const bufferTime = 5 * 60 * 1000; // 5 minutes

  return currentTime >= expiryTime - bufferTime;
};

// ============ SIGNUP FUNCTIONS ============

export const signupApplicant = async (signupData) => {
  try {
    const response = await baseAuthApi.post(
      "/auth/signup/applicant",
      signupData
    );

    if (response.data.token) {
      localStorage.setItem("token", response.data.token);
      localStorage.setItem("user", JSON.stringify(response.data.user));
      localStorage.setItem("tokenExpiry", response.data.expiresAt);
    }

    return response.data;
  } catch (error) {
    console.error("Error signing up applicant:", error);
    throw error;
  }
};

export const onboardRecruiter = async (onboardingData) => {
  try {
    const response = await baseAuthApi.post(
      "/auth/onboard/recruiter",
      onboardingData
    );
    return response.data;
  } catch (error) {
    console.error("Error onboarding recruiter:", error);
    throw error;
  }
};

// Check if a student email is valid
export const checkStudent = async (email) => {
  try {
    const response = await baseAuthApi.post("/auth/check", { email });
    return response.data.isValid;
  } catch (error) {
    console.error("Error checking student:", error);
    throw error;
  }
};

export const getStudentProfile = async (email) => {
  const response = await baseAuthApi.get("/auth/student-profile", {
    params: { email },
  });
  return response.data; // StudentProfileDTO
};
