using Shool.Entity.Models;
using Shool.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shool.Repository
{
    public class AdminRepository:RepositoryBase<Admin>,IAdminRepository
    {
    }
}
