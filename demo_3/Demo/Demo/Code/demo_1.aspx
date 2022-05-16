<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="demo_1.aspx.cs" Inherits="Demo.Code.demo_1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:Button ID="Button1"  runat="server" Text="Button" />
                </ContentTemplate>
            </asp:UpdatePanel>
            
        </div>
    </form>
    
    <script src="../js/jquery-3.4.1.js"></script>
    <script>
        $(function () {
            $("#Button1").click(function () {

                $.ajax({
                    type: "post",
                    url: "text.aspx/ffff",
                    contentType: "application/json;charset=UTF-8;",
                    dataType: "json",
                    data: { name: "tom" ,age:"18"},
                    success: function (data) { alert(11); console.log(data) },
                    error: function (err) { console.log(err); alert("err") }
                });
            });
        })
    </script>
</body>
</html>
