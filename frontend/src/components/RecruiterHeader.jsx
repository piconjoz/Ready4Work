import React from "react";
import RecruiterNavBar from "./RecruiterNavBar";
import JobSearchBar from "./SearchBar";

export default function RecruiterHeader({ active, onTabClick }) {
  return (
    <div className="bg-[#F8F9FD]">
      <RecruiterNavBar active={active} onTabClick={onTabClick} />
      <JobSearchBar />
    </div>
  );
}