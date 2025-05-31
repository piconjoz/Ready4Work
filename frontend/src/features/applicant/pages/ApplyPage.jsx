import Header from "../../../components/Header";
import { useState } from "react";
import PendingList from "./components/PendingList";
import SuccessfulList from "./components/SuccessfulList";
import FailedList from "./components/FailedList";


export default function ApplyPage() {
  const [activeTab, setActiveTab] = useState("pending");
  return (
    <div className="min-h-screen bg-[#F8F9FD] px-6 md:px-10">
      <Header active="applications" />
      <div className="py-6">
        <h1 className="text-2xl font-semibold">Applications</h1>
          {/* Tab Switcher */}
          <div className="flex w-full gap-2 mt-8 justify-center">
            {["pending", "successful", "failed"].map((tab) => (
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
      <div className="mt-6">
        {activeTab === "pending" && 
        <div>
          <PendingList />
        </div>}
        {activeTab === "successful" && 
        <div>
          <SuccessfulList />
        </div>}
        {activeTab === "failed" && 
        <div>
          <FailedList />
        </div>}
      </div>

    </div>
  );
}