using SchoolDemo.Models;
using SchoolDemo.Repositories;

namespace SchoolDemo.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repo;

        public CourseService(ICourseRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Course>> GetAllAsync() => _repo.GetAllAsync();

        public Task<Course?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public async Task CreateAsync(Course course)
        {
            await _repo.AddAsync(course);
            await _repo.SaveChangesAsync();
        }
    }
}
