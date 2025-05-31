import { useState, useRef } from "react";

export default function SimpleAccordion({ items = [] }) {
  const [openItems, setOpenItems] = useState([]);
  const refs = useRef([]);

  const toggleItem = (idx) => {
    setOpenItems((prev) =>
      prev.includes(idx) ? prev.filter((i) => i !== idx) : [...prev, idx]
    );
  };

  return (
    <div className="my-8 mx-auto">
      {items.map((faq, idx) => {
        const isOpen = openItems.includes(idx);
        return (
          <div
            key={idx}
            className={`border border-gray-200 rounded-xl transition ${
              isOpen ? "bg-gray-50" : ""
            } ${idx !== items.length - 1 ? "mb-3" : ""}`}
            role="button"
            tabIndex={0}
            onClick={() => toggleItem(idx)}
            onKeyPress={(e) => {
              if (e.key === "Enter" || e.key === " ") toggleItem(idx);
            }}
          >
            <div className="w-full text-left px-6 pt-4 rounded-xl font-semibold cursor-pointer focus:outline-none focus-visible:ring">
              {faq.question}
            </div>
            <div
              ref={(el) => (refs.current[idx] = el)}
              style={{
                maxHeight: isOpen
                  ? `${refs.current[idx]?.scrollHeight || 200}px`
                  : "0px",
                opacity: isOpen ? 1 : 0,
                overflow: "hidden",
                transition:
                  "max-height 0.35s cubic-bezier(0.4,0,0.2,1), opacity 0.2s"
              }}
              className="px-6 pb-5 text-sm text-gray-700"
            >
              {faq.answer}
            </div>
          </div>
        );
      })}
    </div>
  );
}