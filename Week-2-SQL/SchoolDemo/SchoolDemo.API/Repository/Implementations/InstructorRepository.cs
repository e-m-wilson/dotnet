
using Microsoft.EntityFrameworkCore;
using SchoolDemo.Data;
using SchoolDemo.Models;

namespace SchoolDemo.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        public Task AddAsync(Instructor instructor)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Instructor>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Instructor?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
