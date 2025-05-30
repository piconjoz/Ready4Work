import React from "react";
import { FaCheckCircle } from "react-icons/fa";

export default function StatCard({ icon, value, label }) {
  return (
    <div className="rounded-xl border border-gray-200 bg-white px-4 py-5 w-48">
      {/* Icon */}
      <div className="text-black mb-2">
        {icon}
      </div>

      {/* Value */}
      <div className="text-lg font-medium text-black">
        {value}
      </div>

      {/* Label */}
      <div className="text-sm text-gray-700">
        {label}
      </div>
    </div>
  );
}