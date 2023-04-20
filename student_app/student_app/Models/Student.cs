using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace student_app.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string StudentName { get; set; } = "";
        public string? StudentEmail { get; set; }
        public virtual ICollection<Subject>? Subjects { get; set; }
    }
}
