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
    <div className="bg-white border border-[#e5e7eb] rounded-2xl p-6 mx-auto my-4">
      {/* Title and Metadata */}
      <div className="flex items-center justify-between mb-4">
        <div>
          <div className="text-md font-medium">{title}</div>
          <div className="text-sm text-gray-500">
            Published {publishedDate} ({daysRemaining} Days Remaining) | {visibility}
          </div>
        </div>
      </div>

      <hr className="border-t border-[#E5E5E5] mb-4" />

      {/* Status Counts */}
      <div>
        <div className="text-sm text-gray-500">Pending: {pending}</div>
        <div className="text-sm text-gray-500">
          Applicant: {applicants}/{maxApplicants}
        </div>
      </div>
    </div>
  );
}