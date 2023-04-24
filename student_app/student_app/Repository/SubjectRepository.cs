using Microsoft.EntityFrameworkCore;
using student_app.Models;
using student_app.Repository.RepositoryBase;
using System.Linq.Expressions;

namespace student_app.Repository
{
    public class SubjectRepository : RepositoryBase<Subject>, ISubjectRepository
    {
        public SubjectRepository(APPDBContext ApplicationContext) : base(ApplicationContext)
        {
        }

        public void CreateSubject(Subject subject) => Create(subject);
        public void UpdateSubject(Subject subject) => Update(subject);
        public void DeleteSubject(Subject subject) => Delete(subject);

        public IEnumerable<Subject> GetAllSubject(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(c => c.SubjectName)
            .ToList();

        public Subject GetSubject(int subId, bool trackChanges)
        {
            return ApplicationContext.Set<Subject>().AsNoTracking()
            .Where(std => std.SubjectId == subId)
            .Include(std => std.EnrollStudents)
            .ThenInclude(sv => sv.Students)
            .FirstOrDefault();

            //var subject = FindAll(trackChanges)
            //.Where(sub => sub.SubjectId == subId)
            //.Include(std => std.EnrollStudents)
            //.FirstOrDefault();
            //return subject ?? new Subject();
        }
    }
}
