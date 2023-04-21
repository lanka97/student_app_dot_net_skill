using student_app.Models;

namespace student_app.Repository
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAllStudents(bool trackChanges);
        Student GetStudent(int stdID, bool trackChanges);
        void CreateStudent(Student student);
    }
}
