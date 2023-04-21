namespace student_app.Repository.RepositoryManager
{
    public interface IRepositoryManager
    {
        // IDepartmentRepository Department { get; }
        IStudentRepository Student { get; }
        void Save();
    }
}
