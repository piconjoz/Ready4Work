import React, { useState, useRef, useEffect } from "react";

export default function StatusInputField({
  label,
  name,
  type = "text",
  status = "default",
  errorMessage = "",
  autoComplete = "off",
  value,
  defaultValue = "",
  onChange,
  onBlur,
  className = "",
  readOnly = false,
}) {
  const isError = status === "error";
  const [internalValue, setInternalValue] = useState("");

  const handleChange = (e) => {
    if (readOnly) return;
    if (onChange) {
      onChange(e);
    } else {
      setInternalValue(e.target.value);
    }
  };

  const handleBlur = (e) => {
    if (onBlur) {
      onBlur(e);
    }
  };

  const textAreaRef = useRef(null);
  const initial =
    value !== undefined
      ? value
      : defaultValue !== undefined
        ? defaultValue
        : internalValue;

  useEffect(() => {
    if (type === "textarea" && textAreaRef.current) {
      textAreaRef.current.style.height = "auto";
      textAreaRef.current.style.height = `${textAreaRef.current.scrollHeight}px`;
    }
  }, [initial, type]);

  return (
    <div className={className}>
      <div
        className={`relative rounded-lg px-3 py-3 transition-colors group mt-4 ${
          isError
            ? "bg-[#FFF9F7] border-[#D54B21]"
            : readOnly
              ? "bg-gray-50 border-[#D3D3D3]"
              : "bg-white border-[#D3D3D3]"
        } border`}
      >
        <label
          htmlFor={name}
          className={`block text-xs font-normal ${
            isError
              ? "text-[#D54B21]"
              : readOnly
                ? "text-gray-500"
                : "text-[#B0B0B0] group-focus-within:text-[#7a7a7a]"
          }`}
        >
          {label}
        </label>
        {type === "textarea" ? (
          <textarea
            ref={textAreaRef}
            name={name}
            id={name}
            value={initial}
            onChange={handleChange}
            onBlur={handleBlur}
            readOnly={readOnly}
            className={`w-full bg-transparent text-sm focus:outline-none resize-none overflow-hidden ${
              readOnly ? "cursor-default text-gray-600" : ""
            }`}
            autoComplete={autoComplete}
          />
        ) : (
          <input
            type={type}
            name={name}
            id={name}
            value={initial}
            onChange={handleChange}
            onBlur={handleBlur}
            readOnly={readOnly}
            className={`w-full bg-transparent text-sm focus:outline-none ${
              isError ? "text-gray-700" : ""
            } ${readOnly ? "cursor-default text-gray-600" : ""}`}
            autoComplete={autoComplete}
          />
        )}
      </div>

      {/* Fixed height container for error messages */}
      <div className="mt-2 mb-2">
        {isError && errorMessage && (
          <div className="flex items-center gap-1 text-[#D54B21]">
            <svg className="w-4 h-4" fill="currentColor" viewBox="0 0 24 24">
              <circle cx="12" cy="12" r="10" fill="#D54B21" />
              <rect x="11" y="7" width="2" height="6" rx="1" fill="#fff" />
              <circle cx="12" cy="16" r="1" fill="#fff" />
            </svg>
            <span className="text-xs">{errorMessage}</span>
          </div>
        )}
      </div>
    </div>
  );
}
