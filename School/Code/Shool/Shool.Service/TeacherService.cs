using Shool.Entity.Models;
using Shool.IRepository;
using Shool.IService;

namespace Shool.Service
{
    public class TeacherService : ServiceBase<Teacher>, ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            base._repositoryBase = teacherRepository;
            _teacherRepository = teacherRepository;
        }
    }
}
