import JobCard from "../../../../components/JobCard";
import SITLogo from "../../../../assets/sit_com_ico.svg"; // adjust path if needed
import AlertNote from "../../../../components/AlertNote"
import { MdInfo } from "react-icons/md";

export default function PendingList() {
  const listings = [
    {
      logo: SITLogo,
      title: "Campus Ambassador",
      employer: "Singapore Institute Of Technology",
      scheme: "SIT Student Work Scheme",
      daysRemaining: 30,
      badges: [
        { label: "$12/Hour" },
        { label: "5 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
        { label: "10/08/2025" },
      ],
      secondaryActions: [
        {
          label: "Cancel Application",
          onClick: () => alert("Cancelled"),
          color: "bg-red-600 hover:bg-red-700 text-white",
        },
      ],
    },
    {
      logo: SITLogo,
      title: "Event Coordinator Assistant",
      employer: "Singapore Institute Of Technology",
      scheme: "SIT Student Work Scheme",
      daysRemaining: 45,
      badges: [
        { label: "$14/Hour" },
        { label: "3 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
        { label: "15/08/2025" },
      ],
      secondaryActions: [
        {
          label: "Cancel Application",
          onClick: () => alert("Cancelled"),
          color: "bg-red-600 hover:bg-red-700 text-white",
        },
      ],
    },
    {
      logo: SITLogo,
      title: "Library Support Assistant",
      employer: "Singapore Institute Of Technology",
      scheme: "SIT Student Work Scheme",
      daysRemaining: 20,
      badges: [
        { label: "$10/Hour" },
        { label: "2 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
        { label: "05/07/2025" },
      ],
      secondaryActions: [
        {
          label: "Cancel Application",
          onClick: () => alert("Cancelled"),
          color: "bg-red-600 hover:bg-red-700 text-white",
        },
      ],
    },
    {
      logo: SITLogo,
      title: "Research Assistant",
      employer: "Singapore Institute Of Technology",
      scheme: "SIT Student Work Scheme",
      daysRemaining: 15,
      badges: [
        { label: "$18/Hour" },
        { label: "1 Vacancy", color: "bg-[#F5F5F5] border-gray-300" },
        { label: "01/07/2025" },
      ],
      secondaryActions: [
        {
          label: "Cancel Application",
          onClick: () => alert("Cancelled"),
          color: "bg-red-600 hover:bg-red-700 text-white",
        },
      ],
    },
  ];

  return (
    <div className="mt-6">
      <AlertNote
        title="Pending Applications"
        message="Applications that are still under review and awaiting a decision."
        icon={<MdInfo size={25} color="white" />}
        bgColor="#2465DE"
      />

      <div className="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 gap-6 mt-4">
        {listings.map((job, index) => (
          <JobCard key={index} {...job} />
        ))}
      </div>
    </div>
  );
}