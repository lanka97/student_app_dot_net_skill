namespace student_app.Models.Utill
{
    public class AppResponse
    {
        public int StatusCode { get; set; }
        public string? Msg { get; set; }
        public dynamic? Data { get; set; }
    }
}
