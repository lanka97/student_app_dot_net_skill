using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace student_app.Models
{
    public class APPDBContext: DbContext
    {
        public APPDBContext(DbContextOptions options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Student>()
            //    .HasMany(e => e.Subjects)
            //    .WithMany(e => e.Students)
            //    .UsingEntity<EnrollStudent>(
            //        j => j.Property(e => e.EnrolledOn).HasDefaultValueSql("CURRENT_TIMESTAMP"));
            modelBuilder.Entity<EnrollStudent>()
                    .HasKey(e => new { e.StudentId, e.SubjectId });

            modelBuilder.Entity<EnrollStudent>(
                    j => j.Property(e => e.EnrolledOn).HasDefaultValueSql("CURRENT_TIMESTAMP"));
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<EnrollStudent> EnrollStudents { get; set; }
    }
}
