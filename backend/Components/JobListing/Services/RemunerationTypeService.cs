namespace backend.Components.JobListing.Services;

using backend.Components.JobListing.Models;
using backend.Components.JobListing.Services.Interfaces;
using backend.Components.JobListing.Repositories.Interfaces;

public class RemunerationTypeService : IRemunerationTypeService
{
    private readonly IRemunerationTypeRepository _remunerationTypeRepository;
    
    // constructor injection for repository
    public RemunerationTypeService(IRemunerationTypeRepository remunerationTypeRepository)
    {
        _remunerationTypeRepository = remunerationTypeRepository;
    }
    
    // creates a new remuneration type
    public async Task<RemunerationType> CreateRemunerationTypeAsync(string type)
    {
        if (string.IsNullOrWhiteSpace(type))
            throw new ArgumentException("Remuneration type cannot be empty");
            
        // Check if type already exists
        var existingTypes = await GetAllRemunerationTypesAsync();
        if (existingTypes.Any(r => r.GetRemunerationType().Equals(type, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("This remuneration type already exists");
            
        var remunerationType = new RemunerationType(type);
        
        // save the remuneration type record to db
        return await _remunerationTypeRepository.CreateAsync(remunerationType);
    }
    
    // retrieves a remuneration type by ID
    public async Task<RemunerationType?> GetRemunerationTypeByIdAsync(int remunerationId)
    {
        if (remunerationId <= 0) return null;

        return await _remunerationTypeRepository.GetByIdAsync(remunerationId);
    }
    
    // updates a remuneration type
    public async Task<RemunerationType> UpdateRemunerationTypeAsync(int remunerationId, string type)
    {
        var remunerationType = await GetRemunerationTypeByIdAsync(remunerationId);
        if (remunerationType == null) 
            throw new ArgumentException("Remuneration type does not exist");
        
        if (string.IsNullOrWhiteSpace(type))
            throw new ArgumentException("Remuneration type cannot be empty");
            
        // Check if updated type would create a duplicate
        var existingTypes = await GetAllRemunerationTypesAsync();
        if (existingTypes.Any(r => r.GetRemunerationType().Equals(type, StringComparison.OrdinalIgnoreCase) && 
                                   r.GetRemunerationId() != remunerationId))
            throw new InvalidOperationException("Another remuneration type with this name already exists");
        
        // update the type
        remunerationType.SetType(type);
        
        return await _remunerationTypeRepository.UpdateAsync(remunerationType);
    }
    
    // deletes a remuneration type
    public async Task<bool> DeleteRemunerationTypeAsync(int remunerationId)
    {
        if (remunerationId <= 0) return false;

        return await _remunerationTypeRepository.DeleteAsync(remunerationId);
    }
    
    // gets all remuneration types
    public async Task<List<RemunerationType>> GetAllRemunerationTypesAsync()
    {
        return await _remunerationTypeRepository.GetAllAsync();
    }
    
    // converts to DTO for frontend
    // public RemunerationTypeResponseDTO ConvertToResponseDTO(RemunerationType remunerationType)
    // {
    //     return new RemunerationTypeResponseDTO
    //     {
    //         RemunerationId = remunerationType.GetRemunerationId(),
    //         Type = remunerationType.GetType()
    //     };
    // }
    
    // // gets all remuneration types as DTOs - used by controllers
    // public async Task<List<RemunerationTypeResponseDTO>> GetAllRemunerationTypeDTOsAsync()
    // {
    //     var remunerationTypes = await GetAllRemunerationTypesAsync();
    //     return remunerationTypes.Select(ConvertToResponseDTO).ToList();
    // }
}