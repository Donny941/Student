using StudentDashboard.Models.Entities;

namespace StudentDashboard.Services
{
    public class ServiceBase
    {
        protected readonly StudentDbContext _studentDbContext;

        protected ServiceBase(StudentDbContext studentDbContext)
        {
            _studentDbContext = studentDbContext;
        }

        protected async Task<bool> SaveAsync()
        {
            bool result = false;

            try
            {
                result = await _studentDbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return result;
        }

    }
}
