using Models;
using System;
using System.Collections.Generic;
using System.Text;
using MyBook.IRepository;

namespace MyBook.Repository
{
    public class AdminInfoRepository:BaseRepository<Admin>,IAdminInfoRepository
    {
    }
}
