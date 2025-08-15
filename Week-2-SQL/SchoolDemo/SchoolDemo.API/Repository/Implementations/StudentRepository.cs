using Microsoft.EntityFrameworkCore;
using SchoolDemo.Data;
using SchoolDemo.Models;

namespace SchoolDemo.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        //Now that we are using EF Core, our repos have a dependency.
        //We are going to ask for a DbContext object to run our EF methods from.
        private readonly SchoolDbContext _context;

        public StudentRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Student student)
        {
            //We add a student, that is passed in from the service layer.
            //The service got this student from the body of the request sent in to the endpoint method.
            //Our student has traveled Endpoint(Program.cs) -> StudentService -> StudentRepo
            await _context.Students.AddAsync(student);

            //Then we save the changes
            await _context.SaveChangesAsync();
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
