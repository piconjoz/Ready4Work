namespace backend.Components.Company.Repository;

using Microsoft.EntityFrameworkCore;
using backend.Components.Company.Models;
using backend.Data;

/// <summary>
/// implements the ICompanyRepository interface to provide
/// data access logic for Company entities using EF
/// this repo handles CRUD operations
/// interacting directly with the ApplicationDBContext
/// </summary>

public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationDbContext _context;

    public CompanyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Company>> GetAllCompaniesAsync()
    {
        return await _context.Companies.ToListAsync();
    }

    public async Task<Company?> GetCompanyByIdAsync(int companyId)
    {
        return await _context.Companies.FindAsync(companyId);
    }

    public async Task<Company> CreateAsync(Company company)
    {
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();
        return company;
    }

    public async Task<Company> UpdateAsync(Company company)
    {
        _context.Entry(company).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return company;
    }

    public async Task<bool> DeleteAsync(int companyId)
    {
        var company = await _context.Companies.FindAsync(companyId);
        if (company == null)
        {
            return false;
        }

        _context.Companies.Remove(company);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Company?> GetCompanyByUENAsync(string uen)
    {
        return await _context.Companies.FirstOrDefaultAsync(c => c.UEN == uen);
    }
}
