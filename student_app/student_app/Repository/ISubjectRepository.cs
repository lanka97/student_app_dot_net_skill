using student_app.Models;

namespace student_app.Repository
{
    public interface ISubjectRepository
    {
        IEnumerable<Subject> GetAllSubject(bool trackChanges);
        Subject GetSubject(int subId, bool trackChanges);
        void UpdateSubject(Subject student);
        void CreateSubject(Subject student);
    }
}
