using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace student_app.Models.ViewModel
{
    public class SubjectDto
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = "";
        public int? Credits { get; set; }
        public virtual ICollection<EnrollStudentWithoutSubDto>? EnrollStudents { get; set; }

    }
}
