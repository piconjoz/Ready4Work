import { MdHomeFilled } from "react-icons/md";
import { FaClipboard } from "react-icons/fa";
import { IoBookmarks } from "react-icons/io5";
import { Link, useLocation } from "react-router-dom";
import NavSubMenu from "./NavSubMenu";
import { IoIosSettings } from "react-icons/io";

import { FiSettings, FiLogOut } from "react-icons/fi";

export default function NavBar() {
  const { pathname } = useLocation();
  const routes = {
    home: "/applicant/home",
    applications: "/applicant/apply",
    bookmarks: "/applicant/bookmarks",
  };
  const isActive = (key) => pathname.startsWith(routes[key]);
  return (
    <div className="bg-[#F8F9FD]">
      <div className="flex justify-between items-center p-4">
        {/* Left: Logo */}
        <Link to={routes.home}>
          <img
            src="https://the-ice.org/wp-content/uploads/2020/02/SIT-logo.png"
            alt="SIT"
            className="h-18 cursor-pointer"
          />
        </Link>

        {/* Middle: Tabs */}
        <div className="hidden md:flex gap-4">
          <Link
            to={routes.home}
            className={`flex items-center gap-2 rounded-full border ${
              isActive("home")
                ? "bg-white text-black border-gray-200"
                : "text-gray-300 border-transparent"
            } px-4 py-2 md:px-4 md:py-2 px-5 py-4`}
          >
            <MdHomeFilled className="text-xl md:text-base" />
            <span>Home</span>
          </Link>
          <Link
            to={routes.applications}
            className={`flex items-center gap-2 rounded-full border ${
              isActive("applications")
                ? "bg-white text-black border-gray-200"
                : "text-gray-300 border-transparent"
            } px-4 py-2 md:px-4 md:py-2 px-5 py-4`}
          >
            <FaClipboard className="text-xl md:text-base" />
            <span>Applications</span>
          </Link>
          <Link
            to={routes.bookmarks}
            className={`flex items-center gap-2 rounded-full border ${
              isActive("bookmarks")
                ? "bg-white text-black border-gray-200"
                : "text-gray-300 border-transparent"
            } px-4 py-2 md:px-4 md:py-2 px-5 py-4`}
          >
            <IoBookmarks className="text-xl md:text-base" />
            <span>Bookmarks</span>
          </Link>
        </div>

        {/* Right: Avatar */}
        <NavSubMenu
          avatarUrl="https://encrypted-tbn1.gstatic.com/licensed-image?q=tbn:ANd9GcRHXt1lEDp5xR62TWzq6FzXcZMTNuklFwmDQLQPpI8WEC1yOXp1pglw1v7dUBw83rjPiHJ_QTHvVoFGNog"
          items={[
            { label: "Settings", icon: IoIosSettings, to: "/applicant/settings" },
            { label: "Logout", icon: FiLogOut, onClick: () => console.log("Logout") }
          ]}
        />
      </div>
      {/* Tabs Row - full width below md */}
      <div className="w-full flex flex-wrap justify-center gap-4 px-4 mb-5 md:hidden">
        <Link
          to={routes.home}
          className={`flex items-center gap-2 rounded-full border ${
            isActive("home")
              ? "bg-white text-black border-gray-200"
              : "text-gray-300 border-transparent"
          } px-6 py-2`}
        >
          <MdHomeFilled className="text-md" />
          <span className="hidden">Home</span>
        </Link>
        <Link
          to={routes.applications}
          className={`flex items-center gap-2 rounded-full border ${
            isActive("applications")
              ? "bg-white text-black border-gray-200"
              : "text-gray-300 border-transparent"
          } px-6 py-2`}
        >
          <FaClipboard className="text-md" />
          <span className="hidden">Applications</span>
        </Link>
        <Link
          to={routes.bookmarks}
          className={`flex items-center gap-2 rounded-full border ${
            isActive("bookmarks")
              ? "bg-white text-black border-gray-200"
              : "text-gray-300 border-transparent"
          } px-6 py-2`}
        >
          <IoBookmarks className="text-md" />
          <span className="hidden">Bookmarks</span>
        </Link>
      </div>
    </div>
  );
}