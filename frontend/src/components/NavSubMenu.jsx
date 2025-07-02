import { useState, useEffect, useRef } from "react";
import { Link, useNavigate } from "react-router-dom";
import { logout } from "../services/authAPI.js"; // ← Add this import
import toast from "react-hot-toast"; // ← Add this import

export default function NavSubMenu({ avatarUrl, items = [] }) {
  const [open, setOpen] = useState(false);
  const menuRef = useRef();
  const navigate = useNavigate(); // ← Add this

  useEffect(() => {
    const handleClickOutside = (e) => {
      if (menuRef.current && !menuRef.current.contains(e.target)) {
        setOpen(false);
      }
    };
    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);

  // ← Add logout handler
  const handleLogout = async () => {
    try {
      console.log("Logging out...");
      setOpen(false); // Close menu

      // Call logout API
      await logout();

      console.log("Logout successful");
      toast.success("Logged out successfully!");

      // Redirect to login page
      navigate("/auth/login");
    } catch (error) {
      console.error("Logout error:", error);
      toast.error("Logout failed, but you've been signed out locally.");

      // Still redirect even if API call fails
      navigate("/auth/login");
    }
  };

  return (
    <div className="relative" ref={menuRef}>
      <img
        src={avatarUrl}
        alt="Avatar"
        className="h-10 w-10 rounded-full object-cover cursor-pointer"
        onClick={() => setOpen(!open)}
      />
      {open && (
        <div className="absolute right-0 mt-2 w-48 bg-white rounded-xl shadow-lg border border-gray-200 z-50">
          {items.map(({ label, icon: Icon, to, onClick }, idx) =>
            to ? (
              <Link
                key={idx}
                to={to}
                className="flex items-center gap-2 px-4 py-3 hover:bg-gray-100"
                onClick={() => setOpen(false)} // ← Close menu on click
              >
                {Icon && <Icon className="text-lg" />}
                <span>{label}</span>
              </Link>
            ) : (
              <button
                key={idx}
                onClick={label === "Logout" ? handleLogout : onClick} // ← Use handleLogout for logout
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
