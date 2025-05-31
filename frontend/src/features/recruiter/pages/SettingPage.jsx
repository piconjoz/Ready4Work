import RecruiterHeader from "../../../components/RecruiterHeader";
import MobileTab from "../../../components/MobileTab";
import { IoMdDocument } from "react-icons/io";
import { FaUserFriends } from "react-icons/fa";
import { FaUserCircle } from "react-icons/fa";
import { IoMdLock } from "react-icons/io";

import React, { useState } from "react";
import SettingCompanyInfo from "./components/SettingCompanyInfo";
import SettingCompanyContact from "./components/SettingCompanyContact";
import SettingRecruiterContact from "./components/SettingRecruiterContact";
import SettingSecurity from "./components/SettingSecurity";

const options = [
  { value: "company", label: "Company Information", icon: <IoMdDocument /> },
    { value: "contact1", label: "Company Contact Information", icon: <FaUserFriends /> },
    { value: "contact2", label: "Business Contact Information", icon: <FaUserCircle /> },
    { value: "security", label: "Login & Security", icon: <IoMdLock /> },

];
export default function RecruiterSettingPage() {

   const [selected, setSelected] = useState("company");
   
  return (
       <div className="min-h-screen bg-[#F8F9FD] px-6 md:px-10">
           <RecruiterHeader active="" />
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
                {selected === "company" && <SettingCompanyInfo />}
                {selected === "contact1" && <SettingCompanyContact />}
                {selected === "contact2" && <SettingRecruiterContact />}
                {selected === "security" && <SettingSecurity />}
                {/* {selected === "security" && <div className="text-gray-500">Security settings will be implemented soon.</div>} */}
            </div>
 
           </div>
       </div>
  );
}