namespace student_app.Repository.RepositoryManager
{
    public interface IRepositoryManager
    {
        // IDepartmentRepository Department { get; }
        IStudentRepository Student { get; }
        ISubjectRepository Subject { get; }
        IEnrollRepository Enroll { get; }
        Task<int> Save();
    }
}
