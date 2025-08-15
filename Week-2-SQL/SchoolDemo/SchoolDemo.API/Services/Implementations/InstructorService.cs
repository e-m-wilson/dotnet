using SchoolDemo.Models;
using SchoolDemo.Repositories;

namespace SchoolDemo.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _repo;

        public InstructorService(IInstructorRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Instructor>> GetAllAsync() => _repo.GetAllAsync();

        public Task<Instructor?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public async Task CreateAsync(Instructor instructor)
        {
            await _repo.AddAsync(instructor);
            await _repo.SaveChangesAsync();
        }
    }
}
