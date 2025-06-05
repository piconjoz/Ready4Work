import React, { useState } from "react";
import RecruiterHeader from "../../../components/RecruiterHeader";
import StatusInputField from "../../../components/StatusInputField";
import MobileTab from "../../../components/MobileTab";
import { BsPeopleFill } from "react-icons/bs";
import { MdChecklist } from "react-icons/md";
import { PiFrameCornersFill } from "react-icons/pi";
import Applicant from "./components/Applicant";
import ApplicationInfo from "./components/ApplicantionInfo";
import JobInfo from "./components/JobInfo";
import DateStatusInputField from "../../../components/DateStatusInputField";

export default function ListingEditPage() {
  // Track which top-level tab is selected
  const [selected, setSelected] = useState("applicants");
  const [selectedDeadline, setSelectedDeadline] = useState(); 
  return (
    <div className="min-h-screen bg-[#F8F9FD] px-6 md:px-10">
      <RecruiterHeader active="" />

      {/* Page Title */}
      <div className="pt-6">
        <h1 className="text-2xl font-semibold">Job Listing Profile</h1>
            <div className="bg-white border border-[#D3D3D3] rounded-2xl p-6 my-6">
            <h2 className="text-lg font-semibold mb-2">Listing Settings</h2>
            <div className="grid grid-cols-1 gap-4">
              {/* Full-width Listing Name */}
              <StatusInputField
                label="Listing Name"
                name="listingName"
                type="text"
                status="default"
                defaultValue="Product Development Technologist"
                readOnly={false}
                className="col-span-full"
              />
              {/* Three-column row */}
              <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                <DateStatusInputField
                  value={selectedDeadline}
                  onChange={(newDateString) => setSelectedDeadline(newDateString)}
                />
                <StatusInputField
                  label="Maximum Vacancies"
                  name="maxVacancies"
                  type="number"
                  status="default"
                  defaultValue="2"
                  readOnly={false}
                />
                <StatusInputField
                  label="Visibility"
                  name="visibility"
                  type="text"
                  status="default"
                  defaultValue="Public"
                  readOnly={false}
                />
              </div>
            </div>
          </div>
      </div>

      {/* Top-level Tab Switcher */}
      <div>
        {/* Mobile Tab */}
        <div className="md:hidden">
          <MobileTab
            options={[
              { value: "applicants", label: "Applicants", icon: <BsPeopleFill /> },
              { value: "info", label: "Applicantion Info", icon: <MdChecklist /> },
              { value: "job", label: "Job Info", icon: <PiFrameCornersFill /> }
            ]}
            selected={selected}
            onSelect={(val) => setSelected(val)}
          />
        </div>
        {/* Desktop Tab */}
        <div className="hidden md:flex gap-8 mt-4 border-b border-gray-200">
          {[
            { value: "applicants", label: "Applicants", icon: <BsPeopleFill /> },
            { value: "info", label: "Applicantion Info", icon: <MdChecklist /> },
            { value: "job", label: "Job Info", icon: <PiFrameCornersFill /> }
          ].map((option) => (
            <button
              key={option.value}
              onClick={() => setSelected(option.value)}
              className={`flex items-center gap-2 pb-2 border-b-2 ${
                selected === option.value
                  ? "text-black border-black"
                  : "text-gray-300 border-transparent"
              }`}
            >
              {option.icon}
              <span>{option.label}</span>
            </button>
          ))}
        </div>
      </div>

      {/* Render selected tab content */}
      
      <div className="mt-6">
        {selected === "applicants" && (
            <Applicant />
        )}

        {selected === "info" && (
            <ApplicationInfo/>
        )}
        {selected === "job" && (
            <JobInfo/>
        )}
      </div>
    </div>
  );
}