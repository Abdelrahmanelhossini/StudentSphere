using domain_and_repo.models;
using domain_and_repo.Repositorys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain_and_repo.UnitofWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Student> Students { get; }
        IRepository<Course> Courses { get; }
        IRepository<Enroll> Enrolls { get; }
        IRepository<Level> Levels { get; }
        Task<int> SaveAsync();
    }  
}
