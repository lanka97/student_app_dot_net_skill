using student_app.Models;

namespace student_app.Repository.RepositoryManager
{
    public class RepositoryManager : IRepositoryManager
    {
        private APPDBContext _applicationContext;
        private ISubjectRepository _subjectRepository;
        private IStudentRepository? _studentRepository;
        private IEnrollRepository? _enrollRepository;

        public RepositoryManager(APPDBContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public IStudentRepository Student
        {
            get
            {
                if (_studentRepository == null)
                    _studentRepository = new StudentRepository(_applicationContext);
                return _studentRepository;
            }
        }

        public ISubjectRepository Subject
        {
            get
            {
                if (_subjectRepository == null)
                    _subjectRepository = new SubjectRepository(_applicationContext);
                return _subjectRepository;
            }
        }

        public IEnrollRepository Enroll
        {
            get
            {
                if (_enrollRepository == null)
                    _enrollRepository = new EnrollRepository(_applicationContext);
                return _enrollRepository;
            }
        }

        public async Task<int> Save() { 
            return await _applicationContext.SaveChangesAsync(); 
        }
    }
}
