// backend/Components/Application/Services/ApplicationService.cs
namespace backend.Components.Application.Services;

using backend.Components.AI.Services;
using backend.Components.Application.Models;
using backend.Components.Application.Repository;
using backend.Components.CoverLetter.Services;
using backend.Components.CoverLetter.Repository;
using backend.User.Services.Interfaces;

public class ApplicationService : IApplicationService
{
    private readonly IAIService _aiService;
    private readonly IUserService _userService;
    private readonly IApplicationRepository _applicationRepository;
    private readonly ICoverLetterService _coverLetterService;
    private readonly ICoverLetterRepository _coverLetterRepository;
    private readonly ILogger<ApplicationService> _logger;

    public ApplicationService(
        IAIService aiService,
        IUserService userService,
        IApplicationRepository applicationRepository,
        ICoverLetterService coverLetterService,
        ICoverLetterRepository coverLetterRepository,
        ILogger<ApplicationService> logger)
    {
        _aiService = aiService;
        _userService = userService;
        _applicationRepository = applicationRepository;
        _coverLetterService = coverLetterService;
        _coverLetterRepository = coverLetterRepository;
        _logger = logger;
    }

    public async Task<ApplicationResult> SubmitApplicationWithCoverLetterAsync(int applicantId, int jobListingId)
    {
        try
        {
            _logger.LogInformation("Starting application submission for applicant {ApplicantId} and job {JobId}", applicantId, jobListingId);

            // 1. Check if application already exists
            var existingApplication = await _applicationRepository.GetByApplicantAndJobAsync(applicantId, jobListingId);
            if (existingApplication != null)
            {
                _logger.LogWarning("Duplicate application attempt for applicant {ApplicantId} and job {JobId}", applicantId, jobListingId);
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
                _logger.LogWarning("Applicant not found: {ApplicantId}", applicantId);
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
            _logger.LogInformation("Retrieved job data: {JobTitle} for job {JobId}", jobTitle, jobListingId);

            // 4. Get resume keywords (using the extracted keywords from the text file)
            var resumeKeywords = GetApplicantResumeKeywords(applicantId);

            // 5. Generate cover letter using AI
            _logger.LogInformation("Generating AI cover letter for applicant {ApplicantId} and job {JobId}", applicantId, jobListingId);
            
            var coverLetterText = await _aiService.GenerateCoverLetterAsync(
                resumeKeywords,
                jobTitle,
                jobDescription,
                applicantName
            );

            if (string.IsNullOrEmpty(coverLetterText))
            {
                _logger.LogError("AI service returned empty cover letter for applicant {ApplicantId}", applicantId);
                return new ApplicationResult
                {
                    Success = false,
                    Message = "Failed to generate cover letter. Please try again."
                };
            }

            _logger.LogInformation("Successfully generated cover letter text ({Length} characters)", coverLetterText.Length);

            // 6. Generate PDF and save cover letter entity
            _logger.LogInformation("Creating cover letter PDF for applicant {ApplicantId}", applicantId);
            
            var coverLetter = await _coverLetterService.GenerateAndSaveCoverLetterAsync(
                coverLetterText,
                applicantName,
                jobTitle,
                applicantId
            );

            _logger.LogInformation("Cover letter PDF created with ID {CoverLetterId}", coverLetter.CoverLetterId);

            // 7. Create and save application with cover letter reference
            _logger.LogInformation("Creating job application record for applicant {ApplicantId}", applicantId);
            
            var application = new JobApplication(
                applicantId,
                jobListingId,
                coverLetter.CoverLetterId,  // Reference to CoverLetter entity (follows ER diagram)
                "pending"
            );

            var savedApplication = await _applicationRepository.CreateAsync(application);
            _logger.LogInformation("Job application created with ID {ApplicationId}", savedApplication.ApplicationId);

            _logger.LogInformation("Application submission completed successfully for applicant {ApplicantId} and job {JobId}. Application ID: {ApplicationId}, Cover Letter ID: {CoverLetterId}", 
                applicantId, jobListingId, savedApplication.ApplicationId, coverLetter.CoverLetterId);

            return new ApplicationResult
            {
                Success = true,
                Message = "Application submitted successfully with AI-generated cover letter PDF!",
                GeneratedCoverLetter = coverLetterText,
                ApplicationId = savedApplication.ApplicationId,
                CoverLetterId = coverLetter.CoverLetterId  // Include cover letter ID for download
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
            9 => (
                "Campus Event Coordinator Assistant",
                "Support the events team in planning and executing campus-wide activities and ceremonies. Responsibilities include coordinating with vendors, managing event logistics, and providing on-site support during events. This role is perfect for students interested in event management and hospitality. Tasks include venue setup, attendee registration, equipment management, and post-event cleanup. Strong organizational skills and ability to work under pressure are essential."
            ),
            10 => (
                "Digital Content Creator Intern",
                "Join our digital marketing team to create engaging content for social media platforms and the university website. Responsibilities include photography, video editing, graphic design, and content writing. This position is ideal for students studying communications, marketing, or digital media. Tasks include creating social media posts, editing promotional videos, designing flyers, and writing blog articles. Creative thinking and proficiency with design software are required."
            ),
            _ => (
                "General Student Assistant Position",
                "A versatile student assistant role designed to support various campus operations. This position offers flexibility and exposure to different departments and functions within the institution. Responsibilities may include administrative support, event assistance, data entry, and special projects as assigned. This role is perfect for students looking to gain broad experience and develop transferable skills while contributing to campus operations. Strong communication skills and adaptability are essential."
            )
        };
    }

    private string GetApplicantResumeKeywords(int applicantId)
    {
        // TODO: Replace with actual database query when you have resume parsing
        // For now, using the extracted keywords from April Ludgate's resume as provided in the text file
        
        // In a real implementation, this would:
        // 1. Query the Resumes table for the applicant's latest resume
        // 2. Parse the ResumeText field to extract keywords
        // 3. Return structured keywords for AI processing
        
        return @"PROFESSIONAL IDENTITY:
- Dedicated and experienced professional
- Student intern with strong background
- Public administration and animal welfare expertise
- Proven track record in leadership roles

CORE COMPETENCIES:
- Project management and coordination
- Staff supervision and training
- Cross-departmental collaboration
- Policy implementation and development
- Problem-solving and analytical thinking
- Customer service excellence
- Administrative task management
- Event planning and execution

SPECIFIC EXPERIENCE:
- Deputy Director of Animal Control
- Managed daily operations of city department
- Implemented comprehensive animal welfare policies
- Supervised and trained new staff members
- Coordinated with multiple city departments
- Led joint initiatives and special projects
- Assisted various departments including Parks and Recreation
- Extensive experience in administrative tasks
- Event planning and execution expertise
- Strong inter-departmental communication skills

EDUCATIONAL BACKGROUND:
- Bachelor's Degree in Forestry and Environmental Studies
- Focus on animal welfare and environmental conservation
- Active participation in environmental initiatives
- Leadership in environmental club activities
- Community service and volunteer experience
- Academic excellence in science-related coursework

PERSONAL ATTRIBUTES:
- Seeking challenging internship opportunities
- Committed to skill development and growth
- Strong desire to contribute to organizational goals
- Excellent work ethic and reliability
- Team player with leadership capabilities
- Detail-oriented and organized approach

ADDITIONAL SKILLS:
- Outdoor activities: hiking and nature photography
- Personal interests: reading and gardening
- Animal care and welfare expertise
- Multilingual abilities: English, Spanish, and French
- Photography and visual documentation skills
- Environmental awareness and sustainability focus

PROFESSIONAL REFERENCES:
- Leslie Knope, Pawnee City Hall
- Extensive network of professional contacts
- Strong recommendations from supervisors
- Proven track record of successful collaborations

CONTACT INFORMATION:
- April Ludgate
- Located in Pawnee, Indiana
- Phone: +1 234 567 890
- Email: april.ludgate@example.com
- Available for interviews and immediate start";
    }

    // Additional helper methods for potential future use

    private bool IsValidJobListingId(int jobListingId)
    {
        // In real implementation, this would query the JobListings table
        return jobListingId > 0 && jobListingId <= 10;
    }

    private async Task<bool> HasUserAppliedRecently(int applicantId)
    {
        // In real implementation, this could check if user has applied to multiple jobs recently
        // to prevent spam applications
        try
        {
            var recentApplications = await _applicationRepository.GetByApplicantIdAsync(applicantId);
            var todayApplications = recentApplications.Where(a => a.AppliedDate.Date == DateTime.Today).Count();
            
            // Allow max 5 applications per day
            return todayApplications >= 5;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking recent applications for applicant {ApplicantId}", applicantId);
            return false; // Allow application if we can't check
        }
    }

    private string SanitizeInput(string input)
    {
        // Basic input sanitization
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;
            
        return input.Trim();
    }
}