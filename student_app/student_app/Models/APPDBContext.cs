using Microsoft.EntityFrameworkCore;

namespace student_app.Models
{
    public class APPDBContext: DbContext
    {
        public APPDBContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }
}
