import { MdCheck } from "react-icons/md"; // Material Design check icon

function Toggle({ checked, onChange }) {
  return (
    <label className="relative inline-flex items-center cursor-pointer">
      <input
        type="checkbox"
        checked={checked}
        onChange={() => onChange(!checked)} // Explicit toggle
        className="sr-only peer"
      />
      {/* Track and Thumb */}
      <div className="w-11 h-6 bg-gray-300 rounded-full peer-focus:ring-2 peer-focus:ring-gray-500 peer-checked:bg-black transition-colors duration-300 relative">
        <div className={`absolute top-0.5 ${checked ? 'right-0.5' : 'left-0.5'} w-5 h-5 bg-white border border-gray-300 rounded-full transition-all duration-300 transform flex items-center justify-center`}>
          {checked && <MdCheck className="w-4 h-4 text-black" />}
        </div>
      </div>
    </label>
  );
}

export default Toggle; // Export Toggle directly