using AutoMapper;
using Shool.Entity.Dto;
using Shool.Entity.Models;

namespace Shool.Utility
{
    public class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {
            base.CreateMap<Admin, AdminDto>();
            base.CreateMap<Student, StudentDto>();
            base.CreateMap<Teacher, TeacherDto>();

            base.CreateMap<StudentDto, Student>();
            base.CreateMap<TeacherDto, Teacher>();
        }
    }
}
