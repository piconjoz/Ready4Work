import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import StatusInputField from "../../../../components/StatusInputField";
import PrimaryButton from "../../../../components/PrimaryButton";
import NoticeBanner from "../../../../components/NoticeBanner";
import DisclaimerCheckbox from "../../../../components/DisclaimerCheckbox";
import { signupApplicant, checkStudent } from "../../../../services/authAPI";

export default function SignupForm() {
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [retypePassword, setRetypePassword] = useState("");
  const [accountType, setAccountType] = useState(null);
  const [showNotice, setShowNotice] = useState(false);
  const [shouldShowDetection, setShouldShowDetection] = useState(false);
  const [agreedToTerms, setAgreedToTerms] = useState(false);

  const [showOtpField, setShowOtpField] = useState(false);
  const [otp, setOtp] = useState("");
  const [otpError, setOtpError] = useState("");
  const [emailError, setEmailError] = useState("");
  // New state for server-side errors
  const [serverError, setServerError] = useState("");

  // Check email domain and set account type
  useEffect(() => {
    if (email.trim() && shouldShowDetection) {
      const isStudentEmail = email
        .toLowerCase()
        .includes("@sit.singaporetech.edu.sg");
      const newAccountType = isStudentEmail ? "student" : "employer";

      if (accountType !== newAccountType) {
        setAccountType(newAccountType);
        setShowNotice(true);
      }
    } else if (!email.trim()) {
      setAccountType(null);
      setShowNotice(false);
      setShouldShowDetection(false);
    }
  }, [email, accountType, shouldShowDetection]);

  const handleEmailChange = (e) => {
    const value = e.target.value;
    setEmail(value);
  };

  const handleEmailBlur = async () => {
    if (email.trim() && email.includes("@")) {
      setShouldShowDetection(true);
      // verify student via API
      try {
        const isValid = await checkStudent(email);
        if (isValid) {
          setShowOtpField(true);
          setEmailError("");
        } else {
          setShowOtpField(false);
          setEmailError("No Student Found");
        }
      } catch (err) {
        setShowOtpField(false);
        setEmailError("Error verifying student email");
      }
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setServerError(""); // Clear server error at the start
    if (accountType === "student" && otp !== "1234") {
      setOtpError("Invalid OTP");
      return;
    }

    const signupData = { email, password };
    try {
      const result = await signupApplicant(signupData);
      // Debug: log the access token and refresh-token cookie
      console.log("Access token:", result.token);
      console.log("Refresh token cookie:", document.cookie);
      navigate("/applicant/onboard");
    } catch (err) {
      // Surface server-side validation errors if present
      const resp = err.response;
      if (resp && resp.data && resp.data.errors) {
        // pick first field error
        const fieldErrors = Object.values(resp.data.errors).flat();
        setServerError(fieldErrors.join(" "));
      } else if (resp && resp.data && resp.data.title) {
        setServerError(resp.data.title);
      } else if (resp && resp.data && resp.data.message) {
        setServerError(resp.data.message);
      } else {
        setServerError("Error creating account");
      }
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <StatusInputField
        label="Email"
        name="email"
        type="email"
        status={emailError ? "error" : "default"}
        errorMessage={emailError}
        value={email}
        onChange={handleEmailChange}
        onBlur={handleEmailBlur}
      />
      <StatusInputField
        label="Password"
        name="password"
        type="password"
        status="default"
        errorMessage=""
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />
      <StatusInputField
        label="Reverify Password"
        name="retypePassword"
        type="password"
        status={
          retypePassword && password !== retypePassword ? "error" : "default"
        }
        errorMessage={
          retypePassword && password !== retypePassword
            ? "Passwords do not match"
            : ""
        }
        value={retypePassword}
        onChange={(e) => setRetypePassword(e.target.value)}
      />

      {/* OTP Field */}
      {showOtpField && (
        <StatusInputField
          label="OTP"
          name="otp"
          type="text"
          status={otpError ? "error" : "default"}
          errorMessage={otpError}
          value={otp}
          onChange={(e) => setOtp(e.target.value)}
        />
      )}

      {/* Server-side error display */}
      {serverError && (
        <div className="text-red-600 mt-2">
          {serverError}
        </div>
      )}

      {/* Account Detection Banner */}
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
                ? "We'll set up your student account using the details you've provided. Need help? Reach out to the SIT Helpdesk."
                : "We'll set up your employer account using the details you've provided. Need help? Reach out to the SIT Helpdesk."
            }
            onClose={() => setShowNotice(false)}
          />
        </div>
      )}

      {/* Account Type Checkbox */}
      {accountType && shouldShowDetection && (
        <DisclaimerCheckbox
          title={
            accountType === "student" ? "Student Account" : "Employer Account"
          }
          description={
            accountType === "student"
              ? "Create Ready4Work account with SIT Credentials"
              : "Create Ready4Work account with Company Credentials"
          }
          name={
            accountType === "student" ? "studentAccount" : "employerAccount"
          }
          checked={agreedToTerms}
          onChange={(e) => setAgreedToTerms(e.target.checked)}
        />
      )}

      <PrimaryButton
        className="mt-4"
        type="submit"
        label="Create Account"
        disabled={
          !email ||
          !password ||
          !retypePassword ||
          password !== retypePassword ||
          !accountType ||
          !agreedToTerms ||
          (accountType === "student" && !otp)
        }
      />
    </form>
  );
}
