using System.Threading.Tasks;
using backend.Components.Student.Models;

namespace backend.Components.Student.Repositories.Interfaces
{
public interface IStudentProfileRepository
{
    Task<StudentProfile?> GetByEmailAsync(string email);
}
}