import React, { useState } from "react";

export default function TableEntry({
  entries = [],
  onSearchChange = () => {},
  onEdit = () => {},
  dropdownItems = [],
  columns = [],
}) {
  const [filterOpen, setFilterOpen] = useState(false);
  return (
    <div className="relative overflow-x-auto sm:rounded-lg">
      <div className="flex items-center justify-between flex-wrap gap-4 md:gap-0 pb-4">
        <div className="flex flex-wrap gap-4 items-center">
          {/* Dropdown */}
          <div className="relative inline-block text-left">
            <button
              onClick={() => setFilterOpen((prev) => !prev)}
              className="inline-flex items-center text-gray-500 border border-gray-300 focus:outline-none hover:bg-gray-100 focus:ring-4 focus:ring-gray-100 font-medium rounded-lg text-sm px-3 py-1.5"
              type="button"
            >
              <span className="sr-only">Filter button</span>
              Filter
              <svg
                className="w-2.5 h-2.5 ms-2.5"
                aria-hidden="true"
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 10 6"
              >
                <path
                  stroke="currentColor"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth="2"
                  d="m1 1 4 4 4-4"
                />
              </svg>
            </button>
            <div
              id="dropdownFilter"
              className={`absolute mt-2 left-0 z-50 bg-white border border-gray-200 divide-y divide-gray-100 rounded-lg w-44 dark:bg-gray-700 dark:divide-gray-600 ${
                filterOpen ? "block" : "hidden"
              }`}
            >
              <form className="py-1 px-2 text-sm text-gray-700 dark:text-gray-200" aria-labelledby="dropdownFilterButton">
                <label className="flex items-center gap-2 py-2 px-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white rounded-md">
                  <input type="checkbox" name="applicant" className="accent-blue-600" />
                  Applicant
                </label>
                <label className="flex items-center gap-2 py-2 px-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white rounded-md">
                  <input type="checkbox" name="recruiter" className="accent-blue-600" />
                  Recruiter
                </label>
              </form>
            </div>
          </div>

          {/* Search */}
          <label htmlFor="table-search" className="sr-only">
            Search
          </label>
          <div className="relative">
            <div className="absolute inset-y-0 start-0 flex items-center ps-3 pointer-events-none">
              <svg
                className="w-4 h-4 text-gray-500 dark:text-gray-400"
                aria-hidden="true"
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 20 20"
              >
                <path
                  stroke="currentColor"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth="2"
                  d="m19 19-4-4m0-7A7 7 0 1 1 1 8a7 7 0 0 1 14 0Z"
                />
              </svg>
            </div>
            <input
              type="text"
              id="table-search-users"
              className="block p-2 ps-10 text-sm text-gray-900 border border-gray-300 rounded-lg w-80 bg-gray-50 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
              placeholder="Search for users"
              onChange={(e) => onSearchChange(e.target.value)}
            />
          </div>
        </div>
      </div>

      {/* Table */}
      <table className="w-full text-sm text-left text-gray-500 dark:text-gray-400">
        <thead className="text-xs text-black uppercase bg-gray-50">
          <tr>
            {columns.map((col, idx) => (
              <th
                key={idx}
                className={`px-6 py-3 text-left whitespace-nowrap ${col.className ?? ''}`}
              >
                {col.header}
              </th>
            ))}
          </tr>
        </thead>
        <tbody>
          {entries.map((entry, idx) => (
            <tr
              key={idx}
              className="border-b border-gray-300 hover:bg-gray-50"
            >
              {columns.map((col, cIdx) => (
                <td
                  key={cIdx}
                  className={`px-6 py-4 text-left whitespace-nowrap ${col.className ?? ''}`}
                >
                  {col.render ? col.render(entry) : entry[col.accessor]}
                </td>
              ))}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}