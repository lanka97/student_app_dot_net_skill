using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using student_app.Models;
using student_app.Repository.RepositoryManager;

namespace student_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;

        public SubjectsController(IRepositoryManager repository)
        {
            _repository = repository;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
        {
            var subject = _repository.Subject.GetAllSubject(trackChanges: false);
            return Ok(subject);
        }

        // POST: api/Subjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Subject>> PostSubject(Subject subject)
        {
          if (_repository.Subject == null)
          {
              return Problem("Entity set 'APPDBContext.Subjects'  is null.");
          }
            try {
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
        public async Task<ActionResult<Subject>> GetSubject(int id)
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

            return subject;
        }

        // PUT: api/Subjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutSubject(int id, Subject subject)
        //{
        //    if (id != subject.SubjectId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(subject).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SubjectExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // PUT: api/Subjects/EnrollStudent
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("EnrollStudent")]
        public async Task<IActionResult> EnrollStudent(EnrollStudent entollPayload)
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
                    if (subject.Students?.Count > 0)
                    {
                        var subjectStudent = subject?.Students.Where(std => std.StudentId == entollPayload.StudentId).FirstOrDefault();
                        if (subjectStudent != null) {
                            isStudentExists = true;
                        }
                    }
                    else
                    {
                        subject.Students = new List<Student>();
                    }

                    if (!isStudentExists)
                    {
                        _repository.Subject.GetSubject(entollPayload.SubjectId, false)?.Students?.Add(student);
                    }
                    else {
                        subject?.Students.Remove(student);
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



        // DELETE: api/Subjects/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteSubject(int id)
        //{
        //    if (_context.Subjects == null)
        //    {
        //        return NotFound();
        //    }
        //    var subject = await _context.Subjects.FindAsync(id);
        //    if (subject == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Subjects.Remove(subject);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool subjectexists(int id)
        //{
        //    return (_context.subjects?.any(e => e.subjectid == id)).getvalueordefault();
        //}
    }
}
