using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace student_app.Models.ReqPayload
{
    public class EnrollSubjectReq
    {
        public string SubjectName { get; set; } = "";
        public int Credits { get; set; } = 0;
    }
}
