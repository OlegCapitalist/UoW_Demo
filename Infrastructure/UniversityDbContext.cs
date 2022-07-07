using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class UniversityDbContext: DbContext
    {
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;

        private readonly string _connectionString = String.Empty;

        public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
            : base(options)
        {   }

        public UniversityDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {   
            if (_connectionString != string.Empty)
                optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CoursesMapping(modelBuilder);
            GroupsMapping(modelBuilder);
            StudentsMapping(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void CoursesMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("COURSES");

            modelBuilder.Entity<Course>().Property(x => x.Id).HasColumnName("COURSE_ID");
            modelBuilder.Entity<Course>().Property(x => x.Name).HasColumnName("NAME");
            modelBuilder.Entity<Course>().Property(x => x.Description).HasColumnName("DESCRIPTION");
        }

        private void GroupsMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().ToTable("GROUPS");

            modelBuilder.Entity<Group>().Property(x => x.Id).HasColumnName("GROUP_ID");
            modelBuilder.Entity<Group>().Property(x => x.Name).HasColumnName("NAME");
            modelBuilder.Entity<Group>().Property(x => x.CourseId).HasColumnName("COURSE_ID");
        }

        private void StudentsMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("STUDENTS");

            modelBuilder.Entity<Student>().Property(x => x.Id).HasColumnName("STUDENT_ID");
            modelBuilder.Entity<Student>().Property(x => x.FirstName).HasColumnName("FIRST_NAME");
            modelBuilder.Entity<Student>().Property(x => x.LastName).HasColumnName("LAST_NAME");
            modelBuilder.Entity<Student>().Property(x => x.GroupId).HasColumnName("GROUP_ID");
        }

    }
}
