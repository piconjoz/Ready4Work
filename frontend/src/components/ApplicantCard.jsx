import { MdCheck, MdClose, MdPerson } from "react-icons/md";

export default function ApplicantCard() {
  return (
    <div className="bg-white border border-[#e5e7eb] rounded-2xl p-6 mx-auto my-4">
      {/* Top row: Name, time, actions */}
      <div className="flex items-center justify-between mb-4">
        {/* Name and applied date */}
        <div>
          <div className="text-md font-medium">Natalie Goh</div>
          <div className="text-sm text-gray-500">Applied 18/05/25 13:59</div>
        </div>
        {/* Actions */}
        <div className="flex gap-3">
          <button
            type="button"
            className="w-10 h-10 rounded-full bg-[#F3F3F3] flex items-center justify-center"
            aria-label="Approve">
            <MdCheck size={22} />
          </button>
          <button
            type="button"
            className="w-10 h-10 rounded-full bg-[#F3F3F3] flex items-center justify-center"
            aria-label="Reject">
            <MdClose size={22} />
          </button>
        </div>
      </div>
      <hr className="border-t border-[#E5E5E5] mb-4" />

      {/* Resume icon */}

         <div className="flex flex-wrap gap-x-3 gap-y-2 mb-4">
            <button type="button" className="w-12 h-12 rounded-lg bg-[#F3F3F3] flex items-center justify-center">
                <MdPerson size={24} className="text-black" />
            </button>
            <span className="inline-flex items-center rounded-full border border-gray-300 px-4 my-2 h-8 bg-green-600  text-xs text-white font-medium">
                Accepted
            </span>
         </div>
        
    </div>
  );
}