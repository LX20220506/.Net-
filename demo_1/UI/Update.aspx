<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="UI.Update" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="hidden" id="id" runat="server" value="" />
            <p>
                名稱：<input type="text" ID="name" runat="server" value="" />
            </p>
            <p>
                類型：<asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
            </p>
            <p>
                價格：<input type="text" id="price" runat="server" value="" />
            </p>
            <p>
                庫存：<input type="number" id="kuncun" runat="server" value="" />
            </p>
            <asp:Button ID="Btn_Update" runat="server"  Text="修改" OnClick="Btn_Update_Click"  />
        </div>
    </form>
</body>
</html>
