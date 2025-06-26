import { useState } from "react";
import { submitApplication } from "../../../services/applicationApi";
import Header from "../../../components/Header";
import DetailedJobCard from '../../../components/DetailedJobCard';
import SITLogo from "../../../assets/sit_com_ico.svg";
import AlertNote from "../../../components/AlertNote";
import InfoCard from "../../../components/InfoCard";
import DescSection from "../../../components/DescSection";

// Mock job data - in real implementation, this would come from props or API
const mockJobData = {
  id: 1,
  title: "EDE2022 Probability & Statistical Signal Processing",
  description: "Student coach opportunity for EDE2022 Probability & Statistical Signal Processing — we are seeking EDE students who possess a strong understanding of EDE2022 Probability & Statistical Signal Processing module. The chosen student will serve as tutors, assisting and mentoring Year 1 student on the following dates to help them prepare for their upcoming exams. Date: 7 Jul 10 am to 12pm",
  employer: "Singapore Institute Of Technology",
  scheme: "SIT Student Work Scheme",
  logo: SITLogo,
  badges: [
    { label: "$15/Hour" },
    { label: "2 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
    { label: "18/06/2025" },
  ]
};

export default function ListingPage() {
  const [applicationStatus, setApplicationStatus] = useState(""); // "", "applying", "applied", "error"
  const [errorMessage, setErrorMessage] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  const handleApply = async () => {
    try {
      setIsLoading(true);
      setApplicationStatus("applying");
      setErrorMessage("");

      console.log("Applying for job:", mockJobData.title);
      
      // Call the API to submit application with AI cover letter generation
      const result = await submitApplication(mockJobData.id);
      
      if (result.success) {
        setApplicationStatus("applied");
        console.log("Application submitted successfully:", result);
        
        // Show success message if cover letter was generated
        if (result.coverLetterGenerated) {
          console.log("AI Cover letter generated and submitted");
        }
      } else {
        setApplicationStatus("error");
        setErrorMessage(result.message || "Failed to submit application");
      }
    } catch (error) {
      console.error("Application submission failed:", error);
      setApplicationStatus("error");
      setErrorMessage(error.response?.data?.message || "An unexpected error occurred. Please try again.");
    } finally {
      setIsLoading(false);
    }
  };

  const getStatusMessage = () => {
    switch (applicationStatus) {
      case "applying":
        return {
          title: "Generating Cover Letter...",
          message: "Please wait while we generate a personalized cover letter and submit your application.",
          bgColor: "#2465DE"
        };
      case "applied":
        return {
          title: "Application Submitted Successfully!",
          message: "Your application with AI-generated cover letter has been submitted. Check the Applications page for updates.",
          bgColor: "#0E8345"
        };
      case "error":
        return {
          title: "Application Failed",
          message: errorMessage,
          bgColor: "#DE1135"
        };
      default:
        return null;
    }
  };

  const statusMessage = getStatusMessage();
  const canApply = applicationStatus !== "applied" && !isLoading;

  return (
    <div className="min-h-screen bg-[#F8F9FD] px-6 md:px-10">
      <Header active="applications" />
      
      <div className="py-6">
        <DetailedJobCard
          logo={mockJobData.logo}
          title={mockJobData.title}
          employer={mockJobData.employer}
          scheme={mockJobData.scheme}
          daysRemaining={12}
          badges={mockJobData.badges}
          status={applicationStatus === "applied" ? "applied" : applicationStatus}
          onClick={canApply ? handleApply : undefined}
        />

        {/* Status Alert */}
        {statusMessage && (
          <AlertNote
            title={statusMessage.title}
            message={statusMessage.message}
            bgColor={statusMessage.bgColor}
          />
        )}

        <div className="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 gap-3 mt-4">
          <InfoCard
            title="Application Info"
            info={[
              { label: "Posting Date", value: "16/05/2025" },
              { label: "Deadline", value: "30/05/2025" },
              { label: "Applicants", value: "0" },
              { label: "Vacancies", value: "1" },
            ]}
          />
          <InfoCard
            title="Job Summary"
            info={[
              { label: "Job Duration", value: "7/7/2025 – 7/7/2025" },
              { label: "Working Hours", value: "0900–1800" },
              { label: "Work Location", value: "SIT@Punggol, 1 Punggol Coast Road" },
              { label: "Qualifications", value: "BEng (Hons) Electronics and Data Engineering" },
            ]}
          />
          <InfoCard
            title="Job Benefits"
            info={[
              { label: "Remuneration Type", value: "Hourly" },
              { label: "Rate", value: "$20/hour" },
              { label: "Other Benefits", value: "NA" },
              { label: "Skill Set", value: "Tutors" },
            ]}
          />
        </div>

        <DescSection
          title="Job Requirements"
          paragraphs={[
            "Strong understanding of EDE2022 Probability & Statistical Signal Processing module",
            "Ability to mentor and assist Year 1 students",
            "Good communication and teaching skills",
            "Available on specified tutoring dates",
          ]}
        />

        <DescSection
          title="Job Description"
          paragraphs={[
            "Student coach opportunity for EDE2022 Probability & Statistical Signal Processing — we are seeking EDE students who possess a strong understanding of EDE2022 Probability & Statistical Signal Processing module.",
            "The chosen student will serve as tutors, assisting and mentoring Year 1 student on the following dates to help them prepare for their upcoming exams. Date: 7 Jul 10 am to 12pm"
          ]}
        />

        <DescSection
          title="About Company"
          paragraphs={[
            "Singapore Institute of Technology (SIT) is Singapore's University of Applied Learning, offering applied degree programmes across five clusters: Engineering; Food, Chemical & Biotechnology; Infocomm Technology; Health & Social Sciences; Business, Communication & Design.",
            "Established in 2009 and granted full university status in 2014, SIT integrates work and study through its Integrated Work Study Programme, enabling students to tackle real industry challenges. Spread across six campuses—including its Punggol Campus at 1 Punggol Coast Road—SIT collaborates with nine overseas universities such as Newcastle University and DigiPen co-deliver applied degrees, nurturing graduates who are job- and future-ready.",
          ]}
        />
      </div>
    </div>
  );
}