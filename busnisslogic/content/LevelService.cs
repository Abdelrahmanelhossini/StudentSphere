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
    public class LevelService :ILevelServce
    {
        private readonly IUnitOfWork _unitOfWork;

        public LevelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Level>> GetAllStudentsAsync()
        {
            return await _unitOfWork.Levels.GetAllAsync();
        }

        public async Task<Level> GetStudentByIdAsync(int id)
        {
            return await _unitOfWork.Levels.GetByIdAsync(id);
        }

        public async Task AddStudentAsync(Level level)
        {
            await _unitOfWork.Levels.AddAsync(level);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateStudentAsync(Level level)
        {
            await _unitOfWork.Levels.UpdateAsync(level);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            await _unitOfWork.Levels.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
