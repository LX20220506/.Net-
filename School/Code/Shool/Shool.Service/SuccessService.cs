using Shool.Entity.Models;
using Shool.IRepository;
using Shool.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shool.Service
{
    public class SuccessService : ServiceBase<Success>, ISuccessService
    {
        private readonly ISuccessRepository _successRepository;

        public SuccessService(ISuccessRepository successRepository)
        {
            base._repositoryBase = successRepository;
            _successRepository = successRepository;
        }
    }
}
