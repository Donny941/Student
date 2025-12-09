using Microsoft.EntityFrameworkCore;

namespace StudentDashboard.Models.Entities
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Student { get; set; }

    }
}
