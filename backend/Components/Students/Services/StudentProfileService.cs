using System.Threading.Tasks;
using backend.Components.Student.Repositories.Interfaces;
using backend.Components.Student.Services.Interfaces;

namespace backend.Components.Student.Services
{
    public class StudentProfileService : IStudentProfileService
    {
        private readonly IStudentProfileRepository _repo;

        public StudentProfileService(IStudentProfileRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> StudentExistsAsync(string email)
        {
            var profile = await _repo.GetByEmailAsync(email);
            return profile != null;
        }
    }
}