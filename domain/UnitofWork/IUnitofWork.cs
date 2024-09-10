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

    public class UnitOfWork : IUnitOfWork
    {
        private readonly Db_context _context;
        private Repository<Student> _studentRepository;
        private Repository<Course> _courseRepository;
        private Repository<Enroll> _enrollRepository;
        private Repository<Level> _levelRepository;
        private Repository<Payment> _paymentRepository;
        public UnitOfWork(Db_context context)
        {
            _context = context;
        }

        public IRepository<Student> Students => _studentRepository ??= new Repository<Student>(_context);
        public IRepository<Course> Courses => _courseRepository ??= new Repository<Course>(_context);
   
        public IRepository<Enroll> Enrolls => _enrollRepository ??= new Repository<Enroll>(_context);

        public IRepository<Level> Levels => _levelRepository ??= new Repository<Level>(_context);

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();

        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
