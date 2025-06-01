import AdminHeader from "../../../components/AdminHeader";
import TableEntry from "../../../components/TableEntry";
import React, { useState } from "react";


export default function AdminUsersPage() {

  const users = [
  {
    name: "Neil Sims",
    type: "Recruiter",
    email: "neil.sims@sit.singaporetech.edu.sg",
    status: "Online",
    avatar: "https://store.pastelkeyboard.com/wp-content/uploads/2018/01/icon_PompompurinHotcake.png",
  },
  {
    name: "Bonnie Green",
    type: "Recruiter",
    email: "bonnie@sit.singaporetech.edu.sg",
    status: "Online",
    avatar: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRS2Jxy7831CuUKqT5PeZ8TBxtssVd-sG1hsw&s",
  },
  {
    name: "Jese Leos",
    type: "Applicant",
    email: "jese@sit.singaporetech.edu.sg",
    status: "Online",
    avatar: "https://i.pinimg.com/236x/03/06/3b/03063bf77829697137703afdf8a95a1d.jpg",
  },
  {
    name: "Lawrence Wong",
    type: "Applicant",
    email: "lawrence@wong.com",
    status: "Online",
    avatar: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4hHpExKvT3qQEC4f645oNl33yJQ0it5iB1A&s",
  },
  {
    name: "Donald Trump",
    type: "Recruiter",
    email: "donald@trump.com",
    status: "Offline",
    avatar: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR-Hpm0spAoB_rTrUbH2GH3fn_Z6X6cWl8ZNA&s",
  },
];

  const columns = [
    {
      header: "Name",
      accessor: "name",
      render: (entry) => (
        <div className="flex items-center mr-5">
          <img className="w-10 h-10 rounded-full" src={entry.avatar} alt={`${entry.name} avatar`} />
          <div className="ps-3">
            <div className="text-base text-black font-medium">{entry.name}</div>
            <div className="font-normal text-gray-500">{entry.type}</div>
          </div>
        </div>
      )
    },
    { header: "Email", accessor: "email" },
    {
      header: "Visibility",
      accessor: "status",
      render: (entry) => (
        <button onClick={() => console.log("Delete", entry)} className="font-medium text-red-600 dark:text-red-500 hover:underline">
          Delete Account
        </button>
      )
    },
    {
      header: "Access",
      accessor: "",
      render: (entry) => (
        <button onClick={() => console.log("Edit", entry)} className="font-medium text-blue-600 dark:text-blue-500 hover:underline">
          Reset Password
        </button>
      )
    }
  ];

  const handleSearch = (searchTerm) => {
    console.log("Search term:", searchTerm);
    // Implement search logic here
  };
  return (
    <div className="min-h-screen bg-[#F8F9FD] px-6 md:px-10">
      <AdminHeader active="users" />

      {/* Overview */}
      <div className="pt-6">
        <h1 className="text-2xl font-semibold mb-5">All Users</h1>
        <TableEntry
          entries={users}
          columns={columns}
          onSearchChange={handleSearch}
          onEdit={(user) => console.log("Edit", user)}
        />
      </div>
    </div>
  );
}