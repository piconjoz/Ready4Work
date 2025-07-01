namespace backend.Components.AI.Services;

using System.Text;
using System.Text.Json;

public class AIService : IAIService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AIService> _logger;

    public AIService(HttpClient httpClient, IConfiguration configuration, ILogger<AIService> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<string> GenerateCoverLetterAsync(string resumeKeywords, string jobTitle, string jobDescription, string applicantName)
    {
        try
        {
            _logger.LogInformation("Generating cover letter for {ApplicantName} for position {JobTitle}", applicantName, jobTitle);

            // For now, let's use OpenAI (you can switch to Claude later)
            return await GenerateWithOpenAI(resumeKeywords, jobTitle, jobDescription, applicantName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating cover letter");
            throw;
        }
    }

    private async Task<string> GenerateWithOpenAI(string resumeKeywords, string jobTitle, string jobDescription, string applicantName)
    {
        var apiKey = _configuration["AI:OpenAI:ApiKey"];
        
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("OpenAI API key not configured");
        }

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

        var prompt = CreateCoverLetterPrompt(resumeKeywords, jobTitle, jobDescription, applicantName);

        var requestBody = new
        {
            model = "gpt-3.5-turbo", // Using cheaper model for testing
            messages = new[]
            {
                new { role = "system", content = "You are a professional cover letter writer. Create personalized, compelling cover letters that highlight relevant experience and skills." },
                new { role = "user", content = prompt }
            },
            max_tokens = 500,
            temperature = 0.7
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var responseData = JsonSerializer.Deserialize<JsonElement>(responseContent);

        return responseData
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString() ?? "Failed to generate cover letter";
    }

    private string CreateCoverLetterPrompt(string resumeKeywords, string jobTitle, string jobDescription, string applicantName)
    {
        return $@"Create a professional cover letter for {applicantName} applying for the position of '{jobTitle}'.

Job Description:
{jobDescription}

Applicant's Background (extracted from resume):
{resumeKeywords}

Requirements:
1. Write in first person from {applicantName}'s perspective
2. Keep it professional and concise (3-4 paragraphs)
3. Highlight relevant skills and experience that match the job requirements
4. Show enthusiasm for the role and company
5. Include a strong opening and closing
6. Format as a complete cover letter ready to send

Please generate a compelling cover letter that effectively matches the applicant's background to this specific job opportunity.";
    }
}