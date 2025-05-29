import { useState, useRef } from "react";

const faqs = [
  {
    question: "How do I create an account?",
    answer: 'Click the "Sign Up" button in the top right corner and follow the registration process.'
  },
  {
    question: "I forgot my password. What should I do?",
    answer: "Click on the 'Forgot Password' link on the login page and follow the instructions."
  },
  {
    question: "How do I update my profile information?",
    answer: "Go to your account settings and edit your profile information there."
  }
];

export default function SimpleAccordion() {
  const [openIdx, setOpenIdx] = useState(0);
  const refs = useRef([]);

  return (
    <div className="bg-white rounded-2xl border border-[#D3D3D3] p-6 max-w-3xl my-8 mx-auto">
      {faqs.map((faq, idx) => {
        const isOpen = openIdx === idx;
        return (
          <div
            className={`border border-gray-200 rounded-xl transition ${
              isOpen ? "bg-gray-50" : ""
            } ${idx !== faqs.length - 1 ? "mb-3" : ""}`}
          >
            {/* Make the entire header clickable and accessible */}
            <div
              role="button"
              tabIndex={0}
              className="w-full text-left px-6 pt-4 rounded-xl font-semibold cursor-pointer focus:outline-none focus-visible:ring"
              onClick={() => setOpenIdx(isOpen ? null : idx)}
              onKeyPress={e => { if (e.key === "Enter" || e.key === " ") setOpenIdx(isOpen ? null : idx); }}
            >
              {faq.question}
            </div>
            <div
              ref={el => (refs.current[idx] = el)}
              style={{
                maxHeight: isOpen
                  ? `${refs.current[idx]?.scrollHeight || 200}px`
                  : "0px",
                opacity: isOpen ? 1 : 0,
                overflow: "hidden",
                transition: "max-height 0.35s cubic-bezier(0.4,0,0.2,1), opacity 0.2s"
              }}
              className="px-6 pb-5 text-gray-700"
            >
              {faq.answer}
            </div>
          </div>
        );
      })}
    </div>
  );
}