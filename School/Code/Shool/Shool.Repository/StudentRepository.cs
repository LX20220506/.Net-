using Shool.Entity.Models;
using Shool.IRepository;

namespace Shool.Repository
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
    }
}
