namespace backend.Components.Company.Services;

using backend.Components.Company.Models;
using backend.Components.Company.Repository;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }
    
    public async Task<List<Company>> GetAllCompaniesAsync()
    {
        return await _companyRepository.GetAllCompaniesAsync();
    }
    
    public async Task<Company?> GetCompanyByIdAsync(int companyId)
    {
        return await _companyRepository.GetCompanyByIdAsync(companyId);
    }

    public async Task<Company> CreateCompanyAsync(Company company)
    {
        // business logic should be added inside here (?)
        company.CreatedAt = DateTime.UtcNow;
        company.UpdatedAt = DateTime.UtcNow;
        
        return await _companyRepository.CreateAsync(company);
    }
    
    public async Task<Company> UpdateCompanyAsync(Company company)
    {
        // business logic should be added inside here (?)
        company.UpdatedAt = DateTime.UtcNow;
        
        return await _companyRepository.UpdateAsync(company);
    }
    
    public async Task<bool> DeleteCompanyAsync(int companyId)
    {
        // business logic should be added inside here (?)
        return await _companyRepository.DeleteAsync(companyId);
    }
}