import RecruiterHeader from "../../../components/RecruiterHeader";
import AlertNote from "../../../components/AlertNote";
import StatCard from "../../../components/StatisticCard";
import { HiMiniFire } from "react-icons/hi2";
import { IoSend } from "react-icons/io5";
import { MdHideSource } from "react-icons/md";
import { BsPatchCheckFill } from "react-icons/bs";
import { MdStream } from "react-icons/md";
import JobCard from "../../../components/JobCard";
import { FaUserCircle } from "react-icons/fa";
import EmptyCard from "../../../components/EmptyCard";




const listings2 = [
    {
      logo: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQiZG8aaViLv7di4yE8HU162B8BNTsCMv7_7w&s",
      title: "Student Standardised Patient (PTY1015 Musculoskeletal Physiotherapy 1 - AY24 Tri 3)",
      employer: "Singapore Institute Of Technology",
      scheme: "SIT Student Work Scheme",
      daysRemaining: 5,
      badges: [
        { label: "$19/Hour" },
        { label: "10 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
        { label: "01/09/2025" },
      ]
    },
].sort(() => 0.5 - Math.random());


export default function RecruiterHomePage() {
  return (
    <div className="min-h-screen bg-[#F8F9FD] px-6 md:px-10">
      <RecruiterHeader active="home" />

      {/* Overview */}
      <div className="pt-6">
        <h1 className="text-2xl font-semibold">Overview</h1>
        <AlertNote
            title="Updated Daily"
            message="Statistics are updated as of 18/05/2025 00:00"
            bgColor="#727272"
        />
        <div className="grid grid-cols-2 md:grid-cols-5 gap-2 mt-4">
           <StatCard 
                icon={<HiMiniFire className="w-5 h-5" />}
                value="+52"
                label="New Applications"
            />  
            <StatCard 
                icon={<BsPatchCheckFill className="w-5 h-5" />}
                value="+2"
                label="Accepted Applications"
            />  
            <StatCard 
              icon={<IoSend className="w-5 h-5" />}
              value="+12"
              label="New Postings"
            />  
            <StatCard 
              icon={<MdHideSource className="w-5 h-5" />}
              value="+3"
              label="Expired Postings"
            />  
            <StatCard 
              icon={<MdStream className="w-5 h-5" />}
              value="+21"
              label="Active Postings"
            />  
        </div>
   
      </div>

      {/* Expiring Listings */}
      <div className="py-6">
        <h1 className="text-2xl font-semibold">Expiring Soon</h1>
         <div className="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 gap-6 mt-4">
            {listings2.map((listing) => (
              <JobCard {...listing} key={listing.title} onClick={() => console.log("Clicked job card")} />
            ))}
          </div>
      </div>

      {/* Full Applicants */}
      <div className="py-6">
        <h1 className="text-2xl font-semibold">Completed Listing</h1>
        <div className="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 gap-6 mt-4">
             <EmptyCard 
              text="No Data"
            />
        </div>
           
      </div>
    </div>
  );
}