using domain_and_repo.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace busnisslogic.interfaces
{
    public interface ILevelServce
    {
        Task<IEnumerable<Level>> GetAllStudentsAsync();
        Task<Level> GetStudentByIdAsync(int id);
        Task AddStudentAsync(Level course);
        Task UpdateStudentAsync(Level course);
        Task DeleteStudentAsync(int id);
    }
}
