<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Demo.Code.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../jquery-easyui-1.8.6/themes/default/easyui.css" rel="stylesheet" />
    <link href="../jquery-easyui-1.8.6/themes/icon.css" rel="stylesheet" />
    <link href="../jquery-easyui-1.8.6/demo/demo.css" rel="stylesheet" />
    <script src="../jquery-easyui-1.8.6/jquery.min.js"></script>
    <script src="../jquery-easyui-1.8.6/jquery.easyui.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
	<div style="margin:20px 0;"></div>
	<table id="dg" title="Custom DataGrid Pager" style="width:700px;height:250px"
			data-options="rownumbers:true,singleSelect:true,pagination:true,url:'datagrid_data1.json',method:'get'">
		<thead>
			<tr>
				<th data-options="field:'itemid',width:80">Item ID</th>
				<th data-options="field:'productid',width:100">Product</th>
				<th data-options="field:'listprice',width:80,align:'right'">List Price</th>
				<th data-options="field:'unitcost',width:80,align:'right'">Unit Cost</th>
				<th data-options="field:'attr1',width:240">Attribute</th>
				<th data-options="field:'status',width:60,align:'center'">Status</th>
			</tr>
		</thead>
	</table>
	<script type="text/javascript">
		$(function () {
            $.ajax({
                type: "post",//要用post方式   
                url: 'index.aspx/GetData',//方法所在页面和方法名   
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (data) {
                    console.log(data.d);
                      
                },
                error: function (err) {
                    alert("111");
                }
            });

			var pager = $('#dg').datagrid().datagrid('getPager');	// get the pager of datagrid
			

			pager.pagination({
				buttons:[{
					iconCls:'icon-search',
					handler:function(){
						alert('search');
					}
				},{
					iconCls:'icon-add',
					handler:function(){
						alert('add');
					}
				},{
					iconCls:'icon-edit',
					handler:function(){
						alert('edit');
					}
				}]
			});		
            $(".pagination-info").hide();
		})
    </script>
    </form>
</body>
</html>
