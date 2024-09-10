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
    public class StudentService1 : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService1(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _unitOfWork.Students.GetAllAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _unitOfWork.Students.GetByIdAsync(id);
        }

        public async Task AddStudentAsync(Student student)
        {
            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            await _unitOfWork.Students.UpdateAsync(student);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            await _unitOfWork.Students.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
