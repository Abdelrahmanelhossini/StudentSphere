using domain_and_repo.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace domain_and_repo
{
    public class Db_context : IdentityDbContext<IdentityUser>
    {

        public Db_context(DbContextOptions<Db_context> options)
            : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<Enroll> enroll { get; set; }
        public DbSet<Level> levels { get; set; }
        public DbSet<Payment> payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship between Student and Level
            modelBuilder.Entity<Student>()
                .HasOne(s => s.level)
                .WithMany(l => l.Students)
                .HasForeignKey(s => s.Levelid)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}



          