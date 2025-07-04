import ApplicantCard from "../../../../components/ApplicantCard";

export default function AcceptedList() {
  return (
    <div className="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 gap-3">
        <ApplicantCard
            name="Timothy See"
            appliedAt="20/05/25 09:42"
            status="Accepted"
            statusColor="bg-green-600"
            resumeDetails={{ name: "Timothy See" }} // Example prop for modal
        />
    </div>
  );
}