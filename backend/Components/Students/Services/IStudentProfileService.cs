using System.Threading.Tasks;

namespace backend.Components.Student.Services.Interfaces
{
    public interface IStudentProfileService
    {
        Task<bool> StudentExistsAsync(string email);
    }
}