using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace student_app.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string SubjectName { get; set; } = "";
        public int Credits { get; set; } = 0;

        public virtual ICollection<EnrollStudent>? EnrollStudents { get; set; }
    }
}
