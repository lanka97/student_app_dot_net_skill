﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using student_app.Models;

namespace student_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly APPDBContext _context;

        public SubjectsController(APPDBContext context)
        {
            _context = context;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
        {
          if (_context.Subjects == null)
          {
              return NotFound();
          }
            return await _context.Subjects.ToListAsync();
        }

        // GET: api/Subjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> GetSubject(int id)
        {
          if (_context.Subjects == null)
          {
              return NotFound();
          }
            var subject  = await _context.Subjects.Where(std => std.SubjectId == id)
                          .Include(std => std.Students)
                          .FirstOrDefaultAsync();

            if (subject == null)
            {
                return NotFound();
            }

            return subject;
        }

        // PUT: api/Subjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(int id, Subject subject)
        {

            if (!SubjectExists(id))
            {
                return NotFound();
            }
            else
            {
                var existSubject = await _context.Subjects.FindAsync(id);
                existSubject.SubjectName = (subject.SubjectName == existSubject.SubjectName) ? existSubject.SubjectName: subject.SubjectName;
                existSubject.Credits = (subject.Credits == existSubject.Credits) ? existSubject.Credits: subject.Credits;
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {  
                    throw; 
            }

            return Ok();
        }

        // PUT: api/Subjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("EnrollStudent")]
        public async Task<IActionResult> EnrollStudent(EnrollStudent entollPayload)
        {
            var subject = new Subject();
            var student = new Student();

            // check Subject Exist
            if (entollPayload?.SubjectId != null && entollPayload?.StudentId != null)
            {
                subject = await _context.Subjects.Where(std => std.SubjectId == entollPayload.SubjectId)
                          .Include(std => std.Students)
                          .FirstOrDefaultAsync();
                student = await _context.Students.FindAsync(entollPayload.StudentId);

                if (subject == null || student == null)
                {
                    return BadRequest();
                }
                else {
                    var studentExists = false;
                    if (subject.Students != null)
                    {
                        var subjectStudent = subject?.Students.Where(std => std.StudentId == entollPayload.StudentId).FirstOrDefault();
                        studentExists = subjectStudent != null;
                    }
                    else {
                        subject.Students = new List<Student>();
                    }

                    if (studentExists) {
                        subject?.Students.Remove(student);
                    } else {
                        subject?.Students.Add(student);
                    }

                }
            }

            try
            {
                await _context.SaveChangesAsync();
                return Accepted();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
        }

        // POST: api/Subjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Subject>> PostSubject(Subject subject)
        {
          if (_context.Subjects == null)
          {
              return Problem("Entity set 'APPDBContext.Subjects'  is null.");
          }
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubject", new { id = subject.SubjectId }, subject);
        }

        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            if (_context.Subjects == null)
            {
                return NotFound();
            }
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubjectExists(int id)
        {
            return (_context.Subjects?.Any(e => e.SubjectId == id)).GetValueOrDefault();
        }
    }
}
