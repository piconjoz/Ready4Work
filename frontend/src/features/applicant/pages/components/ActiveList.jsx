import JobCard from "../../../../components/JobCard";
import SITLogo from "../../../../assets/sit_com_ico.svg"; // adjust path if needed
import AlertNote from "../../../../components/AlertNote"
import { MdInfo } from "react-icons/md";

export default function ActiveList() {
  const listings = [
    {
      logo: SITLogo,
      title: "Digital Media Intern",
      employer: "Singapore Institute Of Technology",
      scheme: "SIT Student Work Scheme",
      daysRemaining: 12,
      badges: [
        { label: "$15/Hour" },
        { label: "2 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
        { label: "18/06/2025" },
      ],
      status: "applied",
    },
    {
      logo: SITLogo,
      title: "Lab Technician Assistant",
      employer: "Singapore Institute Of Technology",
      scheme: "SIT Student Work Scheme",
      daysRemaining: 25,
      badges: [
        { label: "$13/Hour" },
        { label: "4 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
        { label: "30/06/2025" },
      ],
    },
    {
      logo: SITLogo,
      title: "Peer Learning Facilitator",
      employer: "Singapore Institute Of Technology",
      scheme: "SIT Student Work Scheme",
      daysRemaining: 18,
      badges: [
        { label: "$16/Hour" },
        { label: "3 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
        { label: "22/06/2025" },
      ],
     status: "applied",
    },
    {
      logo: SITLogo,
      title: "Admin Support Staff",
      employer: "Singapore Institute Of Technology",
      scheme: "SIT Student Work Scheme",
      daysRemaining: 20,
      badges: [
        { label: "$11/Hour" },
        { label: "5 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
        { label: "25/06/2025" },
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