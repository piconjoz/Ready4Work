namespace backend.Components.Company.Models;

/// <summary>
/// this is a company entity with its core attributes. kept inside models folder.
/// </summary>
public class Company
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string PreferredCompanyName { get; set; } = string.Empty;
    public string CompanyDescription { get; set; } = string.Empty;
    public string CountryOfBusinessRegistration { get; set; } = string.Empty;
    public string UEN { get; set; } = string.Empty;
    public int NumberOfEmployees { get; set; }
    public string IndustryCluster { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty;
    public bool AuthorisedTrainingOrganisation { get; set; }
    public string CompanyWebsite { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
