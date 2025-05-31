import { MdHomeFilled } from "react-icons/md";
import { FaClipboard } from "react-icons/fa";

export default function NavBar({ active = "", onTabClick = () => {} }) {
  return (
    <div className="bg-[#F8F9FD]">
      <div className="flex justify-between items-center p-4">
        {/* Left: Logo */}
        <img
          src="https://the-ice.org/wp-content/uploads/2020/02/SIT-logo.png"
          alt="SIT"
          className="h-18"
        />

        {/* Middle: Tabs */}
        <div className="hidden md:flex gap-4">
          <button
            onClick={() => onTabClick("home")}
            className={`flex items-center gap-2 rounded-full border ${
              active === "home"
                ? "bg-white text-black border-gray-200"
                : "text-gray-300 border-transparent"
            } px-4 py-2 md:px-4 md:py-2 px-5 py-4`}
          >
            <MdHomeFilled className="text-xl md:text-base" />
            <span>Home</span>
          </button>
          <button
            onClick={() => onTabClick("listings")}
            className={`flex items-center gap-2 rounded-full border ${
              active === "listings"
                ? "bg-white text-black border-gray-200"
                : "text-gray-300 border-transparent"
            } px-4 py-2 md:px-4 md:py-2 px-5 py-4`}
          >
            <FaClipboard className="text-xl md:text-base" />
            <span>Listings</span>
          </button>
        </div>

        {/* Right: Avatar */}
        <img
          src="https://encrypted-tbn1.gstatic.com/licensed-image?q=tbn:ANd9GcRHXt1lEDp5xR62TWzq6FzXcZMTNuklFwmDQLQPpI8WEC1yOXp1pglw1v7dUBw83rjPiHJ_QTHvVoFGNog"
          alt="User Avatar"
          className="h-10 w-10 rounded-full object-cover"
        />
      </div>
      {/* Tabs Row - full width below md */}
      <div className="w-full flex flex-wrap justify-center gap-4 px-4 mb-5 md:hidden">
        <button
          onClick={() => onTabClick("home")}
          className={`flex items-center gap-2 rounded-full border ${
            active === "home"
              ? "bg-white text-black border-gray-200"
              : "text-gray-300 border-transparent"
          } px-6 py-2`}
        >
          <MdHomeFilled className="text-md" />
          <span className="hidden">Home</span>
        </button>
        <button
          onClick={() => onTabClick("listings")}
          className={`flex items-center gap-2 rounded-full border ${
            active === "listings"
              ? "bg-white text-black border-gray-200"
              : "text-gray-300 border-transparent"
          } px-6 py-2`}
        >
          <FaClipboard className="text-md" />
          <span className="hidden">Listings</span>
        </button>
      </div>
    </div>
  );
}