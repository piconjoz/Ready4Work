import Header from "../../../components/Header";
import { useState } from "react";
import ActiveList from "./components/ActiveList";
import ExpiredList from "./components/ExpiredList";

export default function BookmarkPage() {
  const [activeTab, setActiveTab] = useState("active");
  return (
    <div className="min-h-screen bg-[#F8F9FD] px-6 md:px-10">
      <Header active="bookmarks" />
      <div className="py-6">
        <h1 className="text-2xl font-semibold">Bookmarks</h1>
        {/* Tab Switcher */}
        <div className="flex w-full gap-2 mt-8 justify-center">
          {["active", "expired"].map((tab) => (
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
        {activeTab === "active" && (
          <div>
            <ActiveList />
          </div>
        )}
        {activeTab === "expired" && (
          <div>
            <ExpiredList />
          </div>
        )}
      </div>
    </div>
  );
}