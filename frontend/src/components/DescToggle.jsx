import { useState } from "react";
import { MdCheck } from "react-icons/md"; // Material Design check icon

function Toggle({ checked, onChange }) {
  return (
    <label className="relative inline-flex items-center cursor-pointer">
      <input
        type="checkbox"
        checked={checked}
        onChange={onChange}
        className="sr-only peer"
      />
      {/* Track */}
      <div className="w-11 h-6 bg-gray-300 peer-focus:outline-none peer-focus:ring-2 peer-focus:ring-gray-500 rounded-full transition-colors peer-checked:bg-black" />
      {/* Thumb */}
      <div className="absolute left-0.5 top-0.5 w-5 h-5 bg-white border border-gray-300 rounded-full transition-transform peer-checked:translate-x-5 flex items-center justify-center">
        {checked && <MdCheck className="w-4 h-4 text-black" />}
      </div>
    </label>
  );
}

export default function DescToggle({ title, description, name, defaultChecked = true }) {
  const [checked, setChecked] = useState(defaultChecked);

  return (
    <div className="flex items-center justify-between">
      <div>
        <p className="text-sm">{title}</p>
        <p className="text-sm text-[#5E5E5E]">{description}</p>
      </div>
      <Toggle checked={checked} onChange={() => setChecked((v) => !v)} />
    </div>
  );
}