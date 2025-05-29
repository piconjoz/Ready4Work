import ReactLogo from "../assets/sit_com_ico.svg"; 

export default function JobCard() {
  return (
    <div className="bg-white border border-[#e5e7eb] rounded-2xl p-6 max-w-2xl mx-auto my-4">
      {/* Top row: logo and bookmark */}
      <div className="flex items-start justify-between mb-4">
        {/* Logo */}
        <img
          src={ReactLogo}
          alt="SIT Logo"
          className="w-10 h-10 object-contain rounded"
        />
        {/* Bookmark Icon */}
        <div className="w-10 h-10 flex items-center justify-center rounded-full bg-gray-100">
          {/* Replace with your icon library if you want! */}
          <svg className="w-6 h-6 text-gray-600" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24">
            <path strokeLinecap="round" strokeLinejoin="round" d="M6 4v16l6-4 6 4V4z" />
          </svg>
        </div>
      </div>

      {/* Badges row */}
        <div className="flex flex-wrap gap-x-3 gap-y-2 mb-4">
        {/* First pill */}
        <span className="inline-flex items-center rounded-full border border-gray-300 px-4 py-1.5 text-xs font-medium">
            $1500
        </span>
        {/* Second pill: different color */}
        <span className="inline-flex items-center rounded-full border border-gray-300 bg-[#F5F5F5] px-4 py-1.5 text-xs font-medium">
            10 Vacancies
        </span>
        {/* Third pill */}
        <span className="inline-flex items-center rounded-full border border-gray-300 px-4 py-1.5 text-xs font-medium">
            01/09/2025
        </span>
        {/* Third pill */}
        <span className="inline-flex items-center rounded-full border bg-green-600 text-white px-4 py-1.5 text-xs font-medium">
        +85 Days
        </span>
        </div>

      {/* Title */}
      <div className="mb-4">
        <h3 className="text-lg font-semibold">
          Student Standardised Patient (PTY1015 Musculoskeletal Physiotherapy 1 - AY24 Tri 3)
        </h3>
      </div>

      {/* Description */}
      <div className="mb-6">
        <div className="text">Singapore Institute Of Technology</div>
        <div className="text">SIT Student Work Scheme</div>
      </div>

      {/* Button */}
      <button type="button" className="w-full bg-black text-white rounded-lg py-3 hover:bg-gray-900 transition">
        View Details
      </button>
    </div>
  );
}