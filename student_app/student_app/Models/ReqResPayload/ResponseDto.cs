namespace student_app.Models.ReqPayload
{
    public class ResponseDto
    {
        public string Message { get; set; } = "";
        public int Status { get; set; } = 0;
        public dynamic data { get; set; }
    }
}
