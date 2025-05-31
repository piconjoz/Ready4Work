import ApplicantCard from "../../../../components/ApplicantCard";

export default function OfferedList() {
  return (
    <div className="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 gap-3">
        <ApplicantCard
            name="Jordan Low"
            appliedAt="20/05/25 09:42"
            status="Offered"
            statusColor="bg-amber-950"
            resumeDetails={{ name: "Jordan Low" }} // Example prop for modal
        />
        <ApplicantCard
            name="Sean Yap"
            appliedAt="20/05/25 09:42"
            status="Rejected"
            statusColor="bg-red-600"
        />
    </div>
  );
}