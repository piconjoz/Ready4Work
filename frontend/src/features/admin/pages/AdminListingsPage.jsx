import AdminHeader from "../../../components/AdminHeader";
import TableEntry from "../../../components/TableEntry";
import React from "react";



export default function AdminListingsPage() {
  const listings = [
    {
      name: "Food Process Engineer Research Assistant",
      organisation: "Singapore Institute of Technology",
      email: "hr@pokka",
      status: "Published",
      avatar: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQiZG8aaViLv7di4yE8HU162B8BNTsCMv7_7w&s"
    },
    {
      name: "Innovation Engineer (Food Technology)",
      organisation: "Singapore Institute of Technology",
      email: "hr@pokka.sg",
      status: "Published",
      avatar: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQiZG8aaViLv7di4yE8HU162B8BNTsCMv7_7w&s"
    }
  ];

  const columns = [
    {
      header: "Title",
      accessor: "name",
      render: (entry) => (
        <div className="flex items-center mr-5">
          <img className="w-10 h-10 rounded-full" src={entry.avatar} alt={`${entry.name} logo`} />
          <div className="ps-3 min-w-0">
            <div className="text-base text-black font-medium">{entry.name}</div>
            <div className="font-normal text-gray-500">{entry.organisation}</div>
          </div>
        </div>
      )
    },
    { header: "Email", accessor: "email" },
    { header: "Status", accessor: "status" },
    {
      header: "Visibility",
      accessor: "",
      render: (entry) => (
        <button onClick={() => console.log("Delete", entry)} className="font-medium text-red-600 dark:text-red-500 hover:underline">
          Delete Listing
        </button>
      )
    }
  ];

  return (
    <div className="min-h-screen bg-[#F8F9FD] px-6 md:px-10">
      <AdminHeader active="listings" />
      <div className="pt-6">
        <h1 className="text-2xl font-semibold mb-5">All Listings</h1>
        <TableEntry
          entries={listings}
          columns={columns}
          onSearchChange={(term) => console.log("Search term:", term)}
          onEdit={(listing) => console.log("Edit listing:", listing)}
        />
      </div>
    </div>
  );
}