import JobCard from "../../../../components/JobCard";
import SITLogo from "../../../../assets/sit_com_ico.svg"; // adjust path if needed
import AlertNote from "../../../../components/AlertNote";
import { MdInfo } from "react-icons/md";

export default function ExpiredList() {
  const listings = [
    {
      logo: SITLogo,
      title: "Campus Outreach Volunteer",
      employer: "Singapore Institute Of Technology",
      scheme: "SIT Student Work Scheme",
      daysRemaining: 0,
      badges: [
        { label: "$10/Hour" },
        { label: "Position Closed", color: "bg-[#F5F5F5] border-gray-300" },
        { label: "15/04/2025" },
      ],
    },
    {
      logo: SITLogo,
      title: "Facilities Maintenance Helper",
      employer: "Singapore Institute Of Technology",
      scheme: "SIT Student Work Scheme",
      daysRemaining: 0,
      badges: [
        { label: "$12/Hour" },
        { label: "Application Closed", color: "bg-[#F5F5F5] border-gray-300" },
        { label: "01/05/2025" },
      ],
    },
    {
      logo: SITLogo,
      title: "Library Support Assistant",
      employer: "Singapore Institute Of Technology",
      scheme: "SIT Student Work Scheme",
      daysRemaining: 0,
      badges: [
        { label: "$11/Hour" },
        { label: "0 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
        { label: "20/04/2025" },
      ],
    },
    {
      logo: SITLogo,
      title: "Logistics Support for School Events",
      employer: "Singapore Institute Of Technology",
      scheme: "SIT Student Work Scheme",
      daysRemaining: 0,
      badges: [
        { label: "$13/Hour" },
        { label: "Filled", color: "bg-[#F5F5F5] border-gray-300" },
        { label: "10/05/2025" },
      ],
    },
  ];

  return (
    <div className="mt-6">
       <AlertNote
            title="Bookmarks"
            message="Keep track of roles you’re interested in."
            icon={<MdInfo size={25} color="white" />}
            bgColor="#727272"
        />  

      <div className="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 gap-6 mt-4">
        {listings.map((job, index) => (
          <JobCard key={index} {...job} />
        ))}
      </div>
    </div>
  );
}