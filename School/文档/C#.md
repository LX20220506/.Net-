# ASP.NET Core

# 对象转换成json

~~~C#
var teacher = await _teacherRepository.FindAsync(t => t.TeacherNo == user.userName && t.TeacherPwd == pwd);
if (teacher != null)
	{
    	string token = JWTHelper.GetToken(teacher.TeacherId, teacher.TeacherName);
        // JsonConvert.SerializeObject(对象)  可以将对象转换成json格式
        var obj = new 
        {
            type = "teacher", 
            token = token, 
            account = JsonConvert.SerializeObject(_mapper.Map<TeacherDto>(teacher))
        };
		return ApiResultHelper.Ok(obj);
	}
~~~

