using SchoolDemo.Models;

namespace SchoolDemo.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task CreateAsync(Course course);
    }
}
