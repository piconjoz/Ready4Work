import React, { useState } from "react";
import MobileTab from "../../../../components/MobileTab";
import CompanyInfoForm from "./CompanyInfoForm";
import CompanyContactForm from "./CompanyContactForm";
import RecruiterContactForm from "./RecruiterContactForm";
import { IoMdDocument } from "react-icons/io";
import { FaUserFriends } from "react-icons/fa";
import { FaUserCircle } from "react-icons/fa";




const options = [
  { value: "company", label: "Company Information", icon: <IoMdDocument /> },
    { value: "contact1", label: "Company Contact Information", icon: <FaUserFriends /> },
    { value: "contact2", label: "Business Contact Information", icon: <FaUserCircle /> },

];

export default function OnboardInfo() {  

    const [selected, setSelected] = useState("company");

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
                {selected === "company" && <CompanyInfoForm />}
                {selected === "contact1" && <CompanyContactForm />}
                {selected === "contact2" && <RecruiterContactForm />}
            </div>

        </div>
    );
}