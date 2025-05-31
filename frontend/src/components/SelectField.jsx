import React from "react";

export default function SelectField({ label, name, value, onChange, options = [] }) {
  return (
    <div className="relative bg-white rounded-lg border border-[#D3D3D3] focus-within:border-[#7a7a7a] px-3 py-3 transition-colors group my-4">
      <label
        htmlFor={name}
        className="block text-xs font-normal text-[#B0B0B0] group-focus-within:text-[#7a7a7a]"
      >
        {label}
      </label>
      <select
        id={name}
        name={name}
        value={value}
        onChange={onChange}
        className="w-full bg-transparent text-sm focus:outline-none appearance-none pr-8">
        {options.map((opt) => (
          <option key={opt.value} value={opt.value}>
            {opt.label}
          </option>
        ))}
      </select>
      <span className="pointer-events-none absolute right-4 top-1/2 transform -translate-y-1/2 text-[#5E5E5E]">
        <svg width="20" height="20" fill="none" viewBox="0 0 24 24">
          <path
            d="M7 10l5 5 5-5"
            stroke="#444"
            strokeWidth="2"
            strokeLinecap="round"
            strokeLinejoin="round"
          />
        </svg>
      </span>
    </div>
  );
}