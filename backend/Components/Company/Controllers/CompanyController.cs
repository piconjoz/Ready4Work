namespace backend.Components.Company.Controllers;

using Microsoft.AspNetCore.Mvc;
using backend.Components.Company.Models;
using backend.Components.Company.Services;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }
    
    // get all companies
    [HttpGet]
    public async Task<ActionResult<List<Company>>> GetAllCompanies()
    {
        var companies = await _companyService.GetAllCompaniesAsync();
        return Ok(companies);
    }
    
    // get company by id
    [HttpGet("{companyId}")]
    public async Task<ActionResult<Company?>> GetCompanyById(int companyId)
    {
        var company = await _companyService.GetCompanyByIdAsync(companyId);
        if (company == null)
        {
            return NotFound();
        }
        return Ok(company);
    }
    
    // create a new company
    [HttpPost]
    public async Task<ActionResult<Company>> CreateCompany(Company company)
    {
        var createdCompany = await _companyService.CreateCompanyAsync(company);
        return CreatedAtAction(nameof(GetCompanyById), new { companyId = createdCompany.CompanyId }, createdCompany);
    }
    
    // update an existing company
    [HttpPut("{companyId}")]
    public async Task<ActionResult<Company>> UpdateCompany(int companyId, Company company)
    {
        if (companyId != company.CompanyId)
        {
            return BadRequest();
        }

        var updatedCompany = await _companyService.UpdateCompanyAsync(company);
        return Ok(updatedCompany);
    }
    
    // delete a company
    [HttpDelete("{companyId}")]
    public async Task<IActionResult> DeleteCompany(int companyId)
    {
        var result = await _companyService.DeleteCompanyAsync(companyId);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
