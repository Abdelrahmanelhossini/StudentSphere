using domain_and_repo.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace busnisslogic.interfaces
{
    public interface  IEnrollServece
    {
        Task<IEnumerable<Enroll>> GetAllStudentsAsync();
        Task<Enroll> GetStudentByIdAsync(int id);
        Task AddStudentAsync(Enroll enroll);
        Task UpdateStudentAsync(Enroll enroll);
        Task DeleteStudentAsync(int id);
    }
}
