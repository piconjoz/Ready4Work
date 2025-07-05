import React from 'react';
import { useState } from "react";
// import { FaRegBookmark, FaBookmark } from "react-icons/fa";
import { MdOutlineBookmark, MdOutlineBookmarkBorder } from "react-icons/md";


export default function JobCard({
  logo,
  title,
  employer,
  scheme,
  badges = [],
  daysRemaining = "",
  status = "",
  onClick = () => {},
  secondaryActions = [],
}) {
  const [bookmarked, setBookmarked] = useState(false);
  const toggleBookmark = () => setBookmarked(prev => !prev);
  return (
    <div className="bg-white border border-[#e5e7eb] rounded-2xl p-6  mx-auto my-4 flex flex-col w-full h-full">
      <div className="flex-grow flex flex-col">
        {/* Top row: logo and bookmark */}
        <div className="flex items-start justify-between mb-4">
          <img
            src={logo}
            alt="Company Logo"
            className="w-10 h-10 object-contain rounded"
          />
          <div
            className={`w-10 h-10 flex items-center justify-center rounded-full ${
              !bookmarked ? "bg-gray-100" : ""
            }`}
          >
            <button onClick={toggleBookmark} className="focus:outline-none">
              {bookmarked
                ? <MdOutlineBookmarkBorder className="w-6 h-6 text-gray-600" />
                : <MdOutlineBookmark className="w-6 h-6 text-gray-600" />}
            </button>
          </div>
        </div>

        {/* Badges */}
        <div className="flex flex-wrap gap-x-3 gap-y-2 mb-4">
          {badges.map((badge, idx) => (
            <span
              key={idx}
              className={`inline-flex items-center rounded-full border px-4 py-1.5 text-xs font-medium ${
                badge.color || "border-gray-300 bg-white"
              }`}
            >
              {badge.label}
            </span>
          ))}
          {status === "applied" && (
            <span className="inline-flex items-center rounded-full bg-green-600 text-white px-4 py-1.5 text-xs font-medium">
              Applied
            </span>
          )}
          {status === "" && daysRemaining !== "" && daysRemaining < 7 && (
            <span className="inline-flex items-center rounded-full bg-orange-600 text-white px-4 py-1.5 text-xs font-medium">
              Expired
            </span>
          )}
          {/* {daysRemaining && (
            <span className="inline-flex items-center rounded-full bg-green-600 text-white px-4 py-1.5 text-xs font-medium">
              +{daysRemaining} Days
            </span>
          )} */}
        </div>

        {/* Title */}
        <h3 className="text-lg font-semibold mb-4 line-clamp-2 min-h-[56px]">
          {title}
        </h3>

        {/* Description */}
        <div className="text-sm text-gray-700 space-y-1 h-12 mb-3 overflow-hidden">
          <div>{employer}</div>
          <div>{scheme}</div>
        </div>
      </div>

      {/* Action */}
      <button
        type="button"
        onClick={onClick}
        className="w-full bg-black text-white rounded-lg py-2 text-sm hover:bg-gray-900 transition">
        View Details
      </button>
      {secondaryActions.map((action, idx) => (
        <button
          key={idx}
          type="button"
          onClick={action.onClick}
          className={`w-full mt-2 text-white rounded-lg py-2 text-sm transition ${
            action.color || "bg-gray-500 hover:bg-gray-600"
          }`}
        >
          {action.label}
        </button>
      ))}
    </div>
  );
}