import { MdWarning } from 'react-icons/md';
import NoticeBanner from '../../../components/NoticeBanner';
import DescToggle from "../../../components/DescToggle";
import Accordian from "../../../components/Accordian";
import JobCard from '../../../components/JobCard';
import ApplicantCard from '../../../components/ApplicantCard';  
import ListingCard from '../../../components/ListingCard';
import ProgressIndicator from '../../../components/ProgressIndicator';
import SearchFilter from '../../../components/SearchFilter';
import AlertNote from '../../../components/AlertNote';
import StatusInputField from '../../../components/StatusInputField';
import SelectField from '../../../components/SelectField';
import { useState } from "react";
import DisclaimerCheckbox from '../../../components/DisclaimerCheckbox';
import DescSection from '../../../components/DescSection';  
import SITLogo from "../../../assets/sit_com_ico.svg";
import PrimaryButton from '../../../components/PrimaryButton';



function LoginPageDemo() {

const [selected, setSelected] = useState("0000");

const timeoutOptions = [
  { value: "0000", label: "5 Min" },
  { value: "0100", label: "10 Min" },
  { value: "0200", label: "15 Min" },
  { value: "0300", label: "1 Day" },
  { value: "2300", label: "1 Month" },
];
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
        <NoticeBanner
          title="Notice"
          message="The portal will be unavailable from 12:00 AM to 2:00 AM for scheduled maintenance."
          onClose={() => setShowNotice(false)}
        />
    

        {/* Notifications Alert */}
        <AlertNote
          title="Bookmarks"
          message="Keep track of roles you’re interested in."
          bgColor="#727272"
        />

        <form action="#" method="POST">

          {/* Username Input */}
          <StatusInputField
            label="Username"
            name="Username"
            type="text"
            status="default"
            errorMessage=""
          />  

          {/* Password Input */}
          <StatusInputField
            label="Password"
            name="password"
            type="password"
            status="default"
            errorMessage=""
          />  

          {/* Error Input */}
          <StatusInputField
            label="Verify Password"
            name="verify-password"
            type="password"
            status="error"
            errorMessage="Password does not match"
          />
        

          {/* Dropdown Input */}

          <SelectField
            label="Timeout Duration"
            name="workingHoursEnd"
            value={selected}
            onChange={(e) => setSelected(e.target.value)}
            options={timeoutOptions}
          />

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
          <DisclaimerCheckbox 
            title="Student Account" 
            description="Create Ready4Work account with SIT Credentials"
            name="studentAccount"
          />
        
          
          {/* Description Toggle */}
          <DescToggle
            title="Redact Resume"
            description="Share resume without personal info."
            name="redactedResume"
          />

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
        <DescSection
          title="About Company"
          paragraphs={[
            "Singapore Institute of Technology (SIT) is Singapore’s University of Applied Learning, offering applied degree programmes across five clusters: Engineering; Food, Chemical & Biotechnology; Infocomm Technology; Health & Social Sciences; Business, Communication & Design.",
            "Established in 2009 and granted full university status in 2014, SIT integrates work and study through its Integrated Work Study Programme, enabling students to tackle real industry challenges. Spread across six campuses—including its Punggol Campus at 1 Punggol Coast Road—SIT collaborates with nine overseas universities such as Newcastle University and DigiPen co-deliver applied degrees, nurturing graduates who are job- and future-ready.",
          ]}
        />
        <DescSection
          title="Job Description"
          paragraphs={[
            "Student coach opportunity for EDE2022 Probability & Statistical Signal Processing—we are seeking EDE students who possess a strong understanding of EDE2022 Probability & Statistical Signal Processing module.",
            "The chosen student will serve as tutors, assisting and mentoring Year 1 student on the following dates to help them prepare for their upcoming exams. Date: 7 Jul 10 am to 12pm"
          ]}
        />

        {/* Search Filter */}
        <div className='flex justify-end w-full mb-4'>
            <SearchFilter />
        </div>


        {/* Job Card */}
        <JobCard
          logo={SITLogo}
          title="Student Standardised Patient (PTY1015 Musculoskeletal Physiotherapy 1 - AY24 Tri 3)"
          employer="Singapore Institute Of Technology"
          scheme="SIT Student Work Scheme"
          daysRemaining={85}
          badges={[
            { label: "$19/Hour"},
            { label: "10 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
            { label: "01/09/2025" },
          ]}
          onClick={() => console.log("Clicked job card")}
        />

        {/* Applicant Card */}
        <ApplicantCard
          name="Natalie Goh"
          appliedAt="20/05/25 09:42"
          status="Pending"
          statusColor="bg-green-600"
          resumeDetails={{ name: "Natalie Goh" }} // Example prop for modal
        />

        {/* Listing Card */}
        <ListingCard
          title="QA Intern – Beverage R&D"
          publishedDate="18/05/2025"
          daysRemaining={25}
          visibility="Private"
          pending={3}
          applicants={5}
          maxApplicants={10}
        />

        {/* Progress Indicator */}
        <ProgressIndicator />
        
        {/* Login Button */}
        <PrimaryButton type="submit" label="Login" />

        </form>
      </div>
    </div>
  );
}

export default LoginPageDemo;