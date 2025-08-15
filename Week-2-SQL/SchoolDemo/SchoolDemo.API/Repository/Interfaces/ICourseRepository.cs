using SchoolDemo.Models;

namespace SchoolDemo.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task AddAsync(Course course);
        Task SaveChangesAsync();
        
    }
}
