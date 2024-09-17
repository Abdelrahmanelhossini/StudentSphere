using busnisslogic.interfaces;
using domain_and_repo.models;
using domain_and_repo.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _unitOfWork.Students.GetAllAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var studentId=_unitOfWork.Students.GetByIdAsync(id);
            if (studentId == null)
            {
                throw new Exception($"Student with ID {studentId} not found ");
            }

            return await _unitOfWork.Students.GetByIdAsync(id);
        }

        public async Task AddStudentAsync(Student student)
        {
            if (string.IsNullOrEmpty(student.Name))
            {
                throw new ArgumentNullException("student name cannot be empty ");
            }
            if (string.IsNullOrEmpty(student.Email) || !IsValidEmail(student.Email)) {
                throw new ArgumentException("Invalid email format");
            }
            var level = _unitOfWork.Levels.GetByIdAsync(student.Levelid);
            if (level == null) 
            {
                throw new ArgumentException("$\"Level with ID {student.Levelid} does not exist.");
            }


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
