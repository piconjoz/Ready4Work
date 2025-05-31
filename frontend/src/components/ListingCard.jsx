export default function ListingCard({
  title = "Listing Title",
  publishedDate = "17/05/2025",
  daysRemaining = 40,
  visibility = "Public",
  pending = 2,
  applicants = 1,
  maxApplicants = 10,
}) {
  return (
    <div className="bg-white border border-[#e5e7eb] rounded-2xl p-6">
      {/* Title and Metadata */}
      <div className="mb-4">
        <h3 className="text-md font-semibold mb-1 line-clamp-2 min-h-[3rem]">
          {title}
        </h3>
        <div className="text-sm text-gray-500">
          Published {publishedDate} ({daysRemaining} Days Remaining)
        </div>
      </div>

      <hr className="border-t border-[#E5E5E5] mb-4" />

      {/* Status Counts */}
      <div>
        <div className="text-sm text-gray-500">Pending: {pending}</div>
        <div className="text-sm text-gray-500">
          Applicant: {applicants}/{maxApplicants}
        </div>
        <div className="text-sm text-gray-500">Visibility: {visibility}</div>
      </div>
    </div>
  );
}