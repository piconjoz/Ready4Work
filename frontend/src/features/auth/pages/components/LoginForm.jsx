import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import StatusInputField from "../../../../components/StatusInputField";
import PrimaryButton from "../../../../components/PrimaryButton";
import NoticeBanner from "../../../../components/NoticeBanner";
import DisclaimerCheckbox from "../../../../components/DisclaimerCheckbox";
import { useAuth } from "../../../../contexts/AuthContext.jsx";
import toast from "react-hot-toast";

export default function LoginForm() {
  const navigate = useNavigate();
  const { login, isLoading: authLoading } = useAuth();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [accountType, setAccountType] = useState(null);
  const [showNotice, setShowNotice] = useState(false);
  const [shouldShowDetection, setShouldShowDetection] = useState(false);
  const [agreedToTerms, setAgreedToTerms] = useState(false);
  const [isLoading, setIsLoading] = useState(false);

  // Account detection logic (same as before)
  useEffect(() => {
    if (username.trim() && shouldShowDetection) {
      const isStudentUsername = username
        .toLowerCase()
        .includes("@sit.singaporetech.edu.sg");
      const newAccountType = isStudentUsername ? "student" : "employer";

      if (accountType !== newAccountType) {
        setAccountType(newAccountType);
        setShowNotice(true);
      }
    } else if (!username.trim()) {
      setAccountType(null);
      setShowNotice(false);
      setShouldShowDetection(false);
    }
  }, [username, accountType, shouldShowDetection]);

  const handleUsernameChange = (e) => {
    setUsername(e.target.value);
  };

  const handleUsernameBlur = () => {
    if (username.trim() && username.includes("@")) {
      setShouldShowDetection(true);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (isLoading || authLoading) return;
    setIsLoading(true);

    try {
      const loginData = {
        username: username,
        password: password,
      };

      console.log("Attempting login...");

      // Use AuthContext login instead of direct API call
      const response = await login(loginData);

      console.log("Login successful!");
      console.log("User type:", response.user.userType);

      toast.success("Login successful!");

      // Navigate based on user type
      if (response.user.userType === 2) {
        console.log("Redirecting to recruiter home");
        navigate("/recruiter/home");
      } else if (response.user.userType === 1) {
        console.log("Redirecting to applicant home");
        navigate("/applicant/home");
      } else if (response.user.userType === 3) {
        console.log("Redirecting to admin dashboard");
        navigate("/admin/dashboard");
      }
    } catch (error) {
      console.error("Login error:", error);

      if (error.response?.status === 401) {
        toast.error("Invalid email or password");
      } else if (error.response?.status === 400) {
        toast.error("Please check your input fields");
      } else if (error.response?.data?.message) {
        toast.error(error.response.data.message);
      } else if (error.code === "NETWORK_ERROR") {
        toast.error("Network error. Please check your connection.");
      } else {
        toast.error("Login failed. Please try again.");
      }
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <StatusInputField
        label="Username"
        name="Username"
        type="text"
        status="default"
        errorMessage=""
        value={username}
        onChange={handleUsernameChange}
        onBlur={handleUsernameBlur}
        disabled={isLoading || authLoading}
      />
      <StatusInputField
        label="Password"
        name="password"
        type="password"
        status="default"
        errorMessage=""
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        disabled={isLoading || authLoading}
      />

      {showNotice && accountType && shouldShowDetection && (
        <div className="mt-6">
          <NoticeBanner
            title={
              accountType === "student"
                ? "Student Account Detected"
                : "Employer Account Detected"
            }
            message={
              accountType === "student"
                ? "We'll authenticate your student account using the details you've provided. Need help? Reach out to the SIT Helpdesk."
                : "We'll authenticate your employer account using the details you've provided. Need help? Reach out to the SIT Helpdesk."
            }
            onClose={() => setShowNotice(false)}
          />
        </div>
      )}

      {accountType && shouldShowDetection && (
        <DisclaimerCheckbox
          title={
            accountType === "student" ? "Student Account" : "Employer Account"
          }
          description={
            accountType === "student"
              ? "Login to Ready4Work with SIT Credentials"
              : "Login to Ready4Work with Company Credentials"
          }
          name={
            accountType === "student" ? "studentAccount" : "employerAccount"
          }
          checked={agreedToTerms}
          onChange={(e) => setAgreedToTerms(e.target.checked)}
          disabled={isLoading || authLoading}
        />
      )}

      <PrimaryButton
        type="submit"
        label={isLoading || authLoading ? "Logging in..." : "Login"}
        disabled={
          !username ||
          !password ||
          (accountType && shouldShowDetection && !agreedToTerms) ||
          isLoading ||
          authLoading // ← Include authLoading
        }
      />
    </form>
  );
}
