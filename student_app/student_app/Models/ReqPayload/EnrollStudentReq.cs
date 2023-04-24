namespace student_app.Models.ReqPayload
{
    public class EnrollStudentReq
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public bool AllowUnEnroll { get; set; }
    }
}
