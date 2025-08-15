using SchoolDemo.Models;

namespace SchoolDemo.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task CreateAsync(Student student);
    }
}
