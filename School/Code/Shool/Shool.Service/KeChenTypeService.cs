using Shool.Entity.Models;
using Shool.IRepository;
using Shool.IService;

namespace Shool.Service
{
    public class KeChenTypeService : ServiceBase<KeChengType>, IKeChenTypeService
    {
        private readonly IKeChenTypeRepository _keChenTypeRepository;

        public KeChenTypeService(IKeChenTypeRepository keChenTypeRepository)
        {
            base._repositoryBase = keChenTypeRepository;
            _keChenTypeRepository = keChenTypeRepository;
        }
    }
}
