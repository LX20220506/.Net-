using Shool.Entity.Models;
using Shool.IRepository;
using Shool.IService;

namespace Shool.Service
{
    public class StudentService : ServiceBase<Student>, IStudentService
    {
        public StudentService(IStudentRepository strudentRepository)
        {
        }
    }
}
