import { MdWarning } from 'react-icons/md';
import DescToggle from "../../../components/DescToggle";
import Accordian from "../../../components/Accordian";
import JobCard from '../../../components/JobCard';
import ApplicantCard from '../../../components/ApplicantCard';  
import ListingCard from '../../../components/ListingCard';
import ProgressIndicator from '../../../components/ProgressIndicator';
import SearchFilter from '../../../components/SearchFilter';

function LoginPage() {
  return (
    <div className="bg-[#F8F9FD] flex items-stretch min-h-screen">
      {/* Left: Image */}
      <div className="w-3/5 hidden lg:block">
        <img
          /*src="https://www.woha.net/prod/wp-content/uploads/2025/02/01-SITCampus_Round1_ShiyaStudio-17.jpg"*/
          src="https://www.cmgassets.com/s3fs-public/users/user12663/sit-punggol-campus_campus-court_garden-and-waterfront.jpg?s1391429d1728288210"
          alt="Placeholder"
          className="object-cover w-full h-full min-h-screen"
        />
      </div>
      {/* Right: Login Form */}
      <div className="lg:p-10 md:p-40 sm:p-20 p-8 w-full lg:w-2/5 bg-[#F8F9FD]">
      <div class="sm:mx-start sm:w-full sm:max-w-sm">
        <img class="mx-start h-25 w-auto" src="https://the-ice.org/wp-content/uploads/2020/02/SIT-logo.png" alt="Your Company"></img>
      </div>
        <h1 className="text-2xl font-semibold my-4 ">Welcome to Ready4Work</h1>

        {/* Right: Notice */}
        <div className="relative w-full mx-auto border border-[#D3D3D3] rounded-2xl bg-white px-6 py-5 mb-6">
          <button
            type="button"
            aria-label="Close notice"
            className="absolute top-4 right-4 p-1 rounded hover:bg-gray-100 transition"
            // onClick={() => ...} // add handler if you want to dismiss
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              className="h-6 w-6 text-black"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
              strokeWidth={2}
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M6 18L18 6M6 6l12 12"
              />
            </svg>
          </button>
          <h5 className=" font-medium text-black mb-2">
            Notice
          </h5>
          <p className=" text-sm text-black">
            Access to Ready4Work will be restricted from 10:00 PM to 6:00 AM on weekdays to support students’ well-being and promote a healthy work-life balance.
          </p>
        </div>

        {/* Notifications Alert */}
        <div className="bg-[#727272] rounded-2xl text-white px-6 py-5 my-4 flex items-start gap-4" role="alert">
          {/* Icon */}
          <span className="inline-flex items-center justify-center">
            {/* Exclamation icon */}
            <MdWarning size={25} color="white" />
          </span>
          {/* Text Content */}
          <div>
            <h5 className="font-medium mb-1">Note</h5>
            <p className="text-sm">Please contact Registrar's Office if you wish to update the personal details</p>
          </div>
        </div>

        <form action="#" method="POST">

          {/* Username Input */}
          <div className="relative bg-white rounded-lg border border-[#D3D3D3] focus-within:border-[#7a7a7a] px-3 py-3  transition-colors group mb-4">
            <label htmlFor="username" className="block text-xs font-normal text-[#B0B0B0] group-focus-within:text-[#7a7a7a]">
              Username
            </label>
            <input
              type="text"
              name="username"
              id="username"
              className="w-full bg-transparent text-sm focus:outline-none "
              autoComplete="username"
            />
          </div>

          {/* Password Input */}
          <div className="relative bg-white rounded-lg border border-[#D3D3D3] focus-within:border-[#7a7a7a] px-3 py-3  transition-colors group">
            <label htmlFor="password" className="block text-xs font-normal text-[#B0B0B0] group-focus-within:text-[#7a7a7a]">
              Password
            </label>
            <input
              type="password"
              name="password"
              id="password"
              className="w-full bg-transparent text-sm focus:outline-none "
              autoComplete="current-password"
            />
          </div>

          {/* Error Input */}
          <div className="relative bg-[#FFF9F7] rounded-lg border border-[#D54B21] px-3 py-3 transition-colors group mt-4">
            <label
              htmlFor="verify-password"
              className="block text-xs font-normal text-[#D54B21]">
              Verify Password
            </label>
            <input
              type="password"
              name="verify-password"
              id="verify-password"
              className="w-full bg-transparent text-sm focus:outline-none text-gray-700"
              autoComplete="off"
            />
          </div>
          {/* Error Message */}
          <div className="flex flex-row-reverse gap-1 mt-2 mb-2 text-[#D54B21]">
            {/* Exclamation icon */}
            <svg className="w-5 h-5" fill="currentColor" viewBox="0 0 24 24">
              <circle cx="12" cy="12" r="10" fill="#D54B21" />
              <rect x="11" y="7" width="2" height="6" rx="1" fill="#fff" />
              <circle cx="12" cy="16" r="1" fill="#fff" />
            </svg>
            <span className="text-sm underline cursor-pointer">Password does not match</span>
          </div>

          {/* Dropdown Input */}
          <div className="relative bg-white rounded-lg border border-[#D3D3D3] focus-within:border-[#7a7a7a] px-3 py-3 transition-colors group my-4">
            <label htmlFor="working-hours-end" className="block text-xs font-normal text-[#B0B0B0] group-focus-within:text-[#7a7a7a]">
              Timeout Duration
            </label>
            <select
              id="working-hours-end"
              name="workingHoursEnd"
              className="w-full bg-transparent text-lg focus:outline-none appearance-none pr-8"
              defaultValue="0000">
              {/* Add your time options here */}
              <option value="0000">5 Min</option>
              <option value="0100">10 Min</option>
              <option value="0200">15 Min</option>
              <option value="0300">1 Day</option>
              <option value="2300">1 Month</option>
            </select>
            {/* Chevron icon (right side) */}
            <span className="pointer-events-none absolute right-4 top-1/2 transform -translate-y-1/2 text-gray-500">
              <svg width="20" height="20" fill="none" viewBox="0 0 24 24">
                <path d="M7 10l5 5 5-5" stroke="#444" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" />
              </svg>
            </span>
          </div>

          {/* Remember Me and Forgot Password Section */}
          <div className="mt-8 mb-8 flex items-center justify-between">
            {/* Remember Me Checkbox */}
            <label className="flex items-center gap-2">
              <input type="checkbox" name="remember" className="accent-black"/>
              <span className="text-sm text-b0 ">Remember me</span>
            </label>

            {/* Forgot Password Link */}  
            <a className="text-sm font-medium underline text-[#E30613] transition" href="#">
              Forgot password?
            </a>
          </div>

          {/* Disclaimer Section */}
          <div className="mt-10 mb-10 flex items-center justify-between">

            {/* Informant Text */} 
            <div className="pr-5">
              <p className="text-md">
              Student Account
              </p>
              <p className="text-sm text-[#5E5E5E]">
                Create Ready4Work account with SIT Credentials 
              </p>
            </div>

            {/* Remember Me Checkbox */}
            <label className="flex items-center mr-2">
              <input type="checkbox" name="remember" className="accent-black scale-150"/>
            </label> 
            
          </div>
          
          {/* Description Toggle */}
          <DescToggle />

          {/* Accordian */}
          <Accordian />
          
          {/* Info Card */}
           <div className="bg-white border border-[#D3D3D3] rounded-2xl p-6 my-6">
              <h2 className="text-lg font-semibold mb-3">Application Info</h2>
              <div className="space-y-3">
                {/* Row 1 */}
                <div className="flex justify-between items-center">
                  <span className="underline">Posting Date</span>
                  <span className="text">16/05/2025</span>
                </div>
                {/* Row 2 */}
                <div className="flex justify-between items-center">
                  <span className="underline">Deadline</span>
                  <span className="text">30/05/2025</span>
                </div>
                {/* Row 3 */}
                <div className="flex justify-between items-center">
                  <span className="underline">Applicants</span>
                  <span className="text">0</span>
                </div>
                {/* Row 4 */}
                <div className="flex justify-between items-center">
                  <span className="underline">Vacancies</span>
                  <span className="text">1</span>
                </div>
              </div>
            </div>

          {/* Info Section */}
          <div className="bg-white border border-[#e5e7eb] rounded-2xl p-6 my-6">
            <h2 className="text-xl xl:text-2xl font-semibold mb-3">About Company</h2>
            <p className="text-md mb-4">
              Singapore Institute of Technology (SIT) is Singapore’s University of Applied Learning, offering applied degree programmes across five clusters: Engineering; Food, Chemical & Biotechnology; Infocomm Technology; Health & Social Sciences; Business, Communication & Design.
            </p>
            <p className="text-md">
              Established in 2009 and granted full university status in 2014, SIT integrates work and study through its Integrated Work Study Programme, enabling students to tackle real industry challenges. Spread across six campuses—including its Punggol Campus at 1 Punggol Coast Road—SIT collaborates with nine overseas universities such as Newcastle University and DigiPen co-deliver applied degrees, nurturing graduates who are job- and future-ready.
            </p>
          </div>

          {/* Search Filter */}
          <div className='flex justify-end w-full mb-4'>
              <SearchFilter />
          </div>
 

          {/* Job Card */}
          <JobCard />

          {/* Applicant Card */}
          <ApplicantCard />

          {/* Listing Card */}
          <ListingCard />

          {/* Progress Indicator */}
          <ProgressIndicator />
          
          {/* Login Button */}
          <button type="submit" className="bg-stone-950 text-white rounded-lg py-3 px-4 w-full ">
            Login
          </button>
          



        </form>
      </div>
    </div>
  );
}

export default LoginPage;