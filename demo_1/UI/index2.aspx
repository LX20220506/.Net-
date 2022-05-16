<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index2.aspx.cs" Inherits="UI.index2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="js/jquery-easyui-1.8.6/themes/default/easyui.css" rel="stylesheet" />
    <link href="js/jquery-easyui-1.8.6/themes/icon.css" rel="stylesheet" />
    
</head>
<body>
    <form id="form1" runat="server">
        <div>
	<div style="margin:20px 0;"></div>
	<div class="easyui-layout" style="width:700px;height:350px;">
		<div data-options="region:'north'" style="height:50px"></div>
		<div data-options="region:'south',split:true" style="height:50px;"></div>
		<div data-options="region:'east',split:true" title="East" style="width:100px;"></div>
		<div data-options="region:'west',split:true" title="West" style="width:400px;">
            <ul id="tt" class="easyui-tree">
    <li>
        <span>Folder</span>
        <ul>
            <li>
                <span>Sub Folder 1</span>
                <ul>
                    <li>
                        <span><a href="#">File 11</a></span>
                    </li>
                    <li>
                        <span>File 12</span>
                    </li>
                    <li>
                        <span>File 13</span>
                    </li>
                </ul>
            </li>
            <li>
                <span>File 2</span>
            </li>
            <li>
                <span>File 3</span>
            </li>
        </ul>
    </li>
    <li>
        <span>File21</span>
    </li>
</ul>
		</div>
		<div data-options="region:'center',title:'Main Title',iconCls:'icon-ok'">
			<table class="easyui-datagrid"
					data-options="url:'datagrid_data1.json',method:'get',border:false,singleSelect:true,fit:true,fitColumns:true">
				<thead>
					<tr>
						<th data-options="field:'itemid'" width="80">Item ID</th>
						<th data-options="field:'productid'" width="100">Product ID</th>
						<th data-options="field:'listprice',align:'right'" width="80">List Price</th>
						<th data-options="field:'unitcost',align:'right'" width="80">Unit Cost</th>
						<th data-options="field:'attr1'" width="150">Attribute</th>
						<th data-options="field:'status',align:'center'" width="60">Status</th>
					</tr>
				</thead>
			</table>
		</div>
	</div>

            <div id="pp" class="easyui-pagination" data-options="total:2000,pageSize:10" style="background:#efefef;border:1px solid #ccc;"></div>
        </div>
    </form>

    <script src="js/jquery-3.4.1.js"></script>
    <script src="js/jquery-easyui-1.8.6/jquery.min.js"></script>
    <script src="js/jquery-easyui-1.8.6/jquery.easyui.min.js"></script>
    <script>
        $(function() {
            $('#pp').pagination({
                onSelectPage: function (pageNumber, pageSize) {
                    $(this).pagination('loading');
                    alert('pageNumber:' + pageNumber + ',pageSize:' + pageSize);
                    $(this).pagination('loaded');
                }
            });
        });
    </script>
</body>
</html>
