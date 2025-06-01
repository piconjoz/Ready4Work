import { MdHomeFilled } from "react-icons/md";
import { Link, useLocation } from "react-router-dom";
import { FaClipboard } from "react-icons/fa";
import NavSubMenu from "./NavSubMenu";
import { FiSettings, FiLogOut } from "react-icons/fi";
import { FaUsers } from "react-icons/fa";



export default function AdminNavBar() {
  const { pathname } = useLocation();
  const routes = {
    users: "/admin/users",
    listings: "/admin/listings",
  };
  const isActive = (key) => pathname.startsWith(routes[key]);

  return (
    <div className="bg-[#F8F9FD]">
      <div className="flex justify-between items-center p-4">
        {/* Left: Logo */}
        <Link to={routes.users}>
          <img
            src="https://the-ice.org/wp-content/uploads/2020/02/SIT-logo.png"
            alt="SIT"
            className="h-18 cursor-pointer"
          />
        </Link>

        {/* Middle: Tabs */}
        <div className="hidden md:flex gap-4">
          <Link
            to={routes.users}
            className={`flex items-center gap-2 rounded-full border ${
              isActive("users")
                ? "bg-white text-black border-gray-200"
                : "text-gray-300 border-transparent"
            } px-4 py-2 md:px-4 md:py-2 px-5 py-4`}
          >
            <FaUsers className="text-xl md:text-base" />
            <span>Users</span>
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
          avatarUrl="https://lens-storage.storage.googleapis.com/png/5c8a646c2b9e4a57af803943a0d59c9e"
          items={[
            // { label: "Settings", icon: IoIosSettings, to: "/recruiter/settings" },
            { label: "Logout", icon: FiLogOut, onClick: () => console.log("Logout") }
          ]}
        />
      </div>
      {/* Tabs Row - full width below md */}
      <div className="w-full flex flex-wrap justify-center gap-4 px-4 mb-5 md:hidden">
        <Link
          to={routes.users}
          className={`flex items-center gap-2 rounded-full border ${
            isActive("users")
              ? "bg-white text-black border-gray-200"
              : "text-gray-300 border-transparent"
          } px-6 py-2`}
        >
          <MdHomeFilled className="text-md" />
          <span className="hidden">Users</span>
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