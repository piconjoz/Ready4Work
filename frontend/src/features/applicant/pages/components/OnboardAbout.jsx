import { FaHeart } from "react-icons/fa";
import React, { useState } from "react";
import MobileTab from "../../../../components/MobileTab";
import OnboardResumeForm from "./OnboardResumeForm";

const options = [
  { value: "about", label: "Employment Profile", icon: <FaHeart /> },
];

export default function OnboardAboutForm() {  

    const [selected, setSelected] = useState("about");

    return (
        <div>
            <h1 className="text-2xl font-semibold mb-4">
                About
            </h1>
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
                {selected === "about" && <OnboardResumeForm />}
            </div>

        </div>
    );
}