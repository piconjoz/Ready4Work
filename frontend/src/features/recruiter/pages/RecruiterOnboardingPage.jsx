import { useState, useEffect } from "react";
import OnboardHeader from "../../../components/OnboardHeader";
import ProgressIndicator from "../../../components/ProgressIndicator";
import { MdCheck, MdPerson } from "react-icons/md";
import { FaPersonCircleQuestion } from "react-icons/fa6";
import OnboardInfo from "./components/OnboardInfo";
import OnboardReview from "./components/OnboardReview";

export default function RecruiterOnboardingPage() {
  const steps = [
    { icon: FaPersonCircleQuestion, label: "Company Info" },
    { icon: MdPerson, label: "Review" },
  ];

  const [currentStep, setCurrentStep] = useState(0); // 0: Company Info tabs, 1: Review
  const [currentInfoStep, setCurrentInfoStep] = useState(0); // 0: Company, 1: Contact, 2: Business
  const [isCurrentStepValid, setIsCurrentStepValid] = useState(false);
  const [showValidationErrors, setShowValidationErrors] = useState(false);

  // Store all form data
  const [signupData, setSignupData] = useState(null);
  const [companyData, setCompanyData] = useState({});
  const [contactData, setContactData] = useState({});
  const [recruiterData, setRecruiterData] = useState({});

  // Load signup data on component mount
  useEffect(() => {
    const storedSignupData = sessionStorage.getItem("signupData");
    if (storedSignupData) {
      setSignupData(JSON.parse(storedSignupData));
    }
  }, []);

  const handleNext = () => {
    if (!isCurrentStepValid) {
      setShowValidationErrors(true);
      return;
    }

    setShowValidationErrors(false);

    if (currentStep === 0) {
      // We're in the company info section
      if (currentInfoStep < 2) {
        // Move to next info step
        setCurrentInfoStep(currentInfoStep + 1);
        setIsCurrentStepValid(false); // Reset validation for next step
      } else {
        // Move to review step
        setCurrentStep(1);
      }
    }
  };

  const handleBack = () => {
    if (currentStep === 1) {
      // Go back from review to last info step
      setCurrentStep(0);
      setCurrentInfoStep(2);
    } else if (currentInfoStep > 0) {
      // Go back to previous info step
      setCurrentInfoStep(currentInfoStep - 1);
      setIsCurrentStepValid(true); // Previous steps were already valid
    }
    setShowValidationErrors(false);
  };

  // Determine button text
  const getNextButtonText = () => {
    if (currentInfoStep === 2) return "Review";
    return "Next";
  };

  // Determine if back button should be disabled
  const isBackDisabled = currentStep === 0 && currentInfoStep === 0;

  // Hide Next button on review step (form has its own submit button)
  const showNextButton = currentStep === 0;

  return (
    <div className="min-h-screen bg-[#F8F9FD] px-6 md:px-10">
      <OnboardHeader />

      <div className="py-6">
        <ProgressIndicator icons={steps} currentStep={currentStep} />

        {currentStep === 0 && (
          <OnboardInfo
            currentStep={currentInfoStep}
            onValidationChange={setIsCurrentStepValid}
            showValidationErrors={showValidationErrors}
            onDataChange={(stepIndex, data) => {
              if (stepIndex === 0) setCompanyData(data);
              else if (stepIndex === 1) setContactData(data);
              else if (stepIndex === 2) setRecruiterData(data);
            }}
          />
        )}
        {currentStep === 1 && (
          <OnboardReview
            signupData={signupData}
            companyData={companyData}
            contactData={contactData}
            recruiterData={recruiterData}
            onEmailChange={(newEmail) => {
              setSignupData((prev) => ({ ...prev, email: newEmail }));
            }}
          />
        )}

        <div className="mt-6 flex justify-between">
          <button
            onClick={handleBack}
            className="bg-gray-300 text-black rounded-lg py-3 px-4 w-45 mx-2 disabled:opacity-50"
            disabled={isBackDisabled}
          >
            Back
          </button>

          {/* Only show Next button when not on review step */}
          {showNextButton && (
            <button
              onClick={handleNext}
              className="bg-black text-white rounded-lg py-3 px-4 w-45 mx-2 disabled:opacity-50"
              disabled={
                currentStep === 0 &&
                currentInfoStep === 2 &&
                !isCurrentStepValid
              }
            >
              {getNextButtonText()}
            </button>
          )}
        </div>
      </div>
    </div>
  );
}
