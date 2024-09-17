using busnisslogic.interfaces;
using domain_and_repo.models;
using domain_and_repo.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace busnisslogic.content
{
    public class EnrollService :IEnrollServece
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnrollService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Enroll>> GetAllStudentsAsync()
        {
            return await _unitOfWork.Enrolls.GetAllAsync();
        }

        public async Task<Enroll> GetStudentByIdAsync(int id)
        {
            return await _unitOfWork.Enrolls.GetByIdAsync(id);
        }

        public async Task AddStudentAsync(Enroll enrolls)
        {
            if (enrolls.degree < 0 )
            {
                throw new ArgumentException("degree cannot be negative.");
            }
            if(enrolls.degree > 100)
            {
                throw new ArgumentException("degree cannot be Greater than 100.");
            }
            await _unitOfWork.Enrolls.AddAsync(enrolls);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateStudentAsync(Enroll enrolls)
        {
            await _unitOfWork.Enrolls.UpdateAsync(enrolls);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            await _unitOfWork.Enrolls.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
    

