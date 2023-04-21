using Microsoft.EntityFrameworkCore;
using student_app.Models;
using student_app.Repository.RepositoryBase;
using System.Linq.Expressions;

namespace student_app.Repository
{
    public class SubjectRepository : RepositoryBase<Subject>, ISubjectRepository
    {
        public SubjectRepository(APPDBContext _ApplicationContext) : base(_ApplicationContext)
        {
        }

        public void CreateSubject(Subject subject) => Create(subject);
        public void UpdateSubject(Subject subject) => Update(subject);

        public IEnumerable<Subject> GetAllSubject(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(c => c.SubjectName)
            .ToList();

        public Subject GetSubject(int subId, bool trackChanges)
        {
            var subject = FindAll(trackChanges)
            .Where(sub => sub.SubjectId == subId)
            .Include(std => std.Students)
            .FirstOrDefault();
            return subject ?? new Subject();
        }
    }
}
