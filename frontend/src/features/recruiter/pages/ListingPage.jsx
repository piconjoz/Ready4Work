import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import RecruiterHeader from "../../../components/RecruiterHeader";
import ListingCard from "../../../components/ListingCard";
import PrimaryButton from "../../../components/PrimaryButton";
import EmptyCard from "../../../components/EmptyCard";
import { listJobListings } from "../../../services/RecruiterJobListing";


export default function RecruiterListingPage() {
  const [listings, setListings] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const loadListings = async () => {
      try {
        const response = await listJobListings();
        // console.log("Listings response:", response);
        setListings(response.data ?? []);
        setError(null);
      } catch (err) {
        const msg =
          err.response?.data?.message ??
          "An unexpected error occurred while loading listings.";
        setError(msg);
      } finally {
        setLoading(false);
      }
    };

    loadListings();
  }, []);

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
              onClick={() => navigate("/listing/edit")}
            />
          </div>
        </div>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-6 mt-4">
           {loading ? (
              <EmptyCard text="Loading..." />
            ) : error ? (
              <EmptyCard text={error} />
            ) : listings.length === 0 ? (
              <EmptyCard text="No Data" />
            ) : (
              listings.map((listing, index) => (
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
              ))
            )}
          </div>
      </div>
    </div>
  );
}