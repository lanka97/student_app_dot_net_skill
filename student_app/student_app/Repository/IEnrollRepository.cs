using student_app.Models;

namespace student_app.Repository
{
    public interface IEnrollRepository
    {
        void EnrollStudent(EnrollStudent enrollStudent);
        void UnEnrollStudent(EnrollStudent enrollStudent);
        public EnrollStudent GetEnrollStudent(int stdID, int subID, bool trackChanges);
    }
}
