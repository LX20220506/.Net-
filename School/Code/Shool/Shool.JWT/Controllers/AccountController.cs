using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shool.Entity.Dto;
using Shool.Entity.Models;
using Shool.IRepository;
using Shool.Utility;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Shool.JWT.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public AccountController(IStudentRepository studentRepository,
            ITeacherRepository teacherRepository,
            IAdminRepository adminRepository,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        #region 私有类型
        public class PostLogin {
            public string userName { get; set; }
            public string userPwd { get; set; }
        }
        #endregion

        [HttpPost]
        public async Task<ApiResult> Login([FromBody] PostLogin user) {

            if (user.userPwd!=""&&user.userPwd!=null)
            {
                string pwd = MD5Helper.MD5Encrypt64(user.userPwd);

                if (user.userName.Contains("F"))
                {
                    var teacher = await _teacherRepository.FindAsync(t => t.TeacherNo == user.userName && t.TeacherPwd == pwd);
                    if (teacher != null)
                    {
                        string token = JWTHelper.GetToken(teacher.TeacherId, teacher.TeacherName,"teacher");
                        // JsonConvert.SerializeObject(对象)  可以将对象转换成json格式
                        //account = JsonConvert.SerializeObject(_mapper.Map<TeacherDto>(teacher))
                        return ApiResultHelper.Ok(token);
                    }
                }
                else if (user.userName.Contains("admin"))
                {
                    var admin = await _adminRepository.FindAsync(a=>a.AdminName==user.userName && a.AdminPwd==user.userPwd);
                    if (admin != null)
                    {
                        string token = JWTHelper.GetToken(admin.AdminId, admin.AdminName, "admin");
                        return ApiResultHelper.Ok(token);
                    }
                }
                else
                {
                    var student = await _studentRepository.FindAsync(s => s.StudentName == user.userName && s.StudentPwd == pwd);
                    if (student != null)
                    {
                        string token = JWTHelper.GetToken(student.StudentId, student.StudentName,"student");
                        return ApiResultHelper.Ok(token);
                    }
                }
                
            }
            
            return ApiResultHelper.Err("用户名或密码错误");
        }

        [HttpPost]
        public async Task<ApiResult> Register(Teacher teacher) {

            teacher.TeacherPwd = MD5Helper.MD5Encrypt64(teacher.TeacherPwd);
            
            if (await _teacherRepository.AddAsync(teacher))
            {
                return ApiResultHelper.Ok(teacher);
            }
            return ApiResultHelper.Err("注册失败");
        }


    }
}
