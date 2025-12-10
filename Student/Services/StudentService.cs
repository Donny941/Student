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
            return await _studentDbContext.Student.
                Where(s => s.IsDeleted == false).
                AsNoTracking().ToListAsync();
        }


        public async Task<bool> AddStudent(StudentDashboard.Models.Entities.Student student)
        {
            _studentDbContext.Student.Add(student);
            return await SaveAsync();
        }


        public async Task<StudentDashboard.Models.Entities.Student> GetStudentById(Guid Id)
        {
            return await _studentDbContext.Student.FindAsync(Id);
        }

        public async Task<bool> UpdateStudentAsync(StudentDashboard.Models.Entities.Student student)
        {
            _studentDbContext.Student.Update(student);
            return await SaveAsync();
        }


        public async Task<bool> DeleteStudentAsync(Guid Id)
        {
            var studentToDelete = await _studentDbContext.Student.FindAsync(Id);

            if (studentToDelete is null)
            {
                return true;
            }


            studentToDelete.IsDeleted = true;

            return await SaveAsync();


        }
    }
}
