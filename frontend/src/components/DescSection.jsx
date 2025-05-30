import React from "react";

export default function DescSection({ title, paragraphs = [] }) {
  return (
    <div className="bg-white border border-[#e5e7eb] rounded-2xl p-6 my-6">
      <h2 className="text-xl xl:text-2xl font-semibold mb-3">{title}</h2>
      {paragraphs.map((text, idx) => (
        <p key={idx} className="text-md mb-4 last:mb-0">
          {text}
        </p>
      ))}
    </div>
  );
}