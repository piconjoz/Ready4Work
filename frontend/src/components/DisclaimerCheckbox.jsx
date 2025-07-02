import React from "react";

export default function DisclaimerCheckbox({
  title,
  description,
  name,
  checked,
  onChange,
}) {
  return (
    <div className="mt-10 mb-10 flex items-center justify-between">
      {/* Informant Text */}
      <div className="pr-5">
        <p className="text-md">{title}</p>
        <p className="text-sm text-[#5E5E5E]">{description}</p>
      </div>

      {/* Checkbox */}
      <label className="flex items-center mr-2">
        <input
          type="checkbox"
          name={name}
          className="accent-black scale-150"
          checked={checked}
          onChange={onChange}
        />
      </label>
    </div>
  );
}
