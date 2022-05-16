using Shool.Entity.Models;
using Shool.IRepository;
using Shool.IService;

namespace Shool.Service
{
    public class DaiKeService : ServiceBase<DaiKe>, IDaiKeService
    {
        private readonly IDaiKeRepository _daiKeRepository;

        public DaiKeService(IDaiKeRepository daiKeRepository)
        {
            base._repositoryBase = daiKeRepository;
            _daiKeRepository = daiKeRepository;
        }
    }
}
