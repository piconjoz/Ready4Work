namespace backend.Components.Company.Repository;

using backend.Components.Company.Models;

public interface ICompanyRepository
{
    Task<List<Company>> GetAllCompaniesAsync();
    Task<Company?> GetCompanyByIdAsync(int companyId);
    Task<Company> CreateAsync(Company company);
    Task<Company> UpdateAsync(Company company);
    Task<bool> DeleteAsync(int companyId);
}