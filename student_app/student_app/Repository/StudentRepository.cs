using Microsoft.EntityFrameworkCore;
using student_app.Models;
using student_app.Repository.RepositoryBase;

namespace student_app.Repository
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(APPDBContext ApplicationContext) : base(ApplicationContext)
        {
        }

        public IEnumerable<Student> GetAllStudents(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(c => c.StudentName)
            .ToList();

        public void CreateStudent(Student student) => Create(student);

        public Student GetStudent(int stdId, bool trackChanges) =>
            ApplicationContext.Set<Student>().AsNoTracking()
            .Where(std => std.StudentId == stdId)
            .Include(std => std.EnrollStudents)
            .ThenInclude(sv => sv.Subjects)
            .FirstOrDefault();

        public void DeleteStudent(Student student)
        {
            Delete(student);
        }

        public void UpdateSubject(Student student)
        {
            Update(student);
        }
    }
}
