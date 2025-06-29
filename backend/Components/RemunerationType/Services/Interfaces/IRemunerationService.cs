namespace backend.RemunerationType.Services.Interfaces;

using backend.RemunerationType.Models;
using backend.RemunerationType.DTOs;

public interface IRemunerationTypeService
{
    Task<RemunerationType> CreateRemunerationTypeAsync(string type);
    Task<RemunerationType?> GetRemunerationTypeByIdAsync(int remunerationId);
    Task<RemunerationType> UpdateRemunerationTypeAsync(int remunerationId, string type);
    Task<bool> DeleteRemunerationTypeAsync(int remunerationId);
    Task<List<RemunerationType>> GetAllRemunerationTypesAsync();
    RemunerationTypeResponseDTO ConvertToResponseDTO(RemunerationType remunerationType);
    Task<List<RemunerationTypeResponseDTO>> GetAllRemunerationTypeDTOsAsync();
}