import NavBar from "../../../components/NavBar";
import SearchBar from '../../../components/SearchBar';
import JobCard from "../../../components/JobCard";
import Header from "../../../components/Header";
import SITLogo from "../../../assets/sit_com_ico.svg";



const listings = [
  {
    logo: SITLogo,
    title: "Library Support Assistant (Evening Shift)",
    employer: "Singapore Institute Of Technology",
    scheme: "SIT Student Work Scheme",
    daysRemaining: 18,
    badges: [
      { label: "$10/Hour" },
      { label: "2 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
      { label: "20/06/2025" },
    ],
  },
  {
    logo: SITLogo,
    title: "Photography Crew – Graduation Ceremony",
    employer: "Singapore Institute Of Technology",
    scheme: "SIT Student Work Scheme",
    daysRemaining: 31,
    badges: [
      { label: "$15/Hour" },
      { label: "5 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
      { label: "03/07/2025" },
    ],
  },
  {
    logo: SITLogo,
    title: "Research Assistant – Food Innovation Project",
    employer: "Singapore Institute Of Technology",
    scheme: "SIT Student Work Scheme",
    daysRemaining: 22,
    badges: [
      { label: "$18/Hour" },
      { label: "1 Vacancy", color: "bg-[#F5F5F5] border-gray-300" },
      { label: "25/06/2025" },
    ],
  },
  {
    logo: SITLogo,
    title: "Student Technician – Makerspace",
    employer: "Singapore Institute Of Technology",
    scheme: "SIT Student Work Scheme",
    daysRemaining: 9,
    badges: [
      { label: "$12/Hour" },
      { label: "3 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
      { label: "10/06/2025" },
    ],
  },
  {
    logo: SITLogo,
    title: "Marketing Assistant (Social Media Support)",
    employer: "Singapore Institute Of Technology",
    scheme: "SIT Student Work Scheme",
    daysRemaining: 40,
    badges: [
      { label: "$13/Hour" },
      { label: "2 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
      { label: "05/08/2025" },
    ],
  },
  {
    logo: SITLogo,
    title: "IT Support Intern – Campus Tech Helpdesk",
    employer: "Singapore Institute Of Technology",
    scheme: "SIT Student Work Scheme",
    daysRemaining: 60,
    badges: [
      { label: "$16/Hour" },
      { label: "4 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
      { label: "25/08/2025" },
    ],
  },
].sort(() => 0.5 - Math.random());

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


export default function ApplicantHomePage() {
  return (
    <div className="min-h-screen bg-[#F8F9FD] px-6 md:px-10">
      <Header active="home" />

      {/* Recommended */}
      <div className="py-6">
        <h1 className="text-2xl font-semibold">Recommended For You</h1>
        <div className="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 gap-6 mt-4">
          {listings2.map((listing) => (
            <JobCard {...listing} key={listing.title} onClick={() => console.log("Clicked job card")} />
          ))}
        </div>
      </div>

      {/* New Listings */}
      <div className="py-6">
        <h1 className="text-2xl font-semibold">New Listings</h1>
        <div className="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 gap-6 mt-4">
          {listings.map((listing) => (
            <JobCard {...listing} key={listing.title} onClick={() => console.log("Clicked job card")} />
          ))}
        </div>
      </div>
    </div>
  );
}