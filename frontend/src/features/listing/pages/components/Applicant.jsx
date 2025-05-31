import { useState } from "react";
import ApplicantList from "./ApplicantList";
import OfferedList from "./OfferedList";
import AcceptedList from "./AcceptedList";

export default function Applicant() {
  const [activeTab, setActiveTab] = useState("applicant");
  return (
    <div className="bg-[#F8F9FD]">
      <div className="py-6">
        {/* Tab Switcher */}
        <div className="flex w-full gap-2 justify-center">
          {["applicant", "offered", "accepted"].map((tab) => (
            <button
              key={tab}
              onClick={() => setActiveTab(tab)}
              className={`px-4 py-2 rounded-full text-sm font-medium ${
                activeTab === tab
                  ? "bg-black text-white"
                  : "bg-gray-200 text-black"
              }`}
            >
              {tab.charAt(0).toUpperCase() + tab.slice(1)}
            </button>
          ))}
        </div>
      </div>
      <div className="mb-6 ">
        {activeTab === "applicant" && (
          <div>
            <ApplicantList />
          </div>
        )}
        {activeTab === "offered" && (
          <div>
            <OfferedList />
          </div>
        )}
         {activeTab === "accepted" && (
          <div>
            <AcceptedList />
          </div>
        )}
      </div>
    </div>
  );
}