import { MdHomeFilled } from "react-icons/md";
import { Link, useLocation } from "react-router-dom";
import { FaClipboard } from "react-icons/fa";
import NavSubMenu from "./NavSubMenu";
import { FiSettings, FiLogOut } from "react-icons/fi";
import { IoIosSettings } from "react-icons/io";

export default function NavBar() {
  const { pathname } = useLocation();
  const routes = {
    home: "/recruiter/home",
    listings: "/recruiter/listings",
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
            to={routes.listings}
            className={`flex items-center gap-2 rounded-full border ${
              isActive("listings")
                ? "bg-white text-black border-gray-200"
                : "text-gray-300 border-transparent"
            } px-4 py-2 md:px-4 md:py-2 px-5 py-4`}
          >
            <FaClipboard className="text-xl md:text-base" />
            <span>Listings</span>
          </Link>
        </div>

        {/* Right: Avatar */}
        <NavSubMenu
          avatarUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQiZG8aaViLv7di4yE8HU162B8BNTsCMv7_7w&s"
          items={[
            {
              label: "Settings",
              icon: IoIosSettings,
              to: "/recruiter/settings",
            },
            { label: "Logout", icon: FiLogOut }, // ← Removed onClick, handled in NavSubMenu now
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
          to={routes.listings}
          className={`flex items-center gap-2 rounded-full border ${
            isActive("listings")
              ? "bg-white text-black border-gray-200"
              : "text-gray-300 border-transparent"
          } px-6 py-2`}
        >
          <FaClipboard className="text-md" />
          <span className="hidden">Listings</span>
        </Link>
      </div>
    </div>
  );
}
