namespace student_app.Models.ViewModel
{
    public class EnrollStudentWithoutSubDto
    {
        public int StudentId { get; set; }
        //public int SubjectId { get; set; }
        public DateTime EnrolledOn { get; set; }
        public ShortStudentDto Students { get; set; }
    }
}
