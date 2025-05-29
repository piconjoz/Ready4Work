// frontend/src/components/SearchFilter.jsx
import React, { useState } from "react";

export default function SearchFilter() {
  const [open, setOpen] = useState(false);

  return (
    <div className="relative inline-block w-full lg:w-auto text-left">
      {/* Toggle Button */}
      <button
        type="button"
        onClick={() => setOpen(!open)}
        className="w-full lg:w-auto px-4 py-2 text-white bg-gray-800 rounded-lg hover:bg-gray-900 focus:ring-2 focus:ring-gray-500"
      >
        Filter
      </button>

      {/* Drop-down Panel */}
      {open && (
        <div className="absolute right-0 z-10 mt-2 w-full lg:w-80 bg-white rounded-lg shadow-lg p-6 space-y-6">
          <h2 className="text-xl font-semibold text-gray-900">Filter By</h2>

          {/* Listing Date */}
          <div>
            <h3 className="mb-2 text-sm font-medium text-gray-500">Listing Date</h3>
            <fieldset className="space-y-3">
              {[
                { id: "date-all", label: "All time" },
                { id: "date-week", label: "Past week" },
                { id: "date-month", label: "Past month" },
                { id: "date-custom", label: "Custom (01/01/2025–05/05/2025)" },
              ].map(({ id, label }) => (
                <div className="flex items-center" key={id}>
                  <input
                    id={id}
                    name="listing-date"
                    type="radio"
                    className="w-4 h-4 text-blue-600 border-gray-300 focus:ring-blue-500"
                    defaultChecked={id === "date-custom"}
                  />
                  <label htmlFor={id} className="ml-3 text-sm font-medium text-gray-900">
                    {label}
                  </label>
                </div>
              ))}
            </fieldset>
          </div>

          {/* Employment Type */}
          <div>
            <h3 className="mb-2 text-sm font-medium text-gray-500">Employment Type</h3>
            <div className="space-y-3">
              {["SIT Student Work Scheme", "OWISP", "IIP", "IWSP"].map((type) => (
                <div className="flex items-center" key={type}>
                  <input
                    id={type}
                    type="checkbox"
                    className="w-4 h-4 text-blue-600 border-gray-300 rounded focus:ring-blue-500"
                  />
                  <label htmlFor={type} className="ml-3 text-sm font-medium text-gray-900">
                    {type}
                  </label>
                </div>
              ))}
            </div>
          </div>

          {/* Statuses */}
          <div>
            <h3 className="mb-2 text-sm font-medium text-gray-500">Statuses</h3>
            <div className="space-y-3">
              {["Expired", "Active"].map((status) => (
                <div className="flex items-center" key={status}>
                  <input
                    id={status}
                    type="checkbox"
                    className="w-4 h-4 text-blue-600 border-gray-300 rounded focus:ring-blue-500"
                  />
                  <label htmlFor={status} className="ml-3 text-sm font-medium text-gray-900">
                    {status}
                  </label>
                </div>
              ))}
            </div>
          </div>

          {/* Remuneration Type */}
          <div>
            <h3 className="mb-2 text-sm font-medium text-gray-500">Remuneration Type</h3>
            <fieldset className="space-y-3">
              {["Hourly", "Daily", "Session"].map((rem) => (
                <div className="flex items-center" key={rem}>
                  <input
                    id={rem}
                    name="remuneration"
                    type="radio"
                    className="w-4 h-4 text-blue-600 border-gray-300 focus:ring-blue-500"
                  />
                  <label htmlFor={rem} className="ml-3 text-sm font-medium text-gray-900">
                    {rem}
                  </label>
                </div>
              ))}
            </fieldset>
          </div>
        </div>
      )}
    </div>
  );
}