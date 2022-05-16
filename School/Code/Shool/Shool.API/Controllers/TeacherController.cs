using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shool.Entity.Dto;
using Shool.Entity.Models;
using Shool.IRepository;
using Shool.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shool.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TeacherController : Controller
    {
        private IMapper _mapper;
        private ITeacherRepository _teacherRepository;

        public TeacherController(IMapper mapper,ITeacherRepository teacherRepository)
        {
            _mapper = mapper;
            _teacherRepository = teacherRepository;
        }


        [HttpGet]
        //[Authorize(Roles ="admin")]    //角色认证
        [Authorize(Policy  = "teacher")] //策略认证
        public async Task<ApiResult> GetTeacherList() {

            var list = await _teacherRepository.QueryAsync();

            var listDto = _mapper.Map<List<TeacherDto>>(list);

            // ClaimTypes.Name自带的常量 系统定义好的 ；ClaimTypes类中都是常量 
            //var ss = ClaimTypes.Name;

            var name = User.FindFirst(d => d.Type == "Name").Value;

            return ApiResultHelper.Ok(listDto);
        } 

    }
}
