namespace backend.Components.Company.Services.Interfaces;

using backend.Components.Company.Models;

public interface ICompanyService
{
    Task<List<Company>> GetAllCompaniesAsync();
    Task<Company?> GetCompanyByIdAsync(int companyId);
    Task<Company> CreateCompanyAsync(Company company);
    Task<Company> UpdateCompanyAsync(Company company);
    Task<bool> DeleteCompanyAsync(int companyId);
    Task<Company?> GetCompanyByUENAsync(string uen);
}
