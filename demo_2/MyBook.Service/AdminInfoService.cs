using System;
using System.Collections.Generic;
using System.Text;
using Models;
using MyBook.IRepository;
using MyBook.IService;


namespace MyBook.Service
{
    public class AdminInfoService:BaseService<Admin>, IAdminInfoService
    {
        private readonly IAdminInfoRepository _iAdminInfoRepository;
        public AdminInfoService(IAdminInfoRepository iAdminInfoRepository) {
            base._iBaseRepository = iAdminInfoRepository;
            this._iAdminInfoRepository = iAdminInfoRepository;
        }
    }
}
