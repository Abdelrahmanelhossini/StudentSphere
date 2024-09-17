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
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Course>> GetAllStudentsAsync()
        {
            return await _unitOfWork.Courses.GetAllAsync();
        }

        public async Task<Course> GetStudentByIdAsync(int id)
        {
            return await _unitOfWork.Courses.GetByIdAsync(id);
        }

        public async Task AddStudentAsync(Course courses)
        {
            if (string.IsNullOrEmpty(courses.CourseName))
            {
                throw new ArgumentNullException("course name cannot be empty ");
            }
            await _unitOfWork.Courses.AddAsync(courses);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateStudentAsync(Course courses)
        {
            await _unitOfWork.Courses.UpdateAsync(courses);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            await _unitOfWork.Courses.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}

