import React, { useState, useEffect, useCallback } from "react";
import MobileTab from "../../../../components/MobileTab";
import CompanyInfoForm from "./CompanyInfoForm";
import CompanyContactForm from "./CompanyContactForm";
import RecruiterContactForm from "./RecruiterContactForm";
import { IoMdDocument } from "react-icons/io";
import { FaUserFriends } from "react-icons/fa";
import { FaUserCircle } from "react-icons/fa";

const options = [
  { value: "company", label: "Company Information", icon: <IoMdDocument /> },
  {
    value: "contact1",
    label: "Company Contact Information",
    icon: <FaUserFriends />,
  },
  {
    value: "contact2",
    label: "Business Contact Information",
    icon: <FaUserCircle />,
  },
];

export default function OnboardInfo({
  currentStep,
  onValidationChange,
  showValidationErrors,
  onDataChange,
}) {
  const [formValidation, setFormValidation] = useState({
    company: false,
    contact1: false,
    contact2: false,
  });

  // Map currentStep to tab values
  const stepToTab = ["company", "contact1", "contact2"];
  const currentTab = stepToTab[currentStep];

  // Update parent validation when current step validation changes
  useEffect(() => {
    const currentStepKey = Object.keys(formValidation)[currentStep];
    onValidationChange(formValidation[currentStepKey]);
  }, [formValidation, currentStep, onValidationChange]);

  // Use useCallback to prevent infinite re-renders
  const handleFormValidation = useCallback((formName, isValid) => {
    setFormValidation((prev) => ({
      ...prev,
      [formName]: isValid,
    }));
  }, []);

  // Use useCallback for onDataChange too
  const handleDataChange = useCallback(
    (stepIndex, data) => {
      if (onDataChange) {
        onDataChange(stepIndex, data);
      }
    },
    [onDataChange]
  );

  // Check if a tab should be disabled
  const isTabDisabled = (tabIndex) => {
    return tabIndex > currentStep;
  };

  // Check if a tab is completed
  const isTabCompleted = (tabIndex) => {
    const tabKey = Object.keys(formValidation)[tabIndex];
    return formValidation[tabKey] && tabIndex < currentStep;
  };

  return (
    <div>
      <h1 className="text-2xl font-semibold mb-4">About</h1>

      {/* Tab Switcher */}
      <div>
        {/* Mobile Tab - Show current step only */}
        <div className="md:hidden">
          <div className="bg-white border border-gray-200 rounded-lg p-4 mb-4">
            <div className="flex items-center gap-2">
              {options[currentStep].icon}
              <span className="font-medium">{options[currentStep].label}</span>
            </div>
          </div>
        </div>

        {/* Desktop Tab */}
        <div className="hidden md:flex gap-8 mt-4 border-b border-gray-200">
          {options.map((option, index) => {
            const isDisabled = isTabDisabled(index);
            const isCompleted = isTabCompleted(index);
            const isCurrent = index === currentStep;

            return (
              <button
                key={option.value}
                disabled={isDisabled}
                className={`flex items-center gap-2 pb-2 border-b-2 ${
                  isCurrent
                    ? "text-black border-black"
                    : isCompleted
                      ? "text-green-600 border-transparent"
                      : isDisabled
                        ? "text-gray-300 border-transparent cursor-not-allowed"
                        : "text-gray-300 border-transparent"
                }`}
              >
                {option.icon}
                <span>{option.label}</span>
                {/* Validation indicator */}
                {isCompleted && <span className="ml-1 text-green-500">✓</span>}
              </button>
            );
          })}
        </div>
      </div>

      {/* Render current step content */}
      <div className="mt-6">
        {currentStep === 0 && (
          <CompanyInfoForm
            onValidationChange={handleFormValidation}
            onDataChange={handleDataChange}
            showValidationErrors={showValidationErrors}
          />
        )}
        {currentStep === 1 && (
          <CompanyContactForm
            onValidationChange={handleFormValidation}
            onDataChange={handleDataChange}
            showValidationErrors={showValidationErrors}
          />
        )}
        {currentStep === 2 && (
          <RecruiterContactForm
            onValidationChange={handleFormValidation}
            onDataChange={handleDataChange}
            showValidationErrors={showValidationErrors}
          />
        )}
      </div>
    </div>
  );
}
