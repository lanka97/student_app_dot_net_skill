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
    }
}
