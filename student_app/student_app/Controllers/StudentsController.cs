
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

            var response = new ResponseDto();
            
            var student = _repository.Student.GetStudent(id, true);

            if (student == null)
            {
                response.Message = "Invalid StudentId";
                response.Status = 404;
                return NotFound(response);
            }

            var studentView = _mapper.Map<StudentDto>(student); 


            return studentView;
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(PostStudentReq studentReq)
        {
            var response = new ResponseDto();
            if (_repository.Student == null)
            {
                response.Message = "Entity set 'APPDBContext.Students'  is null.";
                response.Status = 500;
                return BadRequest(response);
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
            var response = new ResponseDto();
            
            var student = _repository.Student.GetStudent(id,true);
            if (student == null)
            {
                response.Message = "Invalid StudentId";
                response.Status = 404;
                return NotFound(response);
            }

            _repository.Student.DeleteStudent(student);
            try
            {
                await _repository.Save(); 
                response.Message = "Student Deleted Succesfully";
                response.Status = 200;
                return NotFound(response);
            }
            catch (DbUpdateConcurrencyException)
            {
                response.Message = "Somthing went wrong.";
                response.Status = 500;
                return BadRequest(response);
            }

        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, PostStudentReq student)
        {
            var response = new ResponseDto();
            var studentObj = _repository.Student.GetStudent(id,true);

            if (studentObj == null)
            {
                response.Message = "Invalid StudentId";
                response.Status = 404;
                return NotFound(response);
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
                response.Message = "Student Update Successfully";
                response.Status = 200;
                return Accepted(response);
            }
            catch (DbUpdateConcurrencyException)
            {
                response.Message = "Somthing went wrong.";
                response.Status = 500;
                return BadRequest(response);
            }
        }

        //private bool StudentExists(int id)
        //{
        //    return (_context.Students?.Any(e => e.StudentId == id)).GetValueOrDefault();
        //}
    }
}
