import { useState } from "react";
import OnboardHeader from "../../../components/OnboardHeader";
import ProgressIndicator from '../../../components/ProgressIndicator';
import { MdCheck, MdPerson } from "react-icons/md";
import { FaPersonCircleQuestion } from "react-icons/fa6";
import OnboardInfo from "./components/OnboardInfo";
import OnboardReview from "./components/OnboardReview";


// // // Placeholder components for the onboarding steps
// function AboutStep() {
//   return <div className="mt-4">This is the About Step.</div>;
// }

// function ReviewStep() {
//   return <div className="mt-4">This is the Review Step.</div>;
// }

export default function ApplicantOnboardingPage() {
  const steps = [
    { icon: FaPersonCircleQuestion, label: "Company Info" },
    { icon: MdPerson, label: "Review" },
  ];

  const [currentStep, setCurrentStep] = useState(0);

  const handleNext = () => {
    if (currentStep < steps.length - 1) setCurrentStep(currentStep + 1);
  };

  const handleBack = () => {
    if (currentStep > 0) setCurrentStep(currentStep - 1);
  };

  return (
    <div className="min-h-screen bg-[#F8F9FD] px-6 md:px-10">
      <OnboardHeader />

      <div className="py-6">
        <ProgressIndicator icons={steps} currentStep={currentStep} />


        {currentStep === 0 && <OnboardInfo/>}
        {currentStep === 1 && <OnboardReview/>}

        <div className="mt-6 flex justify-between">
          <button
            onClick={handleBack}
            className="bg-gray-300 text-black bg-black rounded-lg py-3 px-4 w-45 mx-2 disabled:opacity-50"
            disabled={currentStep === 0}>
            Back
          </button>
          <button
            onClick={handleNext}
            className="bg-black text-white rounded-lg py-3 px-4 w-45 mx-2 disabled:opacity-50"
            disabled={currentStep === steps.length - 1}>
            Next
          </button>
        </div>
      </div>
    </div>
  );
}