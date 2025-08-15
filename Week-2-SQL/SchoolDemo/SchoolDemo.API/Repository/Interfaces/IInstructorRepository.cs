using SchoolDemo.Models;

namespace SchoolDemo.Repositories
{
    public interface IInstructorRepository
    {
        Task<IEnumerable<Instructor>> GetAllAsync();
        Task<Instructor?> GetByIdAsync(int id);
        Task AddAsync(Instructor instructor);
        Task SaveChangesAsync();
    }
}
