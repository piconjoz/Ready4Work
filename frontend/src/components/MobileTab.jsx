import React, { useState } from "react";
import { FaAngleDown } from "react-icons/fa6";

export default function MobileTab({ options = [], selected, onSelect }) {
  const [open, setOpen] = useState(false);

  const selectedItem = options.find((opt) => opt.value === selected);

  return (
    <div className="relative w-64">
      <button
        onClick={() => setOpen(!open)}
        className="w-full flex items-center justify-between border border-gray-300 rounded-lg px-4 py-3 bg-white shadow-xs text-sm text-gray-800">
        <div className="flex items-center gap-2">
          {selectedItem?.icon}
          <span className="font-medium">{selectedItem?.label}</span>
        </div>
        <span className="text-gray-500">
            <FaAngleDown />
        </span>
      </button>

      <div className={`absolute z-10 w-full mt-1 bg-white border border-gray-200 rounded-lg overflow-hidden transition-all duration-300 ease-in-out ${
          open ? "opacity-100 max-h-96 scale-100" : "opacity-0 max-h-0 scale-95"
        }`}>
        {options.map((option) => (
          <button
            key={option.value}
            onClick={() => {
              onSelect(option.value);
              setOpen(false);
            }}
            className={`w-full text-left px-4 py-2 flex items-center gap-2 hover:bg-gray-100 text-sm text-gray-800 ${
              selected === option.value ? "font-medium" : "font-normal"
            }`}>
            {option.icon}
            {option.label}
          </button>
        ))}
      </div>
    </div>
  );
}