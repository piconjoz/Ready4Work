using System.Threading.Tasks;
using backend.Components.Student.DTOs;

namespace backend.Components.Student.Services.Interfaces
{
    public interface IStudentProfileService
    {
        Task<bool> StudentExistsAsync(string email);

        Task<StudentProfileDTO?> GetProfileByEmailAsync(string email);
    }
}