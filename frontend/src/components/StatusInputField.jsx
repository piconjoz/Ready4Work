import React from "react";

export default function StatusInputField({
  label,
  name,
  type = "text",
  status = "default", // 'default' | 'error'
  errorMessage = "",
  autoComplete = "off",
}) {
  const isError = status === "error";

  return (
    <>
      <div className={`relative rounded-lg px-3 py-3 transition-colors group mt-4
          ${isError ? "bg-[#FFF9F7] border-[#D54B21]" : "bg-white border-[#D3D3D3]"}
          border`}>
        <label
          htmlFor={name}
          className={`block text-xs font-normal ${
            isError ? "text-[#D54B21]" : "text-[#B0B0B0] group-focus-within:text-[#7a7a7a]"
          }`}>
          {label}
        </label>
        <input
          type={type}
          name={name}
          id={name}
          className={`w-full bg-transparent text-sm focus:outline-none ${
            isError ? "text-gray-700" : ""
          }`}
          autoComplete={autoComplete}
        />
      </div>

      {/* Error Message */}
      {isError && errorMessage && (
        <div className="flex flex-row-reverse gap-1 mt-2 mb-2 text-[#D54B21]">
          {/* Exclamation icon */}
          <svg className="w-5 h-5" fill="currentColor" viewBox="0 0 24 24">
            <circle cx="12" cy="12" r="10" fill="#D54B21" />
            <rect x="11" y="7" width="2" height="6" rx="1" fill="#fff" />
            <circle cx="12" cy="16" r="1" fill="#fff" />
          </svg>
          <span className="text-sm underline cursor-pointer">{errorMessage}</span>
        </div>
      )}
    </>
  );
}