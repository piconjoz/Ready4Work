import { MdCheck, MdClose, MdPerson } from "react-icons/md";

export default function ApplicantCard() {
  return (
    <div className="bg-white border border-[#e5e7eb] rounded-2xl p-6 mx-auto my-4">
      {/* Top row: Name, time, actions */}
      <div className="flex items-center justify-between mb-4">
        {/* Name and applied date */}
        <div>
          <div className="text-md font-medium">Product Development Technologist</div>
          <div className="text-sm text-gray-500">Published 17/05/2025 (40 Days Remaining) | Public</div>
        </div>
      </div>
      <hr className="border-t border-[#E5E5E5] mb-4" />
      <div>
         <div className="text-sm text-gray-500">Pending: 2</div>
          <div className="text-sm text-gray-500">Applicant: 1/10</div>
      </div>
        
    </div>
  );
}