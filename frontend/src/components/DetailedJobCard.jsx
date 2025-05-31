export default function DetailedJobCard({
  logo,
  title,
  employer,
  scheme,
  badges = [],
  daysRemaining = "",
  status = "",
  onClick = () => {},
}) {
  return (
    <div className="bg-white border border-[#e5e7eb] rounded-2xl p-6  mx-auto my-4 flex flex-col w-full h-full">
      <div className="flex-grow flex flex-col">
        {/* Top row: logo and bookmark */}
        <div className="flex items-start justify-between mb-4">
          <img
            src={logo}
            alt="Company Logo"
            className="w-10 h-10 object-contain rounded"
          />
          <div className="w-10 h-10 flex items-center justify-center rounded-full bg-gray-100">
            <svg className="w-6 h-6 text-gray-600" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" d="M6 4v16l6-4 6 4V4z" />
            </svg>
          </div>
        </div>

        {/* Badges */}
        <div className="flex flex-wrap gap-x-3 gap-y-2 mb-4">
          {badges.map((badge, idx) => (
            <span
              key={idx}
              className={`inline-flex items-center rounded-full border px-4 py-1.5 text-xs font-medium ${
                badge.color || "border-gray-300 bg-white"
              }`}
            >
              {badge.label}
            </span>
          ))}
          {status === "applied" && (
            <span className="inline-flex items-center rounded-full bg-green-600 text-white px-4 py-1.5 text-xs font-medium">
              Applied
            </span>
          )}
          {status === "" && daysRemaining !== "" && daysRemaining < 7 && (
            <span className="inline-flex items-center rounded-full bg-orange-600 text-white px-4 py-1.5 text-xs font-medium">
              Expiring
            </span>
          )}
          {/* {daysRemaining && (
            <span className="inline-flex items-center rounded-full bg-green-600 text-white px-4 py-1.5 text-xs font-medium">
              +{daysRemaining} Days
            </span>
          )} */}
        </div>

        {/* Title */}
        <h3 className="text-lg font-semibold mb-4 line-clamp-2 min-h-[56px]">
          {title}
        </h3>

        {/* Description */}
        <div className="text-sm text-gray-700 space-y-1 h-12 overflow-hidden">
          <div>{employer}</div>
          <div>{scheme}</div>
        </div>

        {/* Action */}
        <button
            type="button"
            onClick={onClick}
            className="w-full bg-black text-white rounded-lg py-2 text-sm mt-5 hover:bg-gray-900 transition">
            Apply
        </button>
      </div>
    </div>
  );
}