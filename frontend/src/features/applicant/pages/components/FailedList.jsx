import JobCard from "../../../../components/JobCard";
import SITLogo from "../../../../assets/sit_com_ico.svg"; // adjust path if needed
import AlertNote from "../../../../components/AlertNote"
import { MdInfo } from "react-icons/md";

export default function FailedList() {
  const listings = [
    {
      logo: SITLogo,
      title: "Campus Tour Guide",
      employer: "Singapore Institute Of Technology",
      scheme: "SIT Student Work Scheme",
      daysRemaining: 14,
      badges: [
        { label: "$14/Hour" },
        { label: "6 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
        { label: "20/06/2025" },
      ],
    },
    {
      logo: SITLogo,
      title: "AV Equipment Support Assistant",
      employer: "Singapore Institute Of Technology",
      scheme: "SIT Student Work Scheme",
      daysRemaining: 10,
      badges: [
        { label: "$15/Hour" },
        { label: "3 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
        { label: "17/06/2025" },
      ],
    },
    {
      logo: SITLogo,
      title: "Library Circulation Intern",
      employer: "Singapore Institute Of Technology",
      scheme: "SIT Student Work Scheme",
      daysRemaining: 21,
      badges: [
        { label: "$12/Hour" },
        { label: "4 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
        { label: "28/06/2025" },
      ],
    },
    {
      logo: SITLogo,
      title: "Event Planning Assistant",
      employer: "Singapore Institute Of Technology",
      scheme: "SIT Student Work Scheme",
      daysRemaining: 16,
      badges: [
        { label: "$13/Hour" },
        { label: "5 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
        { label: "23/06/2025" },
      ],
    },
  ];

  return (
    <div className="mt-6">
      <AlertNote
        title="Failed Applications"
        message="Applications that have been closed, removed or rejected."
        icon={<MdInfo size={25} color="white" />}
        bgColor="#DE1135"
      />

      <div className="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 gap-6 mt-4">
        {listings.map((job, index) => (
          <JobCard key={index} {...job} />
        ))}
      </div>
    </div>
  );
}