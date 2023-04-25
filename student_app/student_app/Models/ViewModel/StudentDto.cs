using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace student_app.Models.ViewModel
{
    public class StudentDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = "";
        public string? StudentEmail { get; set; }
        public virtual ICollection<EnrollStudentWithoutStdDto>? EnrollSubjects { get; set; }
    }
}
