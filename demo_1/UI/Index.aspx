<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="UI.Index" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="js/jquery-easyui-1.8.6/themes/default/easyui.css" rel="stylesheet" />
    <link href="js/jquery-easyui-1.8.6/themes/icon.css" rel="stylesheet" />
    
    
</head>
<body>
    <a href="Add.aspx">添加</a>
    <a href="#" runat="server">批量刪除</a>
    <form id="form1" runat="server">
        <table>
            <tr>
                <th><a href="#" id="btn">全選</a></th>
                <th>商品編號</th>
                <th>商品名稱</th>
                <th>商品類型</th>
                <th>商品價格</th>
                <th>商品庫存</th>
                <th>操作</th>
            </tr>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <asp:Panel>
                        <tr>
                            <td><input type="checkbox"  class="CheckBoxList" /></td>
                            <td><%# Eval("ID") %></td>
                            <td><%# Eval("Name") %></td>
                            <td><%# Eval("TypeName") %></td>
                            <td><%# Eval("Price") %></td>
                            <td><%# Eval("KuCun") %></td>
                            <td>
                                <asp:LinkButton ID="Delete" CommandName='<%# Eval("ID") %>' OnClick="Delete_Click" runat="server" OnClientClick="return confirm('您確定要刪除嗎')">刪除</asp:LinkButton>
                                <a href="update.aspx?id=<%# Eval("ID") %>" >修改</a>
                            </td>
                        </tr>
                    </asp:Panel>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div id="pp" class="easyui-pagination" data-options="total:2000,pageSize:10,pageNumber:50" style="background:#efefef;border:1px solid #ccc;"></div>

    </form>
    <script src="js/jquery-3.4.1.js"></script>
    <script src="js/jquery-easyui-1.8.6/jquery.min.js"></script>
    <script src="js/jquery-easyui-1.8.6/jquery.easyui.min.js"></script>
    <script>

        
        $(function () {
            $('#pp').pagination({
                onSelectPage: function (pageNumber, pageSize) {


                    $(this).pagination('loading');
                    alert('pageNumber:' + pageNumber + ',pageSize:' + pageSize);
                    $(this).pagination('loaded');
                }
            });
            

            $("#btn").click(function () {
                $.ajax({
                    type: "post",                           //提交方式     
                    url: "WebForm2.aspx/Show",                   //一般处理程序的路径
                    data: { name: "TOM" },           //向后台传入的值
                    dataType: "json",                       //返回值格式
                    contentType:"application/json;charset=utf-8;",
                    success: function (data) {
                        console.log(data);
                    }
                });
            })
        })
    </script>
</body>
</html>
