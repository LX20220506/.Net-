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
    public class AdminService : ServiceBase<Admin>, IAdminServic
    {
        private readonly IAdminRepository _adminRespository;

        public AdminService(IAdminRepository adminRespository)
        {
            base._repositoryBase = adminRespository;
            _adminRespository = adminRespository;
        }
    }
}
