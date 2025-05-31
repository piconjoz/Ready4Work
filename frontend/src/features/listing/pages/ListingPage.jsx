import Header from "../../../components/Header";
import DetailedJobCard from '../../../components/DetailedJobCard';
// import JobCard from "../../../components/JobCard";
import SITLogo from "../../../assets/sit_com_ico.svg";
import AlertNote from "../../../components/AlertNote";
import InfoCard from "../../../components/InfoCard";
import DescSection from "../../../components/DescSection";


export default function ListingPage() {
  return (
      <div className="min-h-screen bg-[#F8F9FD] px-6 md:px-10">
          <Header active="applications" />
            {/* Recommended */}
          <div className="py-6">
            <DetailedJobCard
                logo= {SITLogo}
                title="EDE2022 Probability & Statistical Signal Processing"
                employer="Singapore Institute Of Technology"
                scheme="SIT Student Work Scheme"
                daysRemaining={12}
                badges={[
                    { label: "$15/Hour" },
                    { label: "2 Vacancies", color: "bg-[#F5F5F5] border-gray-300" },
                    { label: "18/06/2025" },
                ]}
            status="applied"
            />
            <AlertNote
            title="Applied"
            message="View the Applications page to find out the result of your application."
            bgColor="#0E8345"
            />

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
                "No Requirements Specified",
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
                "Singapore Institute of Technology (SIT) is Singapore’s University of Applied Learning, offering applied degree programmes across five clusters: Engineering; Food, Chemical & Biotechnology; Infocomm Technology; Health & Social Sciences; Business, Communication & Design.",
                "Established in 2009 and granted full university status in 2014, SIT integrates work and study through its Integrated Work Study Programme, enabling students to tackle real industry challenges. Spread across six campuses—including its Punggol Campus at 1 Punggol Coast Road—SIT collaborates with nine overseas universities such as Newcastle University and DigiPen co-deliver applied degrees, nurturing graduates who are job- and future-ready.",
                ]}
            />

          </div>
      </div>
  );
}