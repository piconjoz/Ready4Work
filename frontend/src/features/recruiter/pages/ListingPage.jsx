import RecruiterHeader from "../../../components/RecruiterHeader";
import ListingCard from "../../../components/ListingCard";
import PrimaryButton from "../../../components/PrimaryButton";



export default function RecruiterListingPage() {
  const listings = [
    {
      title: "Product Development Technologist (Senior)",
      publishedDate: "17/05/2025",
      daysRemaining: 40,
      visibility: "Public",
      pending: 2,
      applicants: 1,
      maxApplicants: 10,
    },
    {
      title: "QA Intern – Beverage R&D",
      publishedDate: "17/05/2025",
      daysRemaining: 5,
      visibility: "Public",
      pending: 5,
      applicants: 8,
      maxApplicants: 10,
    },
    {
      title: "Regulatory Affairs Executive (Food)",
      publishedDate: "12/03/2024",
      daysRemaining: 130,
      visibility: "Public",
      pending: 0,
      applicants: 0,
      maxApplicants: 10,
    },
    {
      title: "Ingredient Sourcing Intern",
      publishedDate: "03/12/2024",
      daysRemaining: 90,
      visibility: "Public",
      pending: 8,
      applicants: 8,
      maxApplicants: 10,
    },
  ];

  return (
    <div className="min-h-screen bg-[#F8F9FD] px-6 md:px-10">
      <RecruiterHeader active="listings" />

      {/* Overview */}
      <div className="pt-6">
        <div className="flex justify-between items-center">
          <h1 className="text-2xl font-semibold">Job Listings</h1>
          <div className="w-40">
            <PrimaryButton
              label="Add Listing"
              onClick={() => console.log("Create New Listing Clicked")}
            />
          </div>
        </div>
        <div className="grid grid-cols-1 md:grid-cols-2 gap-6 mt-4">
          {listings.map((listing, index) => (
            <ListingCard
              key={index}
              title={listing.title}
              publishedDate={listing.publishedDate}
              daysRemaining={listing.daysRemaining}
              visibility={listing.visibility}
              pending={listing.pending}
              applicants={listing.applicants}
              maxApplicants={listing.maxApplicants}
            />
          ))}
        </div>
      </div>
    </div>
  );
}