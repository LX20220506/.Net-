<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="Demo.Code.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../js/jquery-3.4.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClientClick="console.log(11111)" /><asp:Button ID="Button2" OnClick="Button2_Click"  runat="server" Text="Button"  />
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>


            <asp:Button ID="Button3" runat="server" Text="ajax" />

            <input type="button" value="ajax" id="btn" />

        </div>
    </form>
    <script>
        $(function () {
            $("#btn").click(function () {
                alert(11);
                $.ajax({
                    type: "post",                           //提交方式     
                    url: "test.aspx/ffff",                   //一般处理程序的路径
                    data: { name: "TOM" },           //向后台传入的值
                    dataType: "json",                       //返回值格式
                    contentType: "application/json;charset=utf-8;",
                    success: function (data) {
                        console.log(data.d);
                        alert(data.d);
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
