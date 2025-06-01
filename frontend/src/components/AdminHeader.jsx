import React from "react";
import AdminNavBar from "./AdminNavBar";

export default function AdminHeader({ active, onTabClick }) {
  return (
    <div className="bg-[#F8F9FD]">
      <AdminNavBar active={active} onTabClick={onTabClick} />
    </div>
  );
}