using student_app.Models;

namespace student_app.Repository
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAllStudents(bool trackChanges);
        void CreateStudent(Student student);
    }
}
