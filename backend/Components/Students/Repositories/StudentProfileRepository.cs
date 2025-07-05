using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Components.Student.Repositories.Interfaces;
using backend.Components.Student.Models;

namespace backend.Components.Student.Repositories
{
    public class StudentProfileRepository : IStudentProfileRepository
    {
        private readonly ApplicationDbContext _db;

        public StudentProfileRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<StudentProfile?> GetByEmailAsync(string email)
        {
            return await _db.StudentProfiles
                            .FirstOrDefaultAsync(sp => sp.Email == email);
        }
    }
}