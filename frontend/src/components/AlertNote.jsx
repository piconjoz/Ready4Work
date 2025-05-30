import React from "react";
import { MdWarning } from "react-icons/md";

export default function AlertNote({ title, message }) {
  return (
    <div
      className="bg-[#727272] rounded-2xl text-white px-6 py-5 my-4 flex items-start gap-4"
      role="alert"
    >
      {/* Icon */}
      <span className="inline-flex items-center justify-center">
        <MdWarning size={25} color="white" />
      </span>

      {/* Text Content */}
      <div>
        <h5 className="font-medium mb-1">{title}</h5>
        <p className="text-sm">{message}</p>
      </div>
    </div>
  );
}