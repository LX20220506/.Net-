<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="UI.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
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
            <asp:Button ID="Btn_Add" runat="server"  Text="添加" OnClick="Btn_Add_Click" />
        </div>
    </form>
</body>
</html>
