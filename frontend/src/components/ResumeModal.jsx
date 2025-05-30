import React, { useState } from "react";
import { MdPerson } from "react-icons/md";
import { FaAngleDown } from "react-icons/fa6";

export default function ResumeModal({ onClose }) {
  const [openSections, setOpenSections] = useState(["summary"]);

  const sections = [
    {
      id: "summary",
      title: "Professional Summary",
      content: (
        <p className="mt-2 text-sm text-gray-700">
          Detail-oriented Food Technology graduate with a passion for product development
          and hands-on experience in sensory testing. Seeking a role to apply my skills
          in real-world food innovation.
        </p>
      ),
    },
    { id: "education", title: "Education" },
    { id: "experience", title: "Work Experience / Internships" },
    { id: "skills", title: "Skills" },
    { id: "certifications", title: "Certifications / Training" },
    { id: "activities", title: "Leadership & Activities" },
  ];

  const toggle = (id) => {
    setOpenSections((prev) => {
      if (prev.includes(id)) {
        return prev.filter((sectionId) => sectionId !== id);
      } else {
        return [...prev, id];
      }
    });
  };

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/60 px-4">
      <div className="bg-white w-full max-w-md sm:max-w-lg md:max-w-xl lg:max-w-2xl max-h-[90vh] rounded-xl p-6 overflow-y-auto shadow-xl">
        {/* Header */}
        <div className="flex justify-between items-center mb-4">
          <h2 className="text-xl font-bold">Natalie Goh</h2>
          <button onClick={onClose} className="bg-black text-white px-4 py-1 rounded-md">
            Done
          </button>
        </div>

        {/* Sections */}
        <div className="space-y-4">
          {sections.map((section) => (
            <div
              key={section.id}
              onClick={() => toggle(section.id)}
              className="rounded border border-gray-300 transition-all duration-300 ease-in-out cursor-pointer">
              <div
                type="button"
                className="w-full flex items-center justify-between p-3 font-medium text-left">
                <span>{section.title}</span>
                <FaAngleDown
                  className={`transition-transform duration-200 ${
                    openSections.includes(section.id) ? "rotate-180" : "rotate-0"
                  }`}
                />
              </div>
              <div
                className={`grid transition-all duration-300 ease-in-out ${
                  openSections.includes(section.id) ? "grid-rows-[1fr] opacity-100 px-3 pb-3" : "grid-rows-[0fr] opacity-0 px-3"
                }`}>
                <div className="overflow-hidden text-sm text-gray-700">
                  {section.content || <p>Content for {section.title}</p>}
                </div>
              </div>
            </div>
          ))}
        </div>

        {/* Download Button */}
        <div className="mt-6">
          <button
            type="button"
            className="w-12 h-12 rounded-lg bg-[#F3F3F3] flex items-center justify-center">
            <MdPerson size={24} className="text-black" />
          </button>
        </div>
      </div>
    </div>
  );
}