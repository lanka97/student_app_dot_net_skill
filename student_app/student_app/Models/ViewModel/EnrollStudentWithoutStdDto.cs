namespace student_app.Models.ViewModel
{
    public class EnrollStudentWithoutStdDto
    {
        //public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public DateTime EnrolledOn { get; set; }
        public ShortSubjectDto Subjects { get; set; }
    }
}
