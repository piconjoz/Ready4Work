import React, { useState } from "react";
import SearchBar from '../../../components/SearchBar';
import StatCard from '../../../components/StatisticCard';
import { FaCheckCircle } from "react-icons/fa";
import { FaHeart, FaLock, FaUser } from "react-icons/fa";
import MobileTab from '../../../components/MobileTab';
import ResumeModal from "../../../components/ResumeModal";
import NavBar from "../../../components/NavBar";


const options = [
  { value: "profile", label: "My Profile", icon: <FaHeart /> },
  { value: "security", label: "Login & Security", icon: <FaLock /> },
  { value: "resume", label: "My Resume", icon: <FaUser /> },
];

function LogoutPageDemo() {

  const [selected, setSelected] = useState("profile");

  return (
    <div>
  <NavBar active="home" onTabClick={(tab) => console.log("Clicked:", tab)} />      
  <SearchBar />
      <StatCard 
        icon={<FaCheckCircle className="w-5 h-5" />}
        value="+4"
        label="Accepted Applications"
      />
      <MobileTab
        options={options}
        selected={selected}
        onSelect={(val) => setSelected(val)}
      />
      
    </div>
  );
}

export default LogoutPageDemo;
