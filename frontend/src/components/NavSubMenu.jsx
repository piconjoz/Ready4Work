import { useState, useEffect, useRef } from "react";
import { Link } from "react-router-dom";

export default function NavSubMenu({ avatarUrl, items = [] }) {
  const [open, setOpen] = useState(false);
  const menuRef = useRef();

  useEffect(() => {
    const handleClickOutside = (e) => {
      if (menuRef.current && !menuRef.current.contains(e.target)) {
        setOpen(false);
      }
    };
    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);

  return (
    <div className="relative" ref={menuRef}>
      <img
        src={avatarUrl}
        alt="Avatar"
        className="h-10 w-10 rounded-full object-cover cursor-pointer"
        onClick={() => setOpen(!open)}
      />
      {open && (
        <div className="absolute right-0 mt-2 w-48 bg-white rounded-xl shadow-lg  border border-gray-200 z-50">
          {items.map(({ label, icon: Icon, to, onClick }, idx) =>
            to ? (
              <Link
                key={idx}
                to={to}
                className="flex items-center gap-2 px-4 py-3 hover:bg-gray-100"
              >
                {Icon && <Icon className="text-lg" />}
                <span>{label}</span>
              </Link>
            ) : (
              <button
                key={idx}
                onClick={onClick}
                className="flex items-center gap-2 w-full text-left px-4 py-3 hover:bg-gray-100"
              >
                {Icon && <Icon className="text-lg" />}
                <span>{label}</span>
              </button>
            )
          )}
        </div>
      )}
    </div>
  );
}
