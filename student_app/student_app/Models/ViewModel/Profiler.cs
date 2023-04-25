using AutoMapper;

namespace student_app.Models.ViewModel
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.EnrollSubjects, opt => opt.MapFrom(src => src.EnrollStudents))
                .ReverseMap()
                .ForMember(dest => dest.EnrollStudents, opt => opt.MapFrom(src => src.EnrollSubjects));

            //CreateMap<EnrollStudent, EnrollStudentWithoutStdDto>()
            //    .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
            //    .ForMember(dest => dest.SubjectId, opt => opt.MapFrom(src => src.SubjectId))
            //    .ForMember(dest => dest.EnrolledOn, opt => opt.MapFrom(src => src.EnrolledOn));
            CreateMap<EnrollStudent, EnrollStudentWithoutStdDto>();

            CreateMap<Student, ShortStudentDto>();
        }
    }

    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<Subject, SubjectDto>()
                .ForMember(dest => dest.EnrollStudents, opt => opt.MapFrom(src => src.EnrollStudents))
                .ReverseMap()
                .ForMember(dest => dest.EnrollStudents, opt => opt.MapFrom(src => src.EnrollStudents));

            CreateMap<EnrollStudent, EnrollStudentWithoutSubDto>();

            CreateMap<Subject, ShortSubjectDto>();
        }
    }
}
