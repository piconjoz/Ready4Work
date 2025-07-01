namespace backend.Components.CoverLetter.Services;

using backend.Components.CoverLetter.Models;
using backend.Components.CoverLetter.Repository;
using Microsoft.AspNetCore.Hosting;
using iText.Html2pdf;
using System.Text;

public class CoverLetterService : ICoverLetterService
{
    private readonly IWebHostEnvironment _environment;
    private readonly ICoverLetterRepository _coverLetterRepository;
    private readonly ILogger<CoverLetterService> _logger;

    public CoverLetterService(
        IWebHostEnvironment environment, 
        ICoverLetterRepository coverLetterRepository, 
        ILogger<CoverLetterService> logger)
    {
        _environment = environment;
        _coverLetterRepository = coverLetterRepository;
        _logger = logger;
    }

    public async Task<CoverLetter> GenerateAndSaveCoverLetterAsync(string content, string applicantName, string jobTitle, int applicantId)
    {
        try
        {
            _logger.LogInformation("Generating cover letter PDF for applicant {ApplicantId}", applicantId);

            // Create HTML template for the cover letter
            var htmlContent = CreateCoverLetterHtml(content, applicantName, jobTitle);
            
            // Generate unique filename
            var fileName = $"cover_letter_{applicantId}_{DateTime.UtcNow:yyyyMMdd_HHmmss}.pdf";
            var uploadsDir = Path.Combine(_environment.ContentRootPath, "uploads", "cover_letters");
            Directory.CreateDirectory(uploadsDir);
            var filePath = Path.Combine(uploadsDir, fileName);
            
            // Convert HTML to PDF
            var pdfBytes = ConvertHtmlToPdf(htmlContent);
            await File.WriteAllBytesAsync(filePath, pdfBytes);
            
            _logger.LogInformation("Cover letter PDF saved to {FilePath}", filePath);

            // Create CoverLetter entity (without ApplicationId initially)
            var coverLetter = new CoverLetter(
                applicantId,
                filePath,
                content,
                pdfBytes.Length
            );

            return await _coverLetterRepository.CreateAsync(coverLetter);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating cover letter PDF for applicant {ApplicantId}", applicantId);
            throw;
        }
    }

    private string CreateCoverLetterHtml(string content, string applicantName, string jobTitle)
    {
        // Format the content into proper paragraphs
        var formattedContent = FormatContentToParagraphs(content);
        
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <style>
        body {{ 
            font-family: 'Times New Roman', serif; 
            font-size: 12pt; 
            line-height: 1.6; 
            max-width: 8.5in; 
            margin: 0; 
            padding: 1in;
            color: #333;
        }}
        .header {{ 
            text-align: center; 
            margin-bottom: 40px; 
            border-bottom: 2px solid #2c3e50;
            padding-bottom: 20px;
        }}
        .content {{ 
            text-align: justify; 
            margin-bottom: 40px;
        }}
        .signature {{ 
            margin-top: 60px; 
        }}
        h1 {{ 
            color: #2c3e50; 
            font-size: 20pt; 
            margin-bottom: 10px;
            font-weight: bold;
        }}
        .job-info {{
            font-size: 11pt;
            color: #555;
            margin-bottom: 5px;
        }}
        .date {{
            font-size: 10pt;
            color: #777;
        }}
        p {{ 
            margin-bottom: 16px; 
            text-indent: 0;
        }}
        .signature-line {{
            margin-top: 20px;
            border-top: 1px solid #ccc;
            width: 200px;
            padding-top: 5px;
        }}
    </style>
</head>
<body>
    <div class='header'>
        <h1>Cover Letter</h1>
        <div class='job-info'><strong>Position:</strong> {jobTitle}</div>
        <div class='job-info'><strong>Applicant:</strong> {applicantName}</div>
        <div class='date'><strong>Date:</strong> {DateTime.Now:MMMM dd, yyyy}</div>
    </div>
    
    <div class='content'>
        {formattedContent}
    </div>
    
    <div class='signature'>
        <p>Sincerely,</p>
        <div class='signature-line'>
            <strong>{applicantName}</strong>
        </div>
    </div>
</body>
</html>";
    }

    private string FormatContentToParagraphs(string content)
    {
        // Split by line breaks and create proper paragraphs
        var lines = content.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        var paragraphs = new StringBuilder();
        
        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();
            if (!string.IsNullOrEmpty(trimmedLine))
            {
                paragraphs.AppendLine($"<p>{trimmedLine}</p>");
            }
        }
        
        return paragraphs.ToString();
    }

    private byte[] ConvertHtmlToPdf(string htmlContent)
    {
        using var memoryStream = new MemoryStream();
        HtmlConverter.ConvertToPdf(htmlContent, memoryStream);
        return memoryStream.ToArray();
    }

    public async Task<byte[]> GetCoverLetterPdfAsync(int coverLetterId)
    {
        var coverLetter = await _coverLetterRepository.GetByIdAsync(coverLetterId);
        if (coverLetter == null)
        {
            throw new ArgumentException($"Cover letter with ID {coverLetterId} not found");
        }
        
        if (!File.Exists(coverLetter.CoverLetterPath))
        {
            throw new FileNotFoundException($"Cover letter PDF file not found at {coverLetter.CoverLetterPath}");
        }
        
        return await File.ReadAllBytesAsync(coverLetter.CoverLetterPath);
    }

    public async Task<string> GetCoverLetterDownloadUrlAsync(int coverLetterId)
    {
        var coverLetter = await _coverLetterRepository.GetByIdAsync(coverLetterId);
        if (coverLetter == null)
        {
            throw new ArgumentException($"Cover letter with ID {coverLetterId} not found");
        }
        
        return $"/api/cover-letters/{coverLetterId}/download";
    }

    public async Task<bool> DeleteCoverLetterAsync(int coverLetterId)
    {
        return await _coverLetterRepository.DeleteAsync(coverLetterId);
    }
}