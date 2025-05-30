import React from "react";

export default function NoticeBanner({ title, message, onClose }) {
  return (
    <div className="relative w-full mx-auto border border-[#D3D3D3] rounded-2xl bg-white px-6 py-5 mb-6">
      <button
        type="button"
        aria-label="Close notice"
        className="absolute top-4 right-4 p-1 rounded hover:bg-gray-100 transition"
        onClick={onClose}
      >
        <svg
          xmlns="http://www.w3.org/2000/svg"
          className="h-6 w-6 text-black"
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
          strokeWidth={2}
        >
          <path
            strokeLinecap="round"
            strokeLinejoin="round"
            d="M6 18L18 6M6 6l12 12"
          />
        </svg>
      </button>
      <h5 className="font-medium text-black mb-2">{title}</h5>
      <p className="text-sm text-black">{message}</p>
    </div>
  );
}