$(function () {
   
    $('#flexigridData').datagrid({
        title: '客户分享列表', //列表的标题
        iconCls: 'icon-site',
        width: '100%',
        height: 'auto',
        striped: true,
        collapsible: true,
        singleSelect: true,
        url: '../CustomerShare/GetData', //获取数据的url
        idField: 'CustomerShareID',
        sortName: 'CreatedTime',
        sortOrder: 'desc',
        queryParams: { "id": $("#CustomerID").val() },
        columns: [[
            { field: 'ck', title: '全选', checkbox: true, width: 80 },
            { field: 'SysPersonName', title: '分享对象', width: 90 },
             {
                 field: 'Authority', title: '权限', width: 125, formatter: function (value, rowData) {
                     var s = "";
                     var checked1 = '';
                     var checked2 = '';
                     if (value == 1) {
                         checked1 = ' checked="checked" ';
                     }
                     else {
                         checked2 = ' checked="checked" ';
                     }
                     var s = '<label style="margin: 0px 10px 0px 0px;"><input id="' + rowData.CustomerShareID + '_1" name="' + rowData.CustomerShareID + '" type="radio" value="1" ' + checked1 + '  onclick="changeAuthority(this)"/>查看  </label> ';

                     s += '<label style="margin: 0px 10px 0px 0px;"><input id="' + rowData.CustomerShareID + '_2"  name="' + rowData.CustomerShareID + '" type="radio" value="2" ' + checked2 + ' onclick="changeAuthority(this)"/> 编辑 </label>';

                     return s;
                 }
             }
            , {
                field: 'CreatedTime', title: '分享时间', width: 125, formatter: function (value, rec) {
                    if (value) {
                        return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                    }
                }
            }
            //, {
            //    field: 'State', title: '操作', width: $(this).width() * 0.2, align: 'center', resizable: false,
            //    formatter: function (value, rowData, rowIndex) {
                                        
            //        var a = '<input type="button" value="取消分享" OnClick="getCancelShare(\'' + rowData.CustomerShareID + '\',\'' + rowData.SysPersonName + '\')"/>';
            //        return a;

            //    }
            //}
        ]],
        //onDblClickRow: function () {
        //    flexiModify();
        //},
        pagination: true,
        rownumbers: true

    });


    $('#ShareHistoryData').datagrid({
        title: '分享历史', //列表的标题
        iconCls: 'icon-site',
        width: '100%',
        height: 'auto',
        striped: true,
        collapsible: true,
        singleSelect: true,
        url: '../CustomerShare/GetCustomerShareData', //获取数据的url
        idField: 'CustomerShareID',
        sortName: 'CreatedTime',
        sortOrder: 'desc',
        queryParams: { "id": $("#CustomerID").val() },
        columns: [[
            { field: 'SysPersonIDFrom', title: '分享人', width: 90 },
            { field: 'SysPersonIDTo', title: '接收分享', width: 90 },
            { field: 'ShareRemark', title: '说明', width: 290 }
             , {
                 field: 'LastUpdatedTime', title: '分享时间', width: 165, formatter: function (value, rec) {
                     if (value) {
                         return datetimeConvert(value, "yyyy-MM-dd hh:mm");
                     }
                 }
             }
        ]],
        pagination: true,
        rownumbers: true

    });



    var parent = window.dialogArguments; //获取父页面
    if (parent == "undefined" || parent == null) {
        //首先获取iframe标签的id值

        var iframeid = "15070116034045347812e83f254d2";

        //然后关闭AJAX相应的缓存
        $.ajaxSetup({
            cache: false
        });

        //获取按钮值
        $.getJSON("../Home/GetToolbar", { id: iframeid }, function (data) {
            if (data == null) {
                return;
            }
            $('#flexigridData').datagrid("addToolbarItem", data);
        });
    } else {

        //添加选择按钮
        $('#flexigridData').datagrid("addToolbarItem", [{ "text": "选择", "iconCls": "icon-ok", handler: function () { flexiSelect(); } }]);
    }
});


//“查询”按钮，弹出查询框
function flexiQuery() {

    //将查询条件按照分隔符拼接成字符串
    var search = "";
    var startDate = $("#StartDate").datetimebox('getValue');
    var EndDate = $("#EndDate").datetimebox('getValue');

    if (startDate != "") {
        search = search + 'StartDate' + "&" + startDate + '^';
    }
    if (EndDate != "") {
        search = search + 'EndDate' + "&" + EndDate + '^';
    }

    //执行查询
    $('#flexigridData').datagrid('load', { search: search, id: $("#CustomerID").val() });
}


//清空
function RemvoeAll() {
    $('#StartDate').datetimebox('setValue', '');
    $('#EndDate').datetimebox('setValue', '');
}
//“选择”按钮，在其他（与此页面有关联）的页面中，此页面以弹出框的形式出现，选择页面中的数据
function flexiSelect() {

    var rows = $('#flexigridData').datagrid('getSelections');
    if (rows.length == 0) {
        $.messager.alert('操作提示', '请选择数据!', 'warning');
        return false;
    }

    var arr = [];
    for (var i = 0; i < rows.length; i++) {
        arr.push(rows[i].CustomerAttributionChangeID);
    }
    //主键列和显示列之间用 ^ 分割   每一项用 , 分割
    if (arr.length > 0) {//一条数据和多于一条
        returnParent(arr.join("&")); //每一项用 & 分割
    }
}
//导航到查看详细的按钮
function getView() {

    var arr = $('#flexigridData').datagrid('getSelections');

    if (arr.length == 1) {

        window.location.href = "/CustomerShare/Details?ID=" + arr[0].CustomerID;

    } else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }
    return false;
}

//导航到创建的按钮
function flexiCreate() {
    window.location.href = "../CustomerShare/Create";
    return false;
}
//导航到修改的按钮
function flexiModify() {

    var arr = $('#flexigridData').datagrid('getSelections');

    if (arr.length == 1) {
        var ID = arr[0].CustomerID;
        window.location.href = "/CustomerShare/Details?ID=" + arr[0].CustomerID;
    } else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }
    return false;

};
//删除的按钮
function flexiDelete() {

    var arr = $('#flexigridData').datagrid('getSelections');
    if (arr.length == 0) {
        $.messager.alert('操作提示', '请选择数据!', 'warning');
        return false;
    }
    if (arr.length == 1) {
        var ID = arr[0].CustomerShareID;
        $.messager.confirm('操作提示', "确认删除：“ " + arr[0].SysPersonName + " ”吗？", function (r) {
            if (r) {
                $.post("../CustomerShare/CancelShare", { ID: ID }, function (res) {
                    if (res == "OK") {
                        //移除删除的数据
                        $.messager.alert('操作提示', '删除成功!', 'info');
                        $("#flexigridData").datagrid("reload");
                        $("#flexigridData").datagrid("clearSelections");
                    }
                    else {
                        if (res == "") {
                            $.messager.alert('操作提示', '删除失败!请查看该数据与其他模块下的信息的关联，或联系管理员。', 'info');
                        }
                        else {
                            $.messager.alert('操作提示', res, 'info');
                        }
                    }
                });
            }
        });

    } else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }
};

//取消分享
function cancelShare() {

    var arr = $('#flexigridData').datagrid('getSelections');
    if (arr.length == 0) {
        $.messager.alert('操作提示', '请选择数据!', 'warning');
        return false;
    }
    if (arr.length == 1) {
        var ID = arr[0].CustomerShareID;
        $.messager.confirm('操作提示', "确认删除：“ " + arr[0].SysPersonName + " ”吗？", function (r) {
            if (r) {
                $.post("../CustomerShare/CancelShare", { ID: ID }, function (res) {
                    if (res == "OK") {
                        //移除删除的数据
                        $.messager.alert('操作提示', '取消分享成功!', 'info');
                        $("#flexigridData").datagrid("reload");
                        $("#flexigridData").datagrid("clearSelections");
                    }
                    else {
                        if (res == "") {
                            $.messager.alert('操作提示', '删除失败!请查看该数据与其他模块下的信息的关联，或联系管理员。', 'info');
                        }
                        else {
                            $.messager.alert('操作提示', res, 'info');
                        }
                    }
                });
            }
        });

    } else {
        $.messager.alert('操作提示', '请选择一条数据!', 'warning');
    }
}

//取消分享
function getCancelShare(CustomerShareID, SysPersonName) {

     
        
        $.messager.confirm('操作提示', "确认要取消：“ " + SysPersonName + " ”分享吗？", function (r) {
            if (r) {
                $.post("../CustomerShare/CancelShare", { ID: CustomerShareID }, function (res) {
                    if (res == "OK") {
                        //移除删除的数据
                        $.messager.alert('操作提示', '取消分享成功!', 'info');
                        $("#flexigridData").datagrid("clearSelections");
                        $("#flexigridData").datagrid("reload");                        
                    }
                    else {
                        if (res == "") {
                            $.messager.alert('操作提示', '取消分享失败。', 'info');
                        }
                        else {
                            $.messager.alert('操作提示', res, 'info');
                        }
                        $("#flexigridData").datagrid("clearSelections");
                        $("#flexigridData").datagrid("reload");
                    }
                });
            }
        }); 
}

//修改权限
function changeAuthority(share) {
    var authority = $(share).val();
    var Id = $(share).attr("name");


    $.post("../CustomerShare/GetChangeAuthority", { CustomerShareID: Id, Authority: authority }, function (res) {
        if (res == "OK") {
            //移除删除的数据
            $.messager.alert('操作提示', '权限修改成功!', 'info');
            $("#flexigridData").datagrid("reload");
            $("#flexigridData").datagrid("clearSelections");
        }
        else {
            if (res == "") {
                $.messager.alert('操作提示', '权限修改失败', 'info');
            }
            else {
                $.messager.alert('操作提示', res, 'info');
            }
        }
    });

}