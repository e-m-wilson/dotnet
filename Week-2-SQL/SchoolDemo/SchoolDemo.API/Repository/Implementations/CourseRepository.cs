using Microsoft.EntityFrameworkCore;
using SchoolDemo.Data;
using SchoolDemo.Models;

namespace SchoolDemo.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        public Task AddAsync(Course course)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Course>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Course?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
