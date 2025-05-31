import React from "react";
import { MdInfo } from "react-icons/md";

export default function AlertNote({
  title,
  message,
  icon = <MdInfo size={25} color="white" />,
  bgColor = "#727272",
  textColor = "white"
}) {
  return (
    <div
      className="rounded-2xl px-6 py-5 my-4 flex items-start gap-4"
      style={{ backgroundColor: bgColor, color: textColor }}
      role="alert"
    >
      {/* Icon */}
      <span className="inline-flex items-center justify-center">
        {icon}
      </span>

      {/* Text Content */}
      <div>
        <h5 className="font mb-1">{title}</h5>
        <p className="font-light text-sm">{message}</p>
      </div>
    </div>
  );
}