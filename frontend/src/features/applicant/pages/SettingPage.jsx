import Header from "../../../components/Header";
import MobileTab from "../../../components/MobileTab";
import { FaHeart, FaLock, FaUser } from "react-icons/fa";
import React, { useState } from "react";
import Profile from "./components/Profile";
import Security from "./components/Security";
import Resume from "./components/Resume";


const options = [
  { value: "profile", label: "My Profile", icon: <FaHeart /> },
  { value: "security", label: "Login & Security", icon: <FaLock /> },
  { value: "resume", label: "My Resume", icon: <FaUser /> },
];


export default function SettingPage() {

  const [selected, setSelected] = useState("profile");

  return (
      <div className="min-h-screen bg-[#F8F9FD] px-6 md:px-10">
          <Header active="" />
            {/* Recommended */}
          <div className="py-6">
            <h1 className="text-2xl font-semibold">Settings</h1>

            {/* Tab Switcher */}
            <div>
              {/* Mobile Tab */}
              <div className="md:hidden">
                <MobileTab
                  options={options}
                  selected={selected}
                  onSelect={(val) => setSelected(val)}
                />
              </div>
              {/* Desktop Tab */}
              <div className="hidden md:flex gap-8 mt-4 border-b border-gray-200">
                {options.map((option) => (
                  <button
                    key={option.value}
                    onClick={() => setSelected(option.value)}
                    className={`flex items-center gap-2 pb-2 border-b-2 ${
                      selected === option.value
                        ? "text-black border-black"
                        : "text-gray-300 border-transparent"
                    }`} >
                    {option.icon}
                    <span>{option.label}</span>
                  </button>
                ))}
              </div>
            </div>

            {/* Render selected tab content */}
            <div className="mt-6">
              {selected === "profile" && <Profile />}
              {selected === "security" && <Security />}
              {selected === "resume" && <Resume />}
            </div>

          </div>
      </div>
  );
}