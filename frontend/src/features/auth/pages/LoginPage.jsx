import { MdWarning } from 'react-icons/md';
import { MdLogin, MdAppRegistration } from "react-icons/md";
import NoticeBanner from '../../../components/NoticeBanner';
import StatusInputField from '../../../components/StatusInputField';
import { useState } from "react";
import PrimaryButton from '../../../components/PrimaryButton';
import LoginForm from './components/LoginForm';
import SignupForm from './components/SignupForm'; 

function LoginPage() {

  const [activeTab, setActiveTab] = useState("login");

  return (
    <div className="bg-[#F8F9FD] flex items-stretch min-h-screen">
      {/* Left: Image */}
      <div className="w-3/5 hidden lg:block">
        <img
          src="https://www.cmgassets.com/s3fs-public/users/user12663/sit-punggol-campus_campus-court_garden-and-waterfront.jpg?s1391429d1728288210"
          alt="Placeholder"
          className="object-cover w-full h-full min-h-screen"
        />
      </div>

      {/* Right: Login Form */}
      <div className="lg:p-10 md:p-40 sm:p-20 p-8 w-full lg:w-2/5 bg-[#F8F9FD]">
        <div className="sm:mx-start sm:w-full sm:max-w-sm">
          <img className="mx-start h-25 w-auto" src="https://the-ice.org/wp-content/uploads/2020/02/SIT-logo.png" alt="Your Company" />
        </div>
        <h1 className="text-2xl font-semibold my-4 ">Welcome to Ready4Work</h1>

        {/* Notice */}
        <NoticeBanner
          title="Notice"
          message="The portal will be unavailable from 12:00 AM to 2:00 AM for scheduled maintenance."
          onClose={() => setShowNotice(false)}
        />

        {/* Tab Switcher */}
        <div className="mb-6">
          <div className="flex space-x-6">
            <div className="relative">
              <button
                onClick={() => setActiveTab("login")}
                className={`flex items-center gap-2 px-1 py-2 text-md font-medium transition ${
                  activeTab === "login" ? "text-black" : "text-gray-300"
                }`}
              >
                <MdLogin size={20} />
                Login
              </button>
              {activeTab === "login" && (
                <div className="absolute left-0 bottom-0 w-full h-1 bg-black" />
              )}
            </div>
            <div className="relative">
              <button
                onClick={() => setActiveTab("signup")}
                className={`flex items-center gap-2 px-1 py-2 text-md font-medium transition ${
                  activeTab === "signup" ? "text-black" : "text-gray-300"
                }`}
              >
                <MdAppRegistration size={20} />
                Signup
              </button>
              {activeTab === "signup" && (
                <div className="absolute left-0 bottom-0 w-full h-1 bg-black" />
              )}
            </div>
          </div>
        </div>

        {/* Form Content */}
        <div className="w-full max-w-md">
          {activeTab === "login" ? (
            <LoginForm />
          ) : (
            <SignupForm />
          )}
        </div>
      </div>
    </div>
  );
}

export default LoginPage;