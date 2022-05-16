<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="UI.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <%if (false)  {%>
        <h1>111111</h1>
            <% }
                else
                {%>
        <h1>22222</h1>
                 <%} %>

        <%foreach (var item in arr)
            {%>
                <h1> <% Response.Write(item.ToString()); %></h1>
            <%} %>
        <div>
            <a href="#" id="ii" >ajax</a>

            <p>
                名稱:<input type="text"  value="" />
            </p>
            <p>
                密碼:<input type="password" value="" />
            </p>
            <p>
                保存密碼:<input type="checkbox"  onchange="check(this)" id="fuxuan"  />
            </p>
            <p>
                <input type="button" value="登錄" />
            </p>
        </div>
    </form>

    <script src="js/jquery-3.4.1.js"></script>
    <script>
        function check(e) {
            console.log(e.checked);
            if (e.checked) {
                localStorage.setItem("name", "tom", 0.01);
                console.log(localStorage.getItem("name"));
            } else {
                localStorage.removeItem("name")
            }
        }

        $(function () {
            $("#ii").click(function () {
                $.ajax({
                    type: "post",//要用post方式   
                    url: 'WebForm2.aspx/Show',//方法所在页面和方法名   
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    data: "{name:'tom'}",//方法Show()的入参
                    success: function (data) {
                        console.log(data);
                        alert(data.d);//返回的数据用data.d获取内容   
                    },
                    error: function (err) {
                        alert("111");
                    }
                });
            });
        })
    </script>
</body>
</html>
