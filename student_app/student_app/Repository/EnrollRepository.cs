using student_app.Models;
using student_app.Repository.RepositoryBase;

namespace student_app.Repository
{
    public class EnrollRepository : RepositoryBase<EnrollStudent>, IEnrollRepository
    {
        public EnrollRepository(APPDBContext _ApplicationContext) : base(_ApplicationContext)
        {
        }

        public EnrollStudent GetEnrollStudent(int stdID, int subID, bool trackChanges) {
           return  FindAll(trackChanges)
            .Where(std => std.StudentId == stdID && std.SubjectId == subID )
            .FirstOrDefault() ?? new EnrollStudent();
        }
        public void EnrollStudent(EnrollStudent enrollStudent) => Create(enrollStudent);
        public void UnEnrollStudent(EnrollStudent enrollStudent) => Delete(enrollStudent);
    }
}
