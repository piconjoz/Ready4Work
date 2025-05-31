import React, { useState } from "react";
import { MdCheck, MdClose, MdPerson } from "react-icons/md";
import ResumeModal from "./ResumeModal"; // Adjust if needed

export default function ApplicantCard({
  name = "Natalie Goh",
  appliedAt = "18/05/25 13:59",
  status = "",
  statusColor = "",
  resumeDetails = null,
}) {
  const [showModal, setShowModal] = useState(false);

  return (
    <>
      <div className="bg-white border border-[#e5e7eb] rounded-2xl p-6">
        {/* Top: Name and Action */}
        <div className="flex items-center justify-between mb-4">
          <div>
            <div className="text-md font-medium">{name}</div>
            <div className="text-sm text-gray-500">Applied {appliedAt}</div>
          </div>
          {status !== "Offered" && status !== "Accepted" && status !== "Rejected" && (
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
                aria-label="Reject" >
                <MdClose size={22} />
              </button>
            </div>
          )}
        </div>

        <hr className="border-t border-[#E5E5E5] mb-4" />

        {/* Resume + Status */}
        <div className="flex flex-wrap gap-x-3 gap-y-2 mb-4">
    {status !== "Rejected" && (
      <button
        type="button"
        onClick={() => setShowModal(true)}
        className="w-12 h-12 rounded-lg bg-[#F3F3F3] flex items-center justify-center">
        <MdPerson size={24} className="text-black" />
      </button>
    )}
          {status && (
            <span
              className={`inline-flex items-center rounded-full border border-gray-300 px-4 my-2 h-8 text-xs text-white font-medium ${statusColor}`}>
              {status}
            </span>
          )}
        </div>
      </div>

      {/* Resume Modal */}
      {showModal && (
        <ResumeModal
          onClose={() => setShowModal(false)}
          {...resumeDetails} // Pass in additional modal props if needed
        />
      )}
    </>
  );
}