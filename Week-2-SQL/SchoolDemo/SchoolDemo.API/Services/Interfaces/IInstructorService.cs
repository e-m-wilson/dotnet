using SchoolDemo.Models;

namespace SchoolDemo.Services
{
    public interface IInstructorService
    {
        Task<IEnumerable<Instructor>> GetAllAsync();
        Task<Instructor?> GetByIdAsync(int id);
        Task CreateAsync(Instructor instructor);
    }
}
