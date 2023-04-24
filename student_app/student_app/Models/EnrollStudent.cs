namespace student_app.Models
{
    public class EnrollStudent
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public DateTime EnrolledOn { get; set; }
        public virtual Subject Subjects { get; set; }
        public virtual Student Students { get; set; }
    }
}
