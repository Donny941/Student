using Microsoft.EntityFrameworkCore;
using StudentDashboard.Models.Entities;

namespace StudentDashboard.Services
{
    public class StudentService : ServiceBase
    {
        public StudentService(StudentDbContext studentDbContext) : base(studentDbContext)
        {

        }

        public async Task<List<StudentDashboard.Models.Entities.Student>> GetStudentAsync()
        {
            return await _studentDbContext.Student.AsNoTracking().ToListAsync();
        }


        public async Task<bool> AddStudent(StudentDashboard.Models.Entities.Student student)
        {
            _studentDbContext.Student.Add(student);
            return await SaveAsync();
        }
    }
}
