
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using student_app.Models;
using student_app.Models.ReqPayload;
using student_app.Models.ViewModel;
using student_app.Repository.RepositoryManager;

namespace student_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public StudentsController( IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Students
        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _repository.Student.GetAllStudents(trackChanges: false);
            var studentsView = _mapper.Map<List<ShortStudentDto>>(students);
            return Ok(studentsView);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudent(int id)
        {
          if (_repository.Student == null)
          {
              return NotFound();
          }
            var student = _repository.Student.GetStudent(id, true);

            if (student == null)
            {
                return NotFound();
            }

            var studentView = _mapper.Map<StudentDto>(student); 

            Console.WriteLine(student);

            return studentView;
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(PostStudentReq studentReq)
        {
            if (_repository.Student == null)
            {
                return Problem("Entity set 'APPDBContext.Students'  is null.");
            }

            Student student = new Student();
            student.StudentName = studentReq.StudentName;
            student.StudentEmail = studentReq.StudentEmail;

            _repository.Student.CreateStudent(student);
            await _repository.Save();

            return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (_repository.Student == null)
            {
                return NotFound();
            }
            var student = _repository.Student.GetStudent(id,true);
            if (student == null)
            {
                return NotFound();
            }

            _repository.Student.DeleteStudent(student);
            try
            {
                await _repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, PostStudentReq student)
        {
            var studentObj = _repository.Student.GetStudent(id,true);

            if (studentObj == null)
            {
                return NotFound();
            }
            else
            {
                studentObj.StudentName = student.StudentName;
                studentObj.StudentEmail = student.StudentEmail;

                _repository.Student.UpdateSubject(studentObj);
            }

            try
            {
                await _repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                    throw;
            }

            return NoContent();
        }

        //private bool StudentExists(int id)
        //{
        //    return (_context.Students?.Any(e => e.StudentId == id)).GetValueOrDefault();
        //}
    }
}
