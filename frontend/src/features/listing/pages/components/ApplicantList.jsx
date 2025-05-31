import ApplicantCard from "../../../../components/ApplicantCard";

export default function ApplicantList() {
  return (
    <div className="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 gap-3">
        <ApplicantCard
            name="Natalie Goh"
            appliedAt="20/05/25 09:42"
            resumeDetails={{ name: "Natalie Goh" }} // Example prop for modal
        />
    </div>
  );
}