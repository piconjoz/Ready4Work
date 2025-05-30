import React from "react";
import { FaSearch } from "react-icons/fa";

export default function JobSearchBar() {
  const handleSubmit = (e) => {
    e.preventDefault();
    // Do your search logic here (e.g., console.log or navigate)
    console.log("Search triggered");
  };

  return (
    <form
      onSubmit={handleSubmit}
      className="flex items-center justify-between border border-gray-300 bg-white rounded-full px-5 py-3 w-full shadow-sm cursor-pointer"
    >
      {/* Label and input section */}
      <div className="flex flex-col w-full">
        <label className="text-xs font-semibold text-gray-800">Jobs</label>
        <input
          type="text"
          placeholder="Search Job Listings"
          className="text-sm text-gray-600 placeholder-gray-400 outline-none bg-transparent"
        />
      </div>

      {/* Submit button */}
      <button
        type="submit"
        className="bg-red-600 hover:bg-red-700 text-white p-3 rounded-full transition"
      >
        <FaSearch size={14} />
      </button>
    </form>
  );
}