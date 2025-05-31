import React from "react";
import { FaCheckCircle } from "react-icons/fa";

export default function StatCard({ icon, value, label }) {
  return (
    <div className="rounded-xl border border-gray-200 bg-white px-4 py-5">
      {/* Icon */}
      <div className="text-black mb-2">
        {icon}
      </div>

      {/* Value */}
      <div className="text-lg font-medium text-black">
        {value}
      </div>

      {/* Label */}
    <div className="text-sm text-gray-700 text-left">
      {(() => {
        const words = label.split(" ");
        if (words.length >= 2) {
          // Split into two nearly equal lines
          const mid = Math.ceil(words.length / 2);
          const first = words.slice(0, mid).join(" ");
          const second = words.slice(mid).join(" ");
          return (
            <>
              <span>{first}</span>
              <br />
              <span>{second}</span>
            </>
          );
        }
        // For two words or less, just print as is
        return label;
      })()}
    </div>
    </div>
  );
}