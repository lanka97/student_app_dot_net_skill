namespace student_app.Repository.RepositoryManager
{
    public interface IRepositoryManager
    {
        // IDepartmentRepository Department { get; }
        IStudentRepository Student { get; } 
        ISubjectRepository Subject { get; }
        Task<int> Save();
    }
}
