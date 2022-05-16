using Microsoft.AspNetCore.Mvc;
using Shool.Entity.Models;
using Shool.IRepository;
using Shool.IService;
using Shool.Service;
using Shool.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shool.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class KeChenTypeController : Controller
    {
        private IKeChenTypeRepository _keChenTypeRepository;
        private readonly IAOPDemoService _test;

        public KeChenTypeController(IKeChenTypeRepository keChenTypeRepository,IAOPDemoService test)
        {
            _keChenTypeRepository = keChenTypeRepository;
            _test = test;
        } 

        [HttpGet]
        public async Task<ApiResult> GetKeChenTypeInfo() {
            List<KeChengType> list = await _keChenTypeRepository.QueryAsync();


            return ApiResultHelper.Ok(list);
        }


        [HttpGet]
        public  string Test() {
            return _test.Test();
        }
    }
}
