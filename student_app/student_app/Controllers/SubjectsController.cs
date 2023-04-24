using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    public class SubjectsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public SubjectsController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<ActionResult<List<ShortSubjectDto>>> GetSubjects()
        {
            var subject = _repository.Subject.GetAllSubject(trackChanges: false);
            var subjectModalView = _mapper.Map<List<ShortSubjectDto>>(subject);
            return Ok(subjectModalView);
        }

        // POST: api/Subjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Subject>> PostSubject(EnrollSubjectReq subjectReq)
        {
          if (_repository.Subject == null)
          {
              return Problem("Entity set 'APPDBContext.Subjects'  is null.");
          }
            try {
                Subject subject = new Subject();
                subject.SubjectName = subjectReq.SubjectName;
                subject.Credits = subjectReq.Credits;

                _repository.Subject.CreateSubject(subject);
                await _repository.Save();
                return CreatedAtAction(nameof(PostSubject), new { id = subject.SubjectId }, subject);
            }
            catch (Exception ex){
                return BadRequest();
            }
        }

        //GET: api/Subjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectDto>> GetSubject(int id)
        {
            if (_repository.Subject == null)
            {
                return NotFound();
            }
            var subject = _repository.Subject.GetSubject(id, true);

            if (subject == null)
            {
                return NotFound();
            }

            var subjectViewModel = _mapper.Map<SubjectDto>(subject);
            return subjectViewModel;
        }

        // DELETE: api/Subject/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            if (_repository.Subject == null)
            {
                return NotFound();
            }
            var subject = _repository.Subject.GetSubject(id, true);
            if (subject == null)
            {
                return NotFound();
            }

            _repository.Subject.DeleteSubject(subject);
            try {
                await _repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }

        // PUT: api/Subject/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, EnrollSubjectReq subject)
        {
            var subjectObj = _repository.Subject.GetSubject(id, true);

            if (subjectObj == null)
            {
                return NotFound();
            }
            else
            {
                subjectObj.SubjectName = subject.SubjectName;
                subjectObj.Credits = subject.Credits;

                _repository.Subject.UpdateSubject(subjectObj);
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

        // PUT: api/Subjects/EnrollStudent
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("EnrollStudent")]
        public async Task<IActionResult> EnrollStudent(EnrollStudentReq entollPayload)
        {
            // check Subject Exist
            if (entollPayload?.SubjectId != null && entollPayload?.StudentId != null)
            {
                var subject = _repository.Subject.GetSubject(entollPayload.SubjectId, false);
                var student = _repository.Student.GetStudent(entollPayload.StudentId, false);

                if (subject == null || student == null)
                {
                    return BadRequest();
                }
                else
                {
                    var isStudentExists = false;
                    EnrollStudent enrollStd = new EnrollStudent();

                    if (subject.EnrollStudents?.Count > 0)
                    {
                        var subjectStudent = subject?.EnrollStudents.Where(std => std.StudentId == entollPayload.StudentId).FirstOrDefault();
                        if (subjectStudent != null) {
                            isStudentExists = true;
                        }
                    }

                    if (!isStudentExists)
                    {
                        enrollStd.SubjectId = entollPayload.SubjectId;
                        enrollStd.StudentId = entollPayload.StudentId;
                        _repository.Enroll.EnrollStudent(enrollStd);
                    }
                    else if(entollPayload.AllowUnEnroll) {
                        enrollStd = _repository.Enroll.GetEnrollStudent(entollPayload.StudentId, entollPayload.SubjectId, false);
                        if (enrollStd != null)
                        {
                            _repository.Enroll.UnEnrollStudent(enrollStd);

                        }
                    }
                }
            }

            try
            {
                await _repository.Save();
                return Accepted();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
        }
    }
}
