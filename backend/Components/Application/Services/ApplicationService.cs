namespace backend.Components.Application.Services;

using backend.Components.AI.Services;
using backend.Components.Application.Models;
using backend.Components.Application.Repository;
using backend.User.Services.Interfaces;

public class ApplicationService : IApplicationService
{
    private readonly IAIService _aiService;
    private readonly IUserService _userService;
    private readonly IApplicationRepository _applicationRepository;
    private readonly ILogger<ApplicationService> _logger;

    public ApplicationService(
        IAIService aiService,
        IUserService userService,
        IApplicationRepository applicationRepository,
        ILogger<ApplicationService> logger)
    {
        _aiService = aiService;
        _userService = userService;
        _applicationRepository = applicationRepository;
        _logger = logger;
    }

    public async Task<ApplicationResult> SubmitApplicationWithCoverLetterAsync(int applicantId, int jobListingId)
    {
        try
        {
            // 1. Check if application already exists
            var existingApplication = await _applicationRepository.GetByApplicantAndJobAsync(applicantId, jobListingId);
            if (existingApplication != null)
            {
                return new ApplicationResult
                {
                    Success = false,
                    Message = "You have already applied for this position."
                };
            }

            // 2. Get user details - For testing, skip user validation and use hardcoded name
            string applicantName = "April Ludgate"; // Hardcoded for testing
            
            // Uncomment this when you have users in database:
            /*
            var user = await _userService.GetUserByIdAsync(applicantId);
            if (user == null)
            {
                return new ApplicationResult
                {
                    Success = false,
                    Message = "Applicant not found."
                };
            }
            var applicantName = $"{user.GetFirstName()} {user.GetLastName()}";
            */

            // 3. Get job details (using mock data based on jobListingId)
            var (jobTitle, jobDescription) = GetMockJobData(jobListingId);

            // 4. Get resume keywords (using the extracted keywords from the text file)
            var resumeKeywords = GetApplicantResumeKeywords(applicantId);

            // 5. Generate cover letter using AI
            _logger.LogInformation("Generating cover letter for applicant {ApplicantId} and job {JobId}", applicantId, jobListingId);
            
            var coverLetter = await _aiService.GenerateCoverLetterAsync(
                resumeKeywords,
                jobTitle,
                jobDescription,
                applicantName
            );

            // 6. Create and save application
            var application = new JobApplication(
                applicantId,
                jobListingId,
                coverLetter,
                "pending"
            );

            var savedApplication = await _applicationRepository.CreateAsync(application);

            _logger.LogInformation("Application submitted successfully with ID {ApplicationId}", savedApplication.ApplicationId);

            return new ApplicationResult
            {
                Success = true,
                Message = "Application submitted successfully with AI-generated cover letter!",
                GeneratedCoverLetter = coverLetter,
                ApplicationId = savedApplication.ApplicationId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error submitting application for applicant {ApplicantId} and job {JobId}", applicantId, jobListingId);
            return new ApplicationResult
            {
                Success = false,
                Message = "An error occurred while submitting your application. Please try again."
            };
        }
    }

    private (string jobTitle, string jobDescription) GetMockJobData(int jobListingId)
    {
        // Mock job data based on jobListingId
        // In real implementation, this would query the database
        return jobListingId switch
        {
            1 => (
                "EDE2022 Probability & Statistical Signal Processing Student Coach",
                "Student coach opportunity for EDE2022 Probability & Statistical Signal Processing — we are seeking EDE students who possess a strong understanding of EDE2022 Probability & Statistical Signal Processing module. The chosen student will serve as tutors, assisting and mentoring Year 1 student on the following dates to help them prepare for their upcoming exams. Date: 7 Jul 10 am to 12pm. Requirements include strong analytical skills, ability to explain complex concepts clearly, and patience in working with students. This is an excellent opportunity to develop teaching skills while helping fellow students succeed academically."
            ),
            2 => (
                "Research Assistant – Food Innovation Project",
                "We are seeking a dedicated research assistant to support our food innovation project. The role involves conducting experiments, analyzing data, and assisting with product development. Perfect for students with a background in food science or related fields. Responsibilities include laboratory work, data collection, sensory testing, and documentation of research findings. The successful candidate will work closely with senior researchers and gain valuable hands-on experience in food technology and product development."
            ),
            3 => (
                "Library Support Assistant (Evening Shift)",
                "Support library operations during evening hours. Responsibilities include assisting students with research, maintaining library resources, and providing general administrative support. Great opportunity for students looking to develop customer service and organizational skills. Tasks include helping students locate resources, managing book circulation, organizing materials, and maintaining a quiet study environment. Strong communication skills and attention to detail are essential."
            ),
            4 => (
                "Photography Crew – Graduation Ceremony",
                "Join our photography team for the upcoming graduation ceremony. We need skilled photographers to capture important moments during the event. This role requires experience with event photography, ability to work in various lighting conditions, and professional equipment handling. The successful candidate will be responsible for photographing key ceremony moments, individual graduate photos, and family portraits. Must be available for the full duration of the ceremony and possess strong attention to detail."
            ),
            5 => (
                "Marketing Assistant (Social Media Support)",
                "Assist the marketing team with social media campaigns and content creation. Responsibilities include creating engaging content, managing social media accounts, analyzing engagement metrics, and supporting promotional activities. Ideal for students studying marketing, communications, or related fields. The role offers hands-on experience with digital marketing tools, content creation platforms, and social media analytics. Creative thinking and strong writing skills are essential."
            ),
            6 => (
                "IT Support Intern – Campus Tech Helpdesk",
                "Provide technical support to students and staff across campus. Responsibilities include troubleshooting hardware and software issues, maintaining computer labs, and assisting with technology setup for events. This position is perfect for students studying computer science or information technology. The role involves working with various operating systems, network troubleshooting, and customer service. Strong problem-solving skills and patience when helping users are essential."
            ),
            7 => (
                "Student Technician – Makerspace",
                "Support the campus makerspace operations by assisting students with equipment use, maintaining tools and machinery, and ensuring safety protocols are followed. This role is ideal for students with hands-on technical skills and an interest in fabrication and prototyping. Responsibilities include equipment training, project assistance, inventory management, and workspace organization. Experience with 3D printing, laser cutting, or woodworking is preferred but training will be provided."
            ),
            8 => (
                "Lab Technician Assistant",
                "Assist in laboratory operations across various science departments. Responsibilities include preparing laboratory materials, maintaining equipment, and supporting faculty with research activities. This position is excellent for students studying chemistry, biology, or related sciences. Tasks include sample preparation, equipment calibration, data recording, and ensuring laboratory safety standards. Attention to detail and adherence to safety protocols are crucial for this role."
            ),
            _ => (
                "General Student Assistant Position",
                "A versatile student assistant role designed to support various campus operations. This position offers flexibility and exposure to different departments and functions within the institution. Responsibilities may include administrative support, event assistance, data entry, and special projects as assigned. This role is perfect for students looking to gain broad experience and develop transferable skills while contributing to campus operations."
            )
        };
    }

    private string GetApplicantResumeKeywords(int applicantId)
    {
        // TODO: Replace with actual database query when you have resume parsing
        // For now, using the extracted keywords from April Ludgate's resume as provided in the text file
        return @"PROFESSIONAL IDENTITY:
- Dedicated
- Experienced
- Professional
- Student Intern
- Background
- Public administration
- Animal welfare

CORE COMPETENCIES:
- Manage projects
- Supervise staff
- Coordinate between departments
- Proven ability
- Challenging internship
- Develop skills
- Contribute
- Organizational goals

SPECIFIC EXPERIENCE:
- Deputy Director
- Animal Control
- Managed daily operations
- Department
- Implemented
- Animal welfare policies
- Supervised
- Trained new staff
- Coordinated
- City departments
- Joint initiatives
- Assisted
- Various departments
- Parks and Recreation
- Administrative tasks
- Planning
- Executing
- City events
- Different city departments

EDUCATIONAL BACKGROUND:
- Bachelor's Degree
- Forestry
- Environmental Studies
- Animal welfare
- Environmental conservation
- Environmental initiatives
- Environmental club
- Community service activities

PERSONAL ATTRIBUTES:
- Seeking
- Challenging internship
- Further develop skills
- Contribute
- Organizational goals

ADDITIONAL SKILLS:
- Hiking
- Reading
- Photography
- Gardening
- Animal Care
- English
- Spanish
- French
- Multilingual

PROFESSIONAL REFERENCES:
- Leslie Knope
- Pawnee City Hall

CONTACT INFORMATION:
- April Ludgate
- Pawnee, Indiana
- Phone: +1 234 567 890
- Email: april.ludgate@example.com";
    }
}