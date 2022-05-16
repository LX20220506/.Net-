<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="js/jquery-3.4.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="button" id="btn" value="ajax" />
        </div>
    </form>

        <script>
        $(function () {
            $("#btn").click(function () {
                alert(11);
                $.ajax({
                    type: "POST",                           //提交方式     
                    url: "WebForm1.aspx/Show",                   //一般处理程序的路径
                    dataType: "JSON",                       //返回值格式
                    data: "{'name':'tom'}",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        console.log(data.d);
                    },
                    Error: function (err) {
                        alert(err);
                    }
                });
            });
        })

        </script>
</body>
</html>
