import { useState } from "react";
import Header from "../../../components/Header";
import JobCard from "../../../components/JobCard";
import SITLogo from "../../../assets/sit_com_ico.svg";
import SearchFilter from "../../../components/SearchFilter";

const listings2 = [
    {
      logo: SITLogo,
      title: "Student Standardised Patient (PTY1015 Musculoskeletal Physiotherapy 1 - AY24 Tri 3)",
      employer: "Singapore Institute Of Technology",
      scheme: "SIT Student Work Scheme",
      daysRemaining: 85,
      badges: [
        { label: "$19/Hour" },
        { label: "10 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
        { label: "01/09/2025" },
      ],
    },
    {
      logo: SITLogo,
      title: "Student Helper",
      employer: "Singapore Institute Of Technology",
      scheme: "SIT Student Work Scheme",
      daysRemaining: 85,
      badges: [
        { label: "$19/Hour" },
        { label: "10 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
        { label: "01/09/2025" },
      ],
    },
    {
      logo: SITLogo,
      title: "Design of mist collector for testing bacterial load in cooling towers",
      employer: "Singapore Institute Of Technology",
      scheme: "SIT Student Work Scheme",
      daysRemaining: 85,
      badges: [
        { label: "$19/Hour" },
        { label: "10 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
        { label: "01/09/2025" },
      ],
    },
].sort(() => 0.5 - Math.random());

export default function SearchPage() {
  const [filterOpen, setFilterOpen] = useState(false);

  return (
      <div className="min-h-screen bg-[#F8F9FD] px-6 md:px-10">
          <Header active="" />
            {/* Recommended */}
          <div className="py-6 w-full">
            <h1 className="text-2xl font-semibold">Search for 'Engineer'</h1>
            <div className="flex flex-col lg:flex-row lg:justify-end lg:items-start gap-0 mt-4">
              <button
                onClick={() => setFilterOpen(!filterOpen)}
                className="w-full lg:w-auto px-6 py-2 text-white lg:ml-2 bg-gray-800 rounded-lg hover:bg-gray-900 focus:ring-2 focus:ring-gray-500"
              >
                Filter
              </button>
              <SearchFilter open={filterOpen} />
            </div>
            <div className="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 gap-6 mt-4">
              {listings2.map((listing) => (
                <JobCard {...listing} key={listing.title} onClick={() => console.log("Clicked job card")} />
              ))}
            </div>
          </div>
      </div>
  );
}