using System.Threading.Tasks;
using backend.Components.Student.Repositories.Interfaces;
using backend.Components.Student.Services.Interfaces;
using backend.Components.Student.DTOs;

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

         public async Task<StudentProfileDTO?> GetProfileByEmailAsync(string email)
        {
            var profile = await _repo.GetByEmailAsync(email);
            if (profile == null) 
                return null;

            return new StudentProfileDTO
            {
                NricFin              = profile.NricFin,
                StudentId            = profile.StudentId,
                Nationality          = profile.Nationality,
                AdmitYear            = profile.AdmitYear,
                PrimaryContactNumber = profile.PrimaryContactNumber,
                Gender               = profile.Gender,
                DegreeProgramme      = profile.DegreeProgramme,
                FullName             = profile.FullName,
                Email                = profile.Email
            };
        }
    }
}