using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shool.IRepository;
using Shool.Utility;
using Shool.Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Shool.Entity.Dto;
using AutoMapper;

namespace Shool.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentController(IStudentRepository studentRepository,IMapper mapper) {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }


        //添加学生
        [HttpPost]
        //[Authorize(Policy ="teacher")]
        public async Task<ApiResult> AddStudent([FromBody]StudentDto student) {

            var newstudent = _mapper.Map<Student>(student);
            newstudent.StudentPwd = MD5Helper.MD5Encrypt64("123456");

            if (await _studentRepository.AddAsync(newstudent))
            {
                return ApiResultHelper.Ok("添加成功");
            }

            return ApiResultHelper.Err("添加失败");
        }

        //删除学生
        [HttpDelete]
        //[Authorize(Policy ="teacher")]
        public async Task<ApiResult> DeleteStudent(string studentNo) {
            if (await _studentRepository.RemoveAsync(s=>s.StudentNo== studentNo))
            {
                return ApiResultHelper.Ok("删除成功");
            }
            return ApiResultHelper.Err("删除失败");
        }

        //批量删除学生
        [HttpDelete]
        //[Authorize(Policy = "teacher")]
        public async Task<ApiResult> DeleteStudentList(string[] studentNoList) {
            if (await _studentRepository.RemoveListAsync(studentNoList))
            {
                return ApiResultHelper.Ok("删除成功");
            }
            return ApiResultHelper.Err("删除失败");
        }

        //修改
        [HttpPut]
        //[Authorize(Policy = "student")]
        public async Task<ApiResult> EditStudent([FromBody] Student student) {
            if (await _studentRepository.EditAsync(student))
            {
                return ApiResultHelper.Ok("修改成功");
            }
            return ApiResultHelper.Err("修改成功");
        }

        //查询全部学生信息
        [HttpGet]
        //[Authorize(Policy = "teacher")]
        public async Task<ApiResult> GetAllStudent(int index=1,int size=15) {
            int total = 0;
            var list = _mapper.Map<List<StudentDto>>(await _studentRepository.SelectPageListAsync(index, size, total, s=>s.StudentId!=0));
            if (list!=null)
            {
                return ApiResultHelper.Ok(list,total);
            }
            //Task<string> s1 = Task.Run<string>(() => { return ""; });
            //Task<string> s2 = Task.Run<string>(() => { return "ssss"; });
            //await Task.WhenAll(s1,s2);
            return ApiResultHelper.Err();
        }

        //查询单个学生信息
        [HttpGet]
        [Authorize(Policy = "student")]
        public async Task<ApiResult> GetFindStudent(string studentNo) {
            var student = _mapper.Map<StudentDto>(await _studentRepository.FindAsync(s => s.StudentNo == studentNo));
            if (student!=null)
            {
                return ApiResultHelper.Ok(student);
            }
            return ApiResultHelper.Err();
        }

        //条件筛选

    }
}
