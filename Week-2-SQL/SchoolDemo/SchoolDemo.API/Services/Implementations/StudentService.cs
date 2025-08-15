using SchoolDemo.Models;
using SchoolDemo.Repositories;

namespace SchoolDemo.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }

        public Task<List<Student>> GetAllAsync() => _repo.GetAllAsync();

        public Task<Student?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public async Task CreateAsync(Student student)
        {
            await _repo.AddAsync(student);
        }
    }
}
