import React from "react";

export default function EmptyCard({ text, className = "" }) {
  return (
    <div className={`bg-white border border-[#e5e7eb] rounded-2xl p-6 mx-auto my-4 flex flex-col justify-center w-full h-80 ${className}`}>
      <span className="text-dark text-md font-medium text-center w-full">{text}</span>
    </div>
  );
}