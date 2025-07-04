import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import StatusInputField from "../../../../components/StatusInputField";
import PrimaryButton from "../../../../components/PrimaryButton";
import NoticeBanner from "../../../../components/NoticeBanner";
import DisclaimerCheckbox from "../../../../components/DisclaimerCheckbox";

export default function SignupForm() {
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [retypePassword, setRetypePassword] = useState("");
  const [accountType, setAccountType] = useState(null);
  const [showNotice, setShowNotice] = useState(false);
  const [shouldShowDetection, setShouldShowDetection] = useState(false);
  const [agreedToTerms, setAgreedToTerms] = useState(false);

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

  const handleEmailBlur = () => {
    if (email.trim() && email.includes("@")) {
      setShouldShowDetection(true);
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    const signupData = {
      email,
      password,
      accountType,
      registrationDate: new Date().toISOString().split("T")[0],
    };

    sessionStorage.setItem("signupData", JSON.stringify(signupData));

    // Navigate to correct onboarding routes
    if (accountType === "employer") {
      navigate("/recruiter/onboard");
    } else {
      navigate("/applicant/onboard");
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <StatusInputField
        label="Email"
        name="email"
        type="email"
        status="default"
        errorMessage=""
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
          !agreedToTerms
        }
      />
    </form>
  );
}
