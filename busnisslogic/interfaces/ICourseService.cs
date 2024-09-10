using domain_and_repo.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace busnisslogic.interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllStudentsAsync();
        Task<Course> GetStudentByIdAsync(int id);
        Task AddStudentAsync(Course course);
        Task UpdateStudentAsync(Course course);
        Task DeleteStudentAsync(int id);
    }
}
