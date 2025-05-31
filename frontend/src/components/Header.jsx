import React from "react";
import NavBar from "./NavBar";
import JobSearchBar from "./SearchBar";

export default function Header({ active, onTabClick }) {
  return (
    <div className="bg-[#F8F9FD]">
      <NavBar active={active} onTabClick={onTabClick} />
      <JobSearchBar />
    </div>
  );
}